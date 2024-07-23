using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    [Space(5), Header("Fuel Management")]
    public float maxFuel = 100;
    public float fuelBurnRate = 0.02f;
    [Space(5), Header("Movement")]
    public float maxSpeed = 80f;
    public float acceleration = 12f;
    public float steerStrength = 15f;

    [Space(5), Header("Car Body")]
    public Rigidbody carRB;
    public float carWeight = 300;
    public float carDrag = 2;

    [Space(5), Header("Collections")]
    public float upgradeCollection = 0;
    private float collectionRate = 1;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        carRB.drag = carDrag;
        carRB.mass = carWeight;
    }
    public void AddToUpgradeCollection(float value)
    {
        upgradeCollection += value * collectionRate;
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
