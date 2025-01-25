using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DragAndDrop : MonoBehaviour
{
    public Tilemap Map;
    public float Radius = 0.5f;

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
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, Radius, LayerMask.GetMask("Default"));

        float closestDistance = 100.0f;
        Vector2 snapPoint = transform.position;

        foreach (Collider2D c in hitColliders)
        {
            if (c.CompareTag("TowerPlacement"))
            {
                float testDistance = Mathf.Abs((c.transform.position - transform.position).magnitude);

                if (testDistance < closestDistance)
                {
                    closestDistance = testDistance;
                    snapPoint = c.transform.position;
                }
            }
        }

        return snapPoint;
    }
}
