using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] float cost = 50;
    [SerializeField] TMP_Text IncreamentText;
    [SerializeField] TMP_Text costText;

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
    }

    public void IncreaseTopSpeed(int incrementAmount)
    {
        if (PlayerStats.Instance.upgradePoints >= cost)
            PlayerStats.Instance.maxSpeed += incrementAmount;
    }

    public void IncreaseAcceleration(int incrementAmount)
    {
        if (PlayerStats.Instance.upgradePoints >= cost)
            PlayerStats.Instance.acceleration += incrementAmount;
    }
}
