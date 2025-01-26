using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitEnemy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public GameObject smallerEnemyPrefab;
    [SerializeField] public int numberOfEnemies = 3; // Number of smaller enemies to spawn
    [SerializeField] public float splitDistance = 0.5f; //Distance between the splited enemies
    

    private EnemyMovement enemyMovement;

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void Split()
    {
        Vector2 originalPosition = transform.position;
        int pathIndex = enemyMovement.GetCurrentPathIndex();

        for(int i = 0; i < numberOfEnemies; i++)
        {
            Vector2 offset = GetRandomOffset();
            GameObject newEnemy = Instantiate(smallerEnemyPrefab, originalPosition + offset, Quaternion.identity);

            EnemyMovement newEnemyMovement = newEnemy.GetComponent<EnemyMovement>();
            newEnemyMovement.SetPathIndex(pathIndex);
        }

        EnemySpawner.onEnemySplit?.Invoke(numberOfEnemies);

        EnemySpawner.onEnemyDestroy?.Invoke();

        Destroy(gameObject);
    }

    private Vector2 GetRandomOffset()
    {
        float angle = Random.Range(0, 2 * Mathf.PI);
        float x = Mathf.Cos(angle) * splitDistance;
        float y = Mathf.Sin(angle) * splitDistance; 
        
        return new Vector2(x, y);
    }
}