using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("References")]
    public SplitEnemy splitEnemy;

    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private bool isSplit = false; 

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        if(hitPoints <= 0 && !isSplit) {
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }
        else if(hitPoints <= 0 && isSplit)
        {
            splitEnemy.Split();
        }
    }

    public int GetRemainingHealth()
    {
        return hitPoints;
    }
}
