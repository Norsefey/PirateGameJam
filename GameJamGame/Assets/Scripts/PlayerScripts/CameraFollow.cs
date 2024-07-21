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

    // Update is called once per frame
    void LateUpdate()
    {
        if (followTarget)
        {
            Vector3 newPos = Vector3.SmoothDamp(transform.position, camTarget.position + offSet, ref velocity, followSpeed * Time.deltaTime);
            float yPos = transform.position.y;
            newPos.y = yPos;
            transform.position = newPos;
        }
        if(lookatTarget)
            transform.LookAt(camTarget);
    }
}
