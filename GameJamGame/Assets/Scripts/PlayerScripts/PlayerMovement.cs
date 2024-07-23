using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody moveSphere;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform carModel;
    [SerializeField] private Transform carNormal;

    private float topSpeed, currentSpeed;
    private float rotate, currentRotate;
    private bool isGrounded = false;

    [Header("Movement Values")]
    [SerializeField] private float gravity = 10f;
    [SerializeField] private LayerMask layerMask;


    [Header("Model Parts")]
    [SerializeField] private float wheelRotationRate = 2;
    [SerializeField] private Transform frontWheels;
    [SerializeField] private Transform backWheels;

    [Header("Input Controls")]
    [SerializeField] private KeyCode accelerateKey = KeyCode.W;
    [SerializeField] private KeyCode brakeKey = KeyCode.S;
    

    private void Update()
    {
       // follow collider
        transform.position = moveSphere.transform.position - offset;
        // check if player is on/near ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1, layerMask);
        // steering Input
        float steerInput = Input.GetAxis("Horizontal");
        // player moves if they have Fuel and are on the ground
        if (FuelManager.instance.HasFuel() && isGrounded)
        {
            if (Input.GetKey(accelerateKey))
            {
                // for some reason negative go forward
                topSpeed = -PlayerStats.Instance.maxSpeed;
                FuelManager.instance.BurnFuel();
            }
            if (Input.GetKey(brakeKey))
            {
                topSpeed = PlayerStats.Instance.maxSpeed;
                FuelManager.instance.BurnFuel();
            }
        }
        //Steer
        if (steerInput != 0 && Mathf.Abs(currentSpeed) > 2)
        {
            int dir = steerInput > 0 ? 1 : -1;
            float amount = Mathf.Abs((steerInput));
            Steer(dir, amount);
        }
        // Set current Speed and rotation
        if (!isGrounded || Input.GetKey(KeyCode.Space))
            topSpeed = 0;
        currentSpeed = Mathf.SmoothStep(currentSpeed, topSpeed, Time.deltaTime * PlayerStats.Instance.acceleration); 
        topSpeed = 0f;
        currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * 4f); 
        rotate = 0f;

        // rotate car visual based on steer input, with a base of 15 degrees + 90 for offset of model
        carModel.localEulerAngles = Vector3.Lerp(carModel.localEulerAngles, new Vector3(0, 90 + (steerInput * 15), carModel.localEulerAngles.z), .2f);
        // rotate wheels based on steer input, based on 15 degrees
        if (steerInput != 0)
        {
            frontWheels.localEulerAngles = new Vector3(frontWheels.localEulerAngles.x, (steerInput * 15), frontWheels.localEulerAngles.z);
        }
        // Rotate wheels based on car speed
        float rotationAmount = moveSphere.velocity.magnitude * wheelRotationRate * Time.deltaTime;
        float velocityDirection = Vector3.Dot(moveSphere.velocity, -carModel.forward);

        if (velocityDirection > 0)
        {
            // Moving forward
            frontWheels.Rotate(Vector3.forward, rotationAmount);
            backWheels.Rotate(Vector3.forward, rotationAmount);
        }
        else if (velocityDirection < 0)
        {
            // Moving backward
            frontWheels.Rotate(-Vector3.forward, rotationAmount);
            backWheels.Rotate(-Vector3.forward, rotationAmount);
        }
    }

    private void FixedUpdate()
    {
        // acceleration
        moveSphere.AddForce(-carModel.transform.forward * currentSpeed, ForceMode.Acceleration);
        // gravity
        moveSphere.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        // steering
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.deltaTime * 5f);

        RaycastHit hitOn;
        RaycastHit hitNear;
        // Use the normal of the surface to tilt car
        Physics.Raycast(transform.position + (transform.up * .1f), Vector3.down, out hitOn, 1.1f, layerMask);
        Physics.Raycast(transform.position + (transform.up * .1f), Vector3.down, out hitNear, 2.0f, layerMask);

        //Normal Rotation
        carNormal.up = Vector3.Lerp(carNormal.up, hitNear.normal, Time.deltaTime * 8.0f);
        carNormal.Rotate(0, transform.eulerAngles.y, 0);

    }

    public void Steer(int direction, float amount)
    {
        rotate = (PlayerStats.Instance.steerStrength * direction) * amount;
    }
}
