using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

enum EPriority
{
    Closest = 0,
    Furthest,
    HighestHealth,
}

public class Scanner : MonoBehaviour
{
    EPriority priority = EPriority.Closest;
    protected Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        if (!target)
        {
            target = SearchForTarget(GetComponent<BaseTower>().GetRange());
        }
    }

    void SetPriority(EPriority inPriority)
    {
        priority = inPriority;
    }

    public void PriorityClose()
    {
        SetPriority(EPriority.Closest);
    }
    
    public void PriorityFar()
    {
        SetPriority(EPriority.Furthest);
    }

    public void PriorityHealth()
    {
        SetPriority(EPriority.HighestHealth);
    }

    Transform SearchForTarget(float range)
    {
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
                case EPriority.Furthest:
                    return GetEnemyFurthestFromBase(enemies).transform;
            }
        }

        return null;
    }

    Transform GetClosestEnemyToBase(List<EnemyMovement> Enemies)
    {
        float closestDistance = 100.0f;
        Transform desiredTarget = null;

        foreach (EnemyMovement enemy in Enemies)
        {
            float testDistance = enemy.GetRemainingTravelDistance();

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
            int health = enemy.GetComponent<Health>().GetRemainingHealth();

            if (health > highestHealth)
            {
                highestHealth = health;
                desiredTarget = enemy.transform;
            }
        }

        return desiredTarget;
    }

    Transform GetEnemyFurthestFromBase(List<EnemyMovement> Enemies)
    {
        float furthestDistance = 0.0f;
        Transform desiredTarget = null;

        foreach (EnemyMovement enemy in Enemies)
        {
            float testDistance = enemy.GetRemainingTravelDistance();

            if (testDistance > furthestDistance)
            {
                furthestDistance = testDistance;
                desiredTarget = enemy.transform;
            }
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
}
