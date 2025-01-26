using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScanner : Scanner
{
    BoxCollider2D box;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!target)
        {
            if (collision.GetComponent<EnemyMovement>())
            {
                target = collision.transform;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == target)
        {
            Collider2D[] hits = new Collider2D[10];
            ContactFilter2D filter = new ContactFilter2D();

            box.OverlapCollider(filter, hits);

            if (hits.Length > 0) 
            {
                float closestDistance = 100.0f;
                Transform desiredTarget = null;

                foreach (Collider2D hit in hits)
                {
                    float testDistance = 100.0f;

                    if (hit.GetComponent<EnemyMovement>())
                    {
                        testDistance = hit.GetComponent<EnemyMovement>().GetRemainingTravelDistance();
                    }
                    
                    if (testDistance < closestDistance)
                    {
                        closestDistance = testDistance;
                        desiredTarget = hit.transform;
                    }
                }

                target = desiredTarget;
            }
        }
    }
}
