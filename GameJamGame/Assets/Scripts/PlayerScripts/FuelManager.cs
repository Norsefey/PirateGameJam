using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelManager : MonoBehaviour
{
    public static FuelManager instance;
    private float currentFuel;
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
        if(fuelSlider != null)
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
    public bool UseFuel(float amount)
    {// for abilities, returns true if consumption amount does not deplete below zero
        if(currentFuel - amount > 0)
        {
            currentFuel -= amount;
            return true;
        }
        else
        {
            return false;
        }
        
    }
    public bool HasFuel()
    {// check if we have fuel, used by movement 
        return currentFuel > 0;
    }
}
