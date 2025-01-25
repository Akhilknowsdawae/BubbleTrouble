using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitEnemy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private GameObject smallerEnemyPrefab;
    [SerializeField] private int splitCount = 3; // Number of smaller enemies to spawn
    [SerializeField] private float spreadRadius = 1.0f; // How far smaller enemies will spawn from the original point

    private void OnDestroy()
    {
        SpawnSmallerEnemies();
    }

    private void SpawnSmallerEnemies()
    {
        for (int i = 0; i < splitCount; i++)
        {
            
            Vector3 offset = Random.insideUnitCircle * spreadRadius;
            Vector3 spawnPosition = transform.position + offset;

            GameObject smallerEnemy = Instantiate(smallerEnemyPrefab, spawnPosition, Quaternion.identity);

            
            EnemyMovement enemyMovement = smallerEnemy.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.SetMoveSpeed(enemyMovement.GetMoveSpeed() * 1.5f); // Increase speed of smaller enemies
            }
        }
    }
}
