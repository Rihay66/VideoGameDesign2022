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
        //Specifies to check the distance on the x-axis
        Vector2 self = new Vector2(transform.position.x, 0);
        Vector2 other = new Vector2(player.transform.position.x, 0);

        float pos = Vector2.Distance(self, other);
  
        //Specifies to check the height between entities
        float selfHeight = transform.position.y + maxHeight;
        float otherHeight = player.transform.position.y;
        float centerHeight = transform.position.y + midHeight;
  
       if(otherHeight > centerHeight && !lookUp)
       {
            AttackTransformParent.Rotate(0, transform.rotation.y, 45f);
            lookUp = !lookUp;
            print("Looking up");
       }
       else if(otherHeight < centerHeight && lookUp)
        {
            //[] Figure out how to move the parent back to zero on the Z-axis while also keeping the transform rotation
            AttackTransformParent.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
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
