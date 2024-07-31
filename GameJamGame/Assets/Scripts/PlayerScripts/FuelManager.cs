using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FuelManager : MonoBehaviour
{
    public static FuelManager instance;
    public float currentFuel;
    [SerializeField] private Slider fuelSlider;

    void Start()
    {
        instance = this;
        // might disable this later/ if we want fuel amount to carry over from previous level
        currentFuel = PlayerStats.Instance.maxFuel;
        if (fuelSlider != null)
        {
            fuelSlider.maxValue = PlayerStats.Instance.maxFuel;
            fuelSlider.value = currentFuel;
        }
            
    }

    public void BurnFuel()
    {// for movement, consumes fuel at the fuel burn rate
        currentFuel -= PlayerStats.Instance.fuelBurnRate;
        if (currentFuel <= 0)
            LoseGame();
        if (fuelSlider != null)
            fuelSlider.value = currentFuel;
    }

    public void ReplenishFuel(float amount)
    {// for pick up items to restore fuel
        currentFuel += amount;
        if(currentFuel > PlayerStats.Instance.maxFuel)
        {
            currentFuel = PlayerStats.Instance.maxFuel;
        }

        Debug.Log("Fuel Replenished: " + currentFuel + "/" + PlayerStats.Instance.maxFuel);
    }
    public void DecreaseFuel(float amount)
    {// for abilities, returns true if consumption amount does not deplete below zero
        if(currentFuel - amount > 0)
        {
            currentFuel -= amount;
        }
        else
        {
            LoseGame();
        }
        
    }
    public bool HasFuel()
    {// check if we have fuel, used by movement 
        return currentFuel > 0;
    }


    private void LoseGame()
    {
        SceneManager.LoadScene(4);
    }
}
