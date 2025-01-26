using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("References")]
    public SplitEnemy splitEnemy;

    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] public int unitCurrency = 20;
    [SerializeField] private bool isSplit = false;

    PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        if(hitPoints <= 0 && !isSplit) {
            EnemySpawner.onEnemyDestroy.Invoke();
            playerController.SetCurrency(unitCurrency + playerController.GetCurrency());
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
