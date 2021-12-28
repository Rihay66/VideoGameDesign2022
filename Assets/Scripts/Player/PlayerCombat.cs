using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //initalizing variables necessary for combat

    public float shotCooldown = 1f;
    private float isFiring;

    public GameObject projectile;
    public GameObject player;

    public Transform attackPoint;
    [HideInInspector]
    public float damage = 10f;



    // Start is called before the first frame update
    void Start()
    {
        // will be necessary for actually being able to fire lyre

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // putting in voids so they get called
        ShotTimer();
        FiringModule();
    }

    void FiringModule()
    {
        // control so it actually detects if it shoots
        isFiring = Input.GetAxis("Fire1");


        if (isFiring != 0 && shotCooldown <= 0)
        {
            //this is for player position detection


            GameObject projectileObject = Instantiate(projectile, attackPoint.position, player.transform.rotation);
            var projectileValues = projectileObject.GetComponent<PlayerProjectiles>();
            if (projectileValues != null)
            {
                projectileValues.damage = damage;
            }

            shotCooldown = 1f;
        }

    }
    void ShotTimer()
    {
        //countdown so user can't spam attacks
        if (shotCooldown >= 0)
        {
            shotCooldown -= Time.deltaTime;

        }
    }

}


