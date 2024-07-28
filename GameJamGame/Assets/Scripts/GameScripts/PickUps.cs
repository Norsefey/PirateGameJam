using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    public enum PickUpType
    {
        Fuel,
        Upgrade,
        TempBonus
    }

    public PickUpType type;

    [Header("Fuel Type Variables")]
    [SerializeField] private float replenishAmount = 5;
    [Header("Upgrade Type Variables")]
    [SerializeField] private float upgradeAmount = 5;

    public enum BonusType
    {
        None,
        SpeedIncrease,
        FuelEfficiency,
        DoublePickupAmount
    }
    [Header("Bonus Type Variables")]
    [SerializeField] BonusType bonusType;
    [SerializeField] float bonusTime = 4;
    [Tooltip("The New Temp value of the Stat")]
    [SerializeField] float bonusValue = 120;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case PickUpType.Fuel:
                    // increase current fuel
                    FuelManager.instance.ReplenishFuel(replenishAmount);
                    Destroy(gameObject);
                    break;
                case PickUpType.Upgrade:
                    // increase count in upgrade counter
                    PlayerStats.Instance.AddToUpgradeCollection(upgradeAmount);
                    Destroy(gameObject);
                    break;
                case PickUpType.TempBonus:
                    // give player a temp Bonus
                    ActivateBonus();
                    Destroy(gameObject);
                    break;
            }
        }
    }
    private void ActivateBonus()
    {
        switch (bonusType)
        {
            case BonusType.SpeedIncrease:
               StartCoroutine(PlayerStats.Instance.TempIncreaseSpeed(bonusTime, bonusValue));
                break;
            case BonusType.FuelEfficiency:
                StartCoroutine(PlayerStats.Instance.TempFuelBurnRate(bonusTime, bonusValue));
                break;
            case BonusType.DoublePickupAmount:
                StartCoroutine(PlayerStats.Instance.TempIncreaseCollectionRate(bonusTime, bonusValue));
                break;
        }
    }

}
