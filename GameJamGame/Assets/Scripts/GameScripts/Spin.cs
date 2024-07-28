using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotSpeed = 5;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 1 * rotSpeed * Time.deltaTime, 0);
    }
}
