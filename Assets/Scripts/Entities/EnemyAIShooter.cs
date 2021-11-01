using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIShooter : EnemyAI
{
    // [] Make a stop feature every time entity attacks for the Enemy AI

    // [Header("Features")]
    //public bool pauseMovement = false;

    [Header("Attack Variables")]
    public float fireRate = 1f;
    private float nextFire = 0f;
    public float damage = 10f;
    public float midHeight = 1f; //Used to check if the player is at a certain height so the enemy can aim up

    [Header("Attack Properties")]
    public GameObject projectilePrefab;
    public Transform AttackPoint;
    public Transform AttackTransformParent;

    private bool AttackIsReady = false;

    public override void Attack()
    {
        base.Attack();

        if (commenceAttack != false && player != null)
        {
            AttackPlayer(commenceAttack);
        }
        else if (commenceAttack == false && player != null)
        {
            AttackPlayer(commenceAttack);
        }
    }

    bool lookUp = false;

    private void FixedUpdate()
    {
        //The height of the player
        float otherHeight = player.transform.position.y;
        //The height that adds in this position added with a height amount
        float centerHeight = transform.position.y + midHeight;
  
       if(otherHeight > centerHeight && !lookUp)
       {
            AttackTransformParent.Rotate(0, transform.rotation.y, 45f);
            lookUp = !lookUp;
            print("Looking up");
       }
       else if(otherHeight < centerHeight && lookUp)
        {
            AttackTransformParent.Rotate(0, transform.rotation.y, -45f);
            lookUp = !lookUp;
            print("Looking front");
        }
    }

    void AttackPlayer(bool IsAttacking)
    {
        AttackIsReady = IsAttacking;

        // Shooter type to shoot projectiles
        if (IsAttacking != false)
        {
            StartCoroutine(prepareToAttack(nextFire));

            if (AttackIsReady == true && Time.time > nextFire)
            {
                // Pauses the enemy movement

                // if (pauseMovement == true)
                // {
                //      enemy.CompleteStop = true;
                //  }

                nextFire = Time.time + fireRate;

                // Spawns a projectile
                GameObject projectileObject = Instantiate(projectilePrefab, AttackPoint.position, Quaternion.identity);
                var projectile = projectileObject.GetComponent<EnemyProjectile>();
                Vector3 shootDir = -(AttackTransformParent.position - AttackPoint.position).normalized;
                if (projectile != null)
                {
                    projectile.setup(shootDir);
                    //Gives the projectile damage
                    projectile.projectileDamage = damage;
                    //Debug.Log("Added damage to projectile!");
                }
                //[]Spawn a projectile and paste in damage variable of this script
                // -- Use instatiate
                AttackIsReady = false;
            }
        }
    }

    IEnumerator prepareToAttack(float attackDuration)
    {
        yield return new WaitForSeconds(attackDuration);
        AttackIsReady = true;
        StopAllCoroutines();
    }

}
