using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BaseHealth>())
        {
            collision.gameObject.GetComponent<BaseHealth>().health -= damage;
        }
    }
}
