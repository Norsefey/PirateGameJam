using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera cam;

    [SerializeField] Transform camTarget;
    [SerializeField] bool followTarget = true;
    [SerializeField] float followSpeed = 0.16f;
    [SerializeField] Vector3 offSet = Vector3.zero;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        offSet = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget)
        {
            
            Vector3 newPos = Vector3.SmoothDamp(transform.position, camTarget.position + offSet, ref velocity, followSpeed * Time.deltaTime);
            float yPos = transform.position.y;
            newPos.y = yPos;
            transform.position = newPos;
        }
    }
}
