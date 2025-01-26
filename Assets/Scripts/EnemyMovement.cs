using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float movespeed = 2f;

    private Transform target;
    private int pathIndex = 0;

    private float TotalDistance = 0f;
    private float remainingDistance = 0f;

    private Vector2 PreviousPos;

    private void Start()
    {
        SetTargetForPathIndex();
        TotalDistance = CalculateTotalPathDistance();
        remainingDistance = TotalDistance;
        PreviousPos = transform.position;
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }

        UpdateRemainingDistance();

        //Test
        float remaingDis = GetRemainingTravelDistance();
        Debug.Log($"Remaining Distance to Travel: {remaingDis}");
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * movespeed;
    }

    public float GetMoveSpeed()
    {
        return movespeed;
    }

    public void SetMoveSpeed(float newSpeed)
    {
        movespeed = newSpeed;
    }

    private float CalculateTotalPathDistance()
    {
        float pathDistance = 0f;
        for (int i = 0; i < LevelManager.main.path.Length - 1; i++)
        {
            pathDistance += Vector2.Distance(LevelManager.main.path[i].position, LevelManager.main.path[i + 1].position);
        }
        return pathDistance;
    }

    private void UpdateRemainingDistance() {

        float distanceTraveled = Vector2.Distance(PreviousPos, transform.position);

        remainingDistance -= distanceTraveled;

        PreviousPos = transform.position;
    }

    public float GetRemainingTravelDistance()
    {
        return remainingDistance;
    }

    public void SetPathIndex(int index)
    {
        if(index >= 0 && index < LevelManager.main.path.Length)
        {
            pathIndex = index;
            target = LevelManager.main.path[pathIndex];
        }
    }

    public int GetCurrentPathIndex()
    {
        return pathIndex;
    }

    private void SetTargetForPathIndex()
    {
        if(pathIndex < LevelManager.main.path.Length)
        {
            target = LevelManager.main.path[pathIndex];
        }
    }
}