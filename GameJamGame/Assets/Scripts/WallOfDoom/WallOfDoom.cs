using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOfDoom : MonoBehaviour
{
    [Header("Player Distances")]
    [SerializeField] Transform player;
    [SerializeField] private float closeDistance = 200;
    [SerializeField] private float farDistance = 300;

    [Header("Speeds")]
    [SerializeField] private float defaultMoveSpeed = 5;
    [SerializeField] private float slowSpeed = 2;
    [SerializeField] private float fastSpeed = 8;

    private float moveSpeed = 5;
    public float distanceToPlayer = 0;

    [Header("Visuals")]
    private Renderer wallRenderer;
    private string heightMapProperty = "_Parallax"; // The property name for the height map scale in the shader
    [SerializeField] private float minHeightValue = 0f; // Minimum height map value
    [SerializeField] private float maxHeightValue = 1f; // Maximum height map value
    [SerializeField] private float parallaxSpeed = 1f; // Speed of the ping-pong effect

    // Start is called before the first frame update
    void Start()
    {
        wallRenderer = GetComponent<Renderer>();
        moveSpeed = defaultMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < closeDistance)
        {
            moveSpeed = slowSpeed;
        }
        else if (distanceToPlayer > farDistance) 
        {
            moveSpeed = fastSpeed;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
        }

        // a visual effect to the wall's material
        float pingPongValue = Mathf.PingPong(Time.time * parallaxSpeed, maxHeightValue - minHeightValue) + minHeightValue;
        wallRenderer.material.SetFloat(heightMapProperty, pingPongValue);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
