using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCalculator : MonoBehaviour
{
    // defining the variables to be able to grab players health and adjust health bar size accordingly
    public GameObject player;
    private float health;
    private Vector3 healthBarSize;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.LogWarning(health);
        if (PlayerManager.instance.playerInstance != null)
        {
            player = PlayerManager.instance.playerInstance;
        }
        else
        {
            // If there isn't a PlayerManager it will give an error
            Debug.LogError(PlayerManager.instance.name + " Is either not found or its missing!");
            return;
        }
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

        //if statement used to change health bar size
        if (health <= 100)
        {
            //changing hp value
            gameObject.transform.localScale = new Vector3(health * .01f, 0.9f, 1);
        }
    }
}
