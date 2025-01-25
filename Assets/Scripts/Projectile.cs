using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 600.0f;
    public float lifeTime = 5.0f;
    float Damage = 0.0f;
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

    public void SetDamage(float inDamage)
    {
        Damage = inDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DummyEnemy>())
        {
            // Do Damage
            Destroy(gameObject);
        }
    }
}
