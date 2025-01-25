using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 600.0f;
    public float lifeTime = 5.0f;
    int Damage = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    void Move()
    {
        transform.position += (new Vector3(transform.rotation.x, transform.rotation.y, 0) * speed * Time.deltaTime);
    }

    public void SetDamage(int inDamage)
    {
        Damage = inDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Health>())
        {
            collision.GetComponent<Health>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
