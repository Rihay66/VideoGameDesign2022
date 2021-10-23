using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    [Header("Stats")]
    public float MaxHealth = 100f;
    public float currentHealth { get; private set; }

    private void Start()
    {
        currentHealth = MaxHealth; // sets current health to the max health
    }

    public void TakeDamage(float damage) // This method is called from other scripts and passes in the damage to the entity
    {
        currentHealth -= damage; // Substracts health with the given damage
        Debug.Log(this.name + " has taken " + damage + " damage!");
        if(currentHealth <= 0f) // When the health reaches zero or lower
        {
            Die(); // The entity will die in a certain way
        }
    }

    virtual public void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        // Make the entity dissappear 
        // Make something specific in how this entity will die
    }
}
