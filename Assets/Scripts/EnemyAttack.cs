using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Base")
        {
            Debug.Log("Tower got hit!!");
            collider.gameObject.GetComponent<BaseHealth>().health -= damage;
        }
    }
}
