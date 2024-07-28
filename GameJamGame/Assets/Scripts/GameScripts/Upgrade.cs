using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] float cost = 50;
    [SerializeField] TMP_Text IncreamentText;
    [SerializeField] TMP_Text costText;
    public enum UpgradeType
    {
        Fuel,
        BurnRate,
        TopSpeed,
        Accel
    }
    public UpgradeType type;
    public float increaseAmount = 20;

    private void Start()
    {
        switch (type)
        {
            case UpgradeType.Fuel:
                IncreamentText.text = PlayerStats.Instance.maxFuel + " -> " + (PlayerStats.Instance.maxFuel + increaseAmount);
                break; 
            case UpgradeType.BurnRate:
                IncreamentText.text = PlayerStats.Instance.fuelBurnRate + " -> " + (PlayerStats.Instance.fuelBurnRate - increaseAmount);
                break;
            case UpgradeType.TopSpeed:
                IncreamentText.text = PlayerStats.Instance.maxSpeed + " -> " + (PlayerStats.Instance.maxSpeed + increaseAmount);
                break;
            case UpgradeType.Accel:
                IncreamentText.text = PlayerStats.Instance.acceleration + " -> " + (PlayerStats.Instance.acceleration + increaseAmount);

                break;
        }
    }

    public void IncreaseMaxFuel(int incrementAmount)
    {
        if(PlayerStats.Instance.upgradePoints >= cost)
        {
            PlayerStats.Instance.maxFuel += incrementAmount;
            PlayerStats.Instance.upgradePoints -= cost;
        }
        IncreamentText.text = PlayerStats.Instance.maxFuel + " -> " + (PlayerStats.Instance.maxFuel + incrementAmount);
    }

    public void IncreaseEfficiency(float decreaseAmount)
    {
        if (PlayerStats.Instance.upgradePoints >= cost)
            PlayerStats.Instance.fuelBurnRate -= decreaseAmount;

        IncreamentText.text = PlayerStats.Instance.fuelBurnRate + " -> " + (PlayerStats.Instance.fuelBurnRate - decreaseAmount);

    }

    public void IncreaseTopSpeed(int incrementAmount)
    {
        if (PlayerStats.Instance.upgradePoints >= cost)
            PlayerStats.Instance.maxSpeed += incrementAmount;
        IncreamentText.text = PlayerStats.Instance.maxSpeed + " -> " + (PlayerStats.Instance.maxSpeed + incrementAmount);

    }

    public void IncreaseAcceleration(int incrementAmount)
    {
        if (PlayerStats.Instance.upgradePoints >= cost)
            PlayerStats.Instance.acceleration += incrementAmount;
        IncreamentText.text = PlayerStats.Instance.acceleration + " -> " + (PlayerStats.Instance.acceleration + incrementAmount);

    }
}
