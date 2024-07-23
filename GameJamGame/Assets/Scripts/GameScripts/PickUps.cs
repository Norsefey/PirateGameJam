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
        SpeedIncrease,
        FuelEfficany,
        IncreasePickupRange,
        DoublePickupAmount
    }
    [Header("Bonus Type Variables")]
    [SerializeField] BonusType bonusType;
    [SerializeField] float bonusTime = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case PickUpType.Fuel:
                    FuelManager.instance.ReplenishFuel(replenishAmount);
                    Destroy(gameObject);
                    break;
                case PickUpType.Upgrade:
                    // increase count in upgrade counter
                    break;
                case PickUpType.TempBonus:
                    // give player a type of temp Bonus
                    ActivateBonus();
                    break;
            }
        }
    }

    private void ActivateBonus()
    {
        switch (bonusType)
        {

        }
    }

}
