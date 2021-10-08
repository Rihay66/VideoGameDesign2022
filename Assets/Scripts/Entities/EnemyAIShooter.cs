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
                GameObject projectileObject = Instantiate(projectilePrefab, AttackPoint.position, AttackPoint.rotation);
                var projectile = projectileObject.GetComponent<EnemyProjectile>();
                if (projectile != null)
                {
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
