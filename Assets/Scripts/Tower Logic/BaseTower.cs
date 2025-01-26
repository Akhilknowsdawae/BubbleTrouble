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
    protected int Damage = 2;

    [SerializeField]
    protected float fireRate = 1.0f;

    [SerializeField]
    protected bool bShouldRotate = true;

    protected bool bCanAction = true;

    float fireTimer = 0.0f;

    [SerializeField] protected int costToBuy = 100;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        scanner = GetComponent<Scanner>();
        dragAndDrop = GetComponent<DragAndDrop>();

        if (scanner)
        {
            scanner.enabled = false;
        }

        fireTimer = fireRate;

        if (GetComponent<CircleCollider2D>())
        {
            GetComponent<CircleCollider2D>().radius = Range;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        ActionCooldown();

        if (scanner)
        {
            LookAtTarget(scanner.GetTarget());
        }
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
                fireTimer = fireRate;
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

    public void PlaceTower()
    {
        dragAndDrop.enabled = false;

        if (scanner)
        {
            scanner.enabled = true;
        }
    }

        public int GetCostToBuy()
    {
        return costToBuy;
    }
}
