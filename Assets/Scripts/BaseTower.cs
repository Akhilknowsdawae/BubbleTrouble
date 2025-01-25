using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTower : MonoBehaviour
{
    protected Scanner scanner;
    protected DragAndDrop dragAndDrop;

    [SerializeField]
    protected float Range = 10.0f;

    [SerializeField]
    protected float Damage = 2.0f;

    [SerializeField]
    protected float fireRate = 1.0f;

    [SerializeField]
    protected bool bShouldRotate = true;

    protected bool bCanAction = true;

    float fireTimer = 0.0f;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        scanner = GetComponent<Scanner>();
        dragAndDrop = GetComponent<DragAndDrop>();

        scanner.enabled = false;

        fireTimer = fireRate;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        ActionCooldown();
        LookAtTarget(scanner.GetTarget());
    }

    protected abstract void Action();

    void ActionCooldown()
    {
        if (!bCanAction)
        {
            fireTimer -= Time.deltaTime;

            if (fireTimer <= 0.0f)
            {
                bCanAction = true;
            }
        }
    }

    void LookAtTarget(Transform target)
    {
        if (bShouldRotate && target)
        {
            Vector3 direction = target.position - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, angle));
        }
    }
}
