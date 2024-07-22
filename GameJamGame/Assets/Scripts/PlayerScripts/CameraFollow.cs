using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private bool followTarget = true;
    [SerializeField] private bool lookatTarget = false;

    [SerializeField] private Transform camTarget;
    [SerializeField] private float followSpeed = 0.16f;
    [SerializeField] private Vector3 offSet = Vector3.zero;
    private Vector3 velocity;
    private Vector3 rotation;
    private float zTemp = 0;
    public float y_Offset;

    private void Start()
    {
        //y_Offset = transform.position.y;
        zTemp = offSet.z;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            rotation = transform.rotation.eulerAngles;
            lookatTarget = true;
            offSet.z *= -1;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            lookatTarget = false;
            offSet.z = zTemp;
            transform.rotation = Quaternion.Euler(rotation);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (followTarget)
        {
            Vector3 newPos = Vector3.SmoothDamp(transform.position, camTarget.position + offSet, ref velocity, followSpeed * Time.deltaTime);
            newPos.y = y_Offset + camTarget.position.y;
            transform.position = newPos;
        }
        if(lookatTarget)
            transform.LookAt(camTarget);
    }
}
