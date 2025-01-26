using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField] public int health;
    [SerializeField] public int maxhealth = 100;
    void Start()
    {
        health = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Time.timeScale = 0;
            //Put in gameover sequence here.
        }
    }
}
