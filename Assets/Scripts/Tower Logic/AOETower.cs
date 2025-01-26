using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOETower : BaseTower
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        bShouldRotate = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (bCanAction)
        {
            Action();
        }
    }

    protected override void Action()
    {
        if (scanner.GetTarget())
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, Range);
            if (hit.Length > 0)
            {
                foreach (Collider2D collider in hit)
                {
                    Health health = collider.GetComponent<Health>();

                    if (health)
                    {
                        health.TakeDamage(Damage);
                    }
                }
            }

            bCanAction = false;
        }
    }
}
