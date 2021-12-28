using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [HideInInspector]
    public float areaDamage;

    private void Awake()
    {
        Destroy(gameObject, 2f);
    }

    //area attack, if player is in zone, it hit player, simple as that
    private void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.CompareTag("Player") && areaDamage > 0f)
        {
            PlayerStats player = collide.GetComponent<PlayerStats>();
            if(player != null)
            {
                player.currentHealth -= areaDamage;
                Destroy(gameObject);
            }
            return;
        }
    }
}
