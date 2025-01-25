using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

enum EPriority
{
    Closest = 0,
    HighestHealth,
    HighestDamage,
}

public class Scanner : MonoBehaviour
{
    EPriority priority = EPriority.Closest;
    Transform target;

    bool bSleep = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!target && !bSleep)
        {
            target = SearchForTarget(5.0f);

            if (target)
            {
                Debug.Log(target.name);
            }
        }
    }

    void SetPriority(EPriority inPriority)
    {
        priority = inPriority;
    }

    Transform SearchForTarget(float range)
    {
        Debug.Log("Searching for Target");

        // Search for targets in a defined area
        // Filter results for enemy class or tag using a layer mask
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);

        List<EnemyMovement> enemies = new List<EnemyMovement>();

        // Filter this list further to gather only the enemy script/class of the object
        foreach (Collider2D c in colliders)
        {
            EnemyMovement enemy = c.GetComponent<EnemyMovement>();

            if (enemy)
            {
                enemies.Add(enemy);
            }
        }

        if (enemies.Count > 0)
        {
            // Choose most appropriate enemy based on priority setting
            switch (priority)
            {
                case EPriority.Closest:
                    return GetClosestEnemyToBase(enemies).transform;
                case EPriority.HighestHealth:
                    return GetEnemyWithMostHealth(enemies).transform;
                case EPriority.HighestDamage:
                    return GetEnemyWithMostDamage(enemies).transform;
            }
        }
        else
        {
            bSleep = true;

            Debug.Log("No enemy in range, sleeping");
        }

        return null;
    }

    Transform GetClosestEnemyToBase(List<EnemyMovement> Enemies)
    {
        float closestDistance = 100.0f;
        Transform desiredTarget = null;
        Transform home = GameObject.Find("HomeBase").transform;

        foreach (EnemyMovement enemy in Enemies)
        {
            float testDistance = Mathf.Abs((enemy.transform.position - home.position).magnitude);

            if (testDistance < closestDistance)
            {
                closestDistance = testDistance;
                desiredTarget = enemy.transform;
            }
        }

        return desiredTarget;
    }

    Transform GetEnemyWithMostHealth(List<EnemyMovement> Enemies)
    {
        float highestHealth = 0.0f;
        Transform desiredTarget = null;

        foreach (EnemyMovement enemy in Enemies)
        {
            // Get enemy remaining health
            // If health is higher than highestHealth variable, record it
            // Set target to the queried enemy transform if health is recorded
        }

        return desiredTarget;
    }

    Transform GetEnemyWithMostDamage(List<EnemyMovement> Enemies)
    {
        float highestDamage = 0.0f;
        Transform desiredTarget = null;

        foreach (EnemyMovement enemy in Enemies)
        {
            // Get enemy damage value
            // if damage value is higher than highestDamage variable, record it
            // Set target to the queried enemy transform if damage is recorded
        }

        return desiredTarget;
    }

    public Transform GetTarget()
    {
        return target;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == target)
        {
            target = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<DummyEnemy>())
        {
            if (bSleep)
            {
                bSleep = false;

                Debug.Log("Enemy entered range, waking up");
            }
        }
    }
}
