using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private PathVisibility updatePath;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondCap = 15f;

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLefttoSpawn;
    private float eps; //Enemies per second
    private bool isSpawning = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }
    private void Start()
    {
        StartCoroutine(StartWave());
        updatePath = GameObject.FindObjectOfType<PathVisibility>();
    }

    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn >= (1f / eps) && enemiesLefttoSpawn > 0) 
        {
            SpawnEnemy();
            enemiesLefttoSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if(enemiesAlive == 0 && enemiesLefttoSpawn == 0)
        {
            EndWave();
        }

    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        Debug.Log("Wave Started");
        yield return new WaitForSeconds(timeBetweenWaves);

        isSpawning = true;
        enemiesLefttoSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
    }
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        if (enemyPrefabs[index] != null)
        {
            GameObject prefabToSpawn = enemyPrefabs[index];
            Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
        }
    }

    private void EndWave()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Generator");
        foreach (GameObject obj in objects)
        {
            if (obj.GetComponent<GeneratorTower>())
            {
                obj.GetComponent<GeneratorTower>().GenerateCurrency();
            }
        }
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        updatePath.UpdateWaterLevel();
        StartCoroutine(StartWave());
    }
}
