using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCalculator : MonoBehaviour
{
    // defining the variables to be able to grab players health and adjust health bar size accordingly
    public GameObject player;
    public float health;
    public GameObject healthBar;
    public Vector3 healthBarSize;

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning(health);
    }

    // Update is called once per frame
    void Update()
    {
        HealthProcedure();
     
    }
    void HealthProcedure()
    {
        //grabbing hp value
        health = player.GetComponent<EntityStats>().currentHealth;

        //changing hp value
        healthBarSize = new Vector3(health * .01f, .9f, 1);

        //if statement used to change health bar size
        if (health <= 100)
        {
            healthBar.transform.localScale = healthBarSize;

        }
    }
}
