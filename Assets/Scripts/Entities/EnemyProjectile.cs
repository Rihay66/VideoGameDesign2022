using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyProjectile : MonoBehaviour
{
    [HideInInspector]
    public float projectileDamage;

    [Header("Properties")]
    public float speed = 10f;

    private Rigidbody2D rb;

    //[] Make the projectile go outwards depending on what the enemy is facing
    // -- May use a rigidbody to make collision more smoother
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 6f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EntityStats health = collision.gameObject.GetComponent<EntityStats>();
            if (health != null)
            {
                health.TakeDamage(projectileDamage);
            }
            Destroy(gameObject);
        }
    }
}
