using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOfDoom : MonoBehaviour
{
    [Header("Player Distances")]
    [SerializeField] Transform player;
    [SerializeField] Transform measurePoint;
    [SerializeField] private float closeDistance = 220;
    [SerializeField] private float farDistance = 300;
    [SerializeField] private float deathDistance = 210;

    [Header("Speeds")]
    [SerializeField] private float defaultMoveSpeed = 5;
    [SerializeField] private float slowSpeed = 2;
    [SerializeField] private float fastSpeed = 8;

    private float moveSpeed = 5;
    private float distanceToPlayer = 0;

    [Header("Visuals")]
    [SerializeField] private float minHeightValue = 0f; // Minimum height map value
    [SerializeField] private float maxHeightValue = 1f; // Maximum height map value
    [SerializeField] private float parallaxSpeed = 1f; // Speed of the ping-pong effect
    private Renderer wallRenderer;
    private string heightMapProperty = "_Parallax"; // The property name for the height map scale in the shader

    private enum WallState
    {
        close,
        normal,
        far
    }
    private WallState wallState;

    // Start is called before the first frame update
    void Start()
    {
        wallRenderer = GetComponent<Renderer>();
        moveSpeed = defaultMoveSpeed;
        wallState = WallState.normal;
    }

    // Update is called once per frame
    void Update()
    {
        measurePoint.position = new Vector3 (player.position.x, measurePoint.position.y, measurePoint.position.z);
        distanceToPlayer = Vector3.Distance(measurePoint.position, player.position);
        Debug.Log(distanceToPlayer);
        if (distanceToPlayer < closeDistance && wallState != WallState.close)
        {
            moveSpeed = slowSpeed;
            PlayerStats.Instance.GloomEffect();
            wallState = WallState.close;
        }
        else if (distanceToPlayer > farDistance && wallState != WallState.far) 
        {
            moveSpeed = fastSpeed;
            PlayerStats.Instance.RemoveGloom();
            wallState = WallState.far;
        }
        else if (wallState != WallState.normal && (distanceToPlayer < farDistance && (distanceToPlayer > closeDistance)))
        {
            moveSpeed = defaultMoveSpeed;
            PlayerStats.Instance.RemoveGloom();
            wallState = WallState.normal;
        }else if(distanceToPlayer < deathDistance)
        {
            Debug.Log("Dead");
        }

        // a visual effect to the wall's material
        float pingPongValue = Mathf.PingPong(Time.time * parallaxSpeed, maxHeightValue - minHeightValue) + minHeightValue;
        wallRenderer.material.SetFloat(heightMapProperty, pingPongValue);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
