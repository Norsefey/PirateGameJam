using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [Header("Acceleration")]
    [SerializeField] private float topSpeed = 20;
    [SerializeField] private float accelerationRate = 5;
    [SerializeField] private float decelerationRate = 10;
    private float acceleration = 0;
    
    [Header("Steering")]
    [SerializeField] private float steerStrength = 5;


    [Header("Input Controls")]
    [SerializeField] private KeyCode accelrateKey = KeyCode.W;
    [SerializeField] private KeyCode brakeKey = KeyCode.S;

    [SerializeField] LayerMask ground;
    private bool isGrounded = false;
    private float rayDis = 1;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, rayDis, ground);

        Debug.Log(rb.velocity.magnitude);

        // check if player is on ground, and has not reached top speed
        // since magnitude always returns a positive, we can do this check for both acceleration and deceleration
        if(isGrounded && rb.velocity.magnitude < topSpeed)
        {
            if (Input.GetKey(accelrateKey))
                acceleration = accelerationRate;
            else if (Input.GetKey(brakeKey))
                acceleration = -decelerationRate;
        }
        else
        {
            acceleration = 0;
        }
    }

    private void FixedUpdate()
    {
        // adds a force to the local forward of rigidbody
        rb.AddForce(transform.forward * acceleration,ForceMode.Force);
    }
}
