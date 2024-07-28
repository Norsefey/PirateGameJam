using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RearCameraToggle : MonoBehaviour
{
    [SerializeField] Camera rearCam;

    bool camON = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            rearCam.enabled = !camON;
            camON = !camON;
        }
    }
}
