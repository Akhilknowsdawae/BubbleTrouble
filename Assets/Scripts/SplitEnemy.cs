using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitEnemy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public GameObject smallerEnemyPrefab;
    [SerializeField] public int numberOfEnemies = 3; // Number of smaller enemies to spawn
    

    private EnemyMovement enemyMovement;

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void Split()
    {
        Vector2 position = transform.position;
        int pathIndex = enemyMovement.GetCurrentPathIndex();

        for(int i = 0; i < numberOfEnemies; i++)
        {
            GameObject newEnemy = Instantiate(smallerEnemyPrefab, position, Quaternion.identity);
            EnemyMovement newEnemyMovement = newEnemy.GetComponent<EnemyMovement>();
            newEnemyMovement.SetPathIndex(pathIndex);
        }

        Destroy(gameObject);
    }
}