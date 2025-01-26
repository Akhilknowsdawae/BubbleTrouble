using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DragAndDrop : MonoBehaviour
{
    public Tilemap Map;
    public float Radius = 0.5f;
    TowerSpace placement;
    bool bPlaced = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = FindSnapPosition();
    }

    Vector2 FindSnapPosition()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, Radius, LayerMask.GetMask("TowerPlacement"));

        float closestDistance = 100.0f;
        Vector2 snapPosition = transform.position;

        if (hitColliders.Length == 0) 
        {
            placement = null;
        }

        foreach (Collider2D c in hitColliders)
        {
            if (c.GetComponent<TowerSpace>())
            {
                float testDistance = Mathf.Abs((c.transform.position - transform.position).magnitude);

                if (testDistance < closestDistance)
                {
                    closestDistance = testDistance;
                    placement = c.GetComponent<TowerSpace>();
                    snapPosition = placement.transform.position;
                }
            }
        }

        return snapPosition;
    }

    public bool TryPlacement()
    {
        if (placement && !bPlaced)
        {
            if (placement.GetHasTower() == false)
            {
                GetComponent<BaseTower>().PlaceTower();
                placement.SetHasTower(true);
                bPlaced = true;
                transform.position = placement.transform.position;

                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
