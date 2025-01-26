using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTower : BaseTower
{
    public AudioSource meleeSound;

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
            bCanAction = false;
            scanner.GetTarget().GetComponent<Health>().TakeDamage(Damage);
            meleeSound.Play();
        }
    }
}
