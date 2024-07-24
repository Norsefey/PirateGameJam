using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public int levelIndex = 0;
    [Space(5), Header("Fuel Management")]
    public float maxFuel = 100;
    public float fuelBurnRate = 0.04f;
    public float gloomFuelBurnRate = .06f;
    private float defaultfuelBurnRate;

    [Space(5), Header("Movement")]
    public PlayerMovement playerMove;
    public float maxSpeed = 80f;
    public float acceleration = 12f;
    public float steerStrength = 15f;

    [Space(5), Header("Car Body")]
    public Rigidbody carRB;
    public float carWeight = 300;
    public float carDrag = 2;

    [Space(5), Header("Upgrade Points")]
    public float upgradePoints = 0;
    private float collectionRate = 1;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        levelIndex = SceneManager.GetActiveScene().buildIndex;

        carRB.drag = carDrag;
        carRB.mass = carWeight;
        defaultfuelBurnRate = fuelBurnRate;
    }
    public void AddToUpgradeCollection(float value)
    {
        upgradePoints += value * collectionRate;
    }
    public void GloomEffect()
    {
        fuelBurnRate = gloomFuelBurnRate;
        // do other stuff when wall is to close
    }
    public void RemoveGloom()
    {
        fuelBurnRate = defaultfuelBurnRate;
        // disable other stuff
    }
    public void ReachedFinishLine()
    {
        levelIndex++;
        playerMove.EnableFinishLineControls();
    }
    public void DisablePlayer()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }
    public void EnablePlayer()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        playerMove.DisableFinishLineControls();
    }
    public IEnumerator TempIncreaseSpeed(float time, float newValue)
    {
        float temp = maxSpeed;
        maxSpeed = newValue;
        yield return new WaitForSeconds(time);
        maxSpeed = temp;
    }
    public IEnumerator TempFuelBurnRate(float time, float newValue)
    {
        float temp = fuelBurnRate;
        fuelBurnRate = newValue;
        yield return new WaitForSeconds(time);
        fuelBurnRate = temp;
    }
    public IEnumerator TempIncreaseCollectionRate(float time, float newValue)
    {
        float temp = collectionRate;
        collectionRate = newValue;
        yield return new WaitForSeconds(time);
        collectionRate = temp;
    }
}
