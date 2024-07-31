using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wipe : MonoBehaviour
{
    private void Awake()
    {
        if(PlayerStats.Instance != null)
            Destroy(PlayerStats.Instance.gameObject);
    }
}
