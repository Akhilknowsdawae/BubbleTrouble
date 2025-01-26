using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTower : BaseTower
{
    public GameObject projectile;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
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
            if (projectile)
            {
                GameObject proj = Instantiate(projectile, transform.position, Quaternion.Euler((scanner.GetTarget().transform.position - transform.position)));
                proj.GetComponent<Projectile>().SetDamage(Damage);
                proj.GetComponent<Projectile>().SetTarget(scanner.GetTarget());
            }

            bCanAction = false;
        }
    }
}
