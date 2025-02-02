using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float _minSpawnDelay;
    [SerializeField] float _maxSpawnDelay;

    [Tooltip("How often do we decrease the spawn delay")]
    [SerializeField] float _spawnDelayModifyRate;

    [Tooltip("How much faster do enemies spawn every x seconds")]
    [SerializeField] float _spawnDelayMultiplier;

    [SerializeField] GameObject[] _enemies;
    [SerializeField] GameObject _testingTarget;

    private bool _canSpawnEnemies = true;
    private Bounds _bounds;
    private BoxCollider _boxCollider;
    private float time = 0;

    private void Awake()
    {
        //Components
        _boxCollider = GetComponent<BoxCollider>();
        _bounds = _boxCollider.bounds;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (_canSpawnEnemies)
        {
            SpawnEnemy(_boxCollider.bounds, _enemies);
            float cooldownTime = GetCooldownTimer(_minSpawnDelay, _maxSpawnDelay);
            StartCoroutine(Cooldown(cooldownTime));
        }

        if(time >= _spawnDelayModifyRate)
        {
            (_minSpawnDelay, _maxSpawnDelay) = UpdateSpawnDecreaseRate(_minSpawnDelay, _maxSpawnDelay, _spawnDelayMultiplier);
            time = 0f;
        }
    }
    private void SpawnEnemy(Bounds bounds, GameObject[] Enemies)
    {
        Vector3 spawnPos = GetRandPositionInBounds(bounds);

        RaycastHit hit;
        if(Physics.Raycast(bounds.center, Vector3.down, out hit))
        {
            if(hit.collider.GetComponent<Terrain>() != null)
            {
                Terrain terrain = hit.collider.GetComponent<Terrain>();
                spawnPos.y = terrain.SampleHeight(spawnPos);
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
       
        int randIndex = Random.Range(0, Enemies.Length);
        GameObject randEnemy = Enemies[randIndex];
        GameObject enemyObj = Instantiate(randEnemy, spawnPos, Quaternion.identity);

        //Reference the player
        bool isEnemy = enemyObj.TryGetComponent<EnemyBase>(out EnemyBase enemy);
        enemy.NavAgent.Warp(spawnPos);

        if (PlayerStats.Instance != null)
        {
            enemy.Target = PlayerStats.Instance.playerMove.gameObject;
        }
        else enemy.Target = _testingTarget;
    }

    private IEnumerator Cooldown(float cooldownTime)
    {
        _canSpawnEnemies = false;
        yield return new WaitForSeconds(cooldownTime);
        _canSpawnEnemies = true;
    }

    private float GetCooldownTimer(float min, float max)
    {
        float interval = Random.Range(min, max);
        return interval;
    }

    private (float, float) UpdateSpawnDecreaseRate(float ogMin, float ogMax, float multiplier)
    {
        float newMin = ogMin / multiplier;
        float newMax = ogMax / multiplier;

        return (newMin, newMax);
    }

    private Vector3 GetRandPositionInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(x, y, z);
    }
}
