using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : EntityStats
{
    public GameObject healthBarObject;
    private HealthBar healthBar;

    private void Awake()
    {
        healthBar = healthBarObject.GetComponent<HealthBar>();
        healthBar.SetMaxHealth(MaxHealth);
    }

    private void LateUpdate()
    {
        healthBar.SetHealth(currentHealth);
    }

    public override void Die()
    {
        base.Die();
        // Destroys the object this scripts sits in
        Destroy(gameObject);
    }
}
