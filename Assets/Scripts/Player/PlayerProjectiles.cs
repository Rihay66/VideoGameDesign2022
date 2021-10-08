using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this scripts purpose is to detect what direction the user is facing in order to tell where to shoot the item
public class PlayerProjectiles : MonoBehaviour
{
    //initalizing variables
    [HideInInspector]
    public float damage = 10f;

    public float projSpeed = 5f;

    private float projectileKillTime = 3f;
    private Rigidbody2D rb;

    

    // Start is called before the first frame update
    void Start()
    {
        // detecting whether or not to shoot right or left
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * projSpeed;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //now adding the velocity for the projectile  

        killTime();
    }
    private void killTime()
    {
        Object.Destroy(gameObject, projectileKillTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EntityStats health = collision.gameObject.GetComponent<EntityStats>();
            if (health != null)
            {
                health.TakeDamage(damage);
                Debug.Log("Hit! Took " + damage + "damage!");
                
            }

            Destroy(gameObject);

        }
        
    }

}
