using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    [Header("References")]
    public LevelManager LevelManager;

    [Header("Attributes")]
    [SerializeField] public int health;
    [SerializeField] public int maxhealth = 100;
    private bool isDead = false;
    void Start()
    {
        health = maxhealth;
    }

    void Update()
    {
        if (health <= 0 && !isDead)
        {
            isDead = true;
            //Put in gameover sequence here.
            LevelManager.gameOver();
        }
    }
}
