using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UIElements;

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
    [SerializeField] SpriteRenderer rangeIndicator;

    BoxCollider2D box;
    CircleCollider2D circle;

    FillColliderWithSprite utility;

    int UpgradeLevel = 1;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        scanner = GetComponent<Scanner>();
        dragAndDrop = GetComponent<DragAndDrop>();
        utility = new FillColliderWithSprite();

        if (scanner)
        {
            scanner.enabled = false;
        }

        fireTimer = fireRate;

        box = GetComponent<BoxCollider2D>();
        circle = GetComponent<CircleCollider2D>();

        if (circle)
        {
            circle.radius = Range;
            utility.init(rangeIndicator, null, circle);
        }
        else if (box)
        {
            utility.init(rangeIndicator, box);
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

            Quaternion targetRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, angle));

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 30f * Time.deltaTime);
        }
    }

    public void PlaceTower()
    {
        dragAndDrop.enabled = false;

        if (scanner)
        {
            scanner.enabled = true;
        }

        if (rangeIndicator)
        {
            rangeIndicator.enabled = false;
        }
    }

    public int GetCostToBuy()
    {
        return costToBuy;
    }

    public float GetRange()
    {
        return Range;
    }

    public void UpgradeStats()
    {
        PlayerController player = GameObject.Find("player").GetComponent<PlayerController>();

        if (player.GetCurrency() >= 50)
        {
            if (UpgradeLevel < 3)
            {
                UpgradeLevel++;

                Range += 0.5f;
                Damage += 1;
                fireRate += 1;

                if (circle)
                {
                    circle.radius = Range;
                }
            }

            player.SetCurrency(player.GetCurrency() - 50);
        }
        
    }

    public Scanner GetScanner()
    {
        if (scanner)
        {
            return scanner;
        }

        return null;
    }
}
