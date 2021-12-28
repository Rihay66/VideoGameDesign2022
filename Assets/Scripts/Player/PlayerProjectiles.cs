using UnityEngine;

// this scripts purpose is to detect what direction the user is facing in order to tell where to shoot the item
public class PlayerProjectiles : MonoBehaviour
{
    //initalizing variables
    [HideInInspector]
    public float damage = 10f;

    public float projSpeed = 5f;

    private Rigidbody2D rb; 

    // Start is called before the first frame update
    void Awake()
    {
        // detecting whether or not to shoot right or left
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * projSpeed;
        Destroy(gameObject, 4f);
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
                Destroy(gameObject);
            }
        }
        else if(collision.gameObject.layer != 11)
        {
            Destroy(gameObject);
        }
    }

}
