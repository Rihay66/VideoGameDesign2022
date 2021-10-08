using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAIMelee : EnemyAI
{
    // [] Make a stop feature every time entity attacks for the Enemy AI

    // [Header("Features")]
    //public bool pauseMovement = false;

    [Header("Attack Variables")]
    public float fireRate = 1f;
    private float nextFire = 0f;
    public float damage = 10f;
    public float AttackRange = 1.5f;

    private bool AttackIsReady = false;

   // private EnemyAI enemy;

    public override void Attack()
    {
        base.Attack();

        if (commenceAttack != false && player != null)
        {
            AttackPlayer(commenceAttack);
        } else if (commenceAttack == false && player != null)
        {
            AttackPlayer(commenceAttack);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

    void AttackPlayer(bool IsAttacking)
    {
        AttackIsReady = IsAttacking;

        if (Vector3.Distance(transform.position, player.transform.position) < AttackRange)
        {
            // Melee type of enemy that do area attacks
            if (IsAttacking != false)
            {
                StartCoroutine(prepareToAttack(fireRate));

                if (AttackIsReady == true && Time.time > nextFire)
                {
                    // Pauses the enemy movement

                    // if (pauseMovement == true)
                    // {
                    //      enemy.CompleteStop = true;
                    //  }

                    nextFire = Time.time + fireRate;

                    // Within attack range
                    EntityStats health = player.GetComponent<EntityStats>();
                    if(health != null)
                    {
                        // Deal damage to the player
                        health.TakeDamage(damage);
                        Debug.Log(player.name + " took " + damage + " damage");

                        AttackIsReady = false;
                    }
                }
            }
        }
        else
        {
            //Resets the attacking cycle
            StopAllCoroutines();
            AttackIsReady = false;
        }
    }

    IEnumerator prepareToAttack(float attackDuration)
    {
        yield return new WaitForSeconds(attackDuration);
        AttackIsReady = true;
        StopAllCoroutines();
    }
}
