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

    // void start for the purpose of assigning rage values
    private void Start()
    {
        // assigning rage values
        originalDamage = damage;
    }
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

    public override void CheckDistance()
    {
        base.CheckDistance();
        Vector2 self = new Vector2(transform.position.x, 0);
        Vector2 other = new Vector2(player.transform.position.x, 0);

        float pos = Vector2.Distance(self, other);

        //Specifies to check the height between entities
        float selfHeight = transform.position.y + maxHeight;
        float otherHeight = player.transform.position.y;
        // When the player is on the same height or below the enemy

        if (pos <= maxDistance && selfHeight >= otherHeight)
        {
            exclamationObject.SetActive(true);
            FaceTarget();
            if (pos >= minDistance)
            {
                //Stop moving
                // Attack from close 
                if (Shooter && !Melee)
                {
                    // Attack from distance
                    commenceAttack = true;
                    Attack();
                }
                move = true;
                //transform.Translate(Vector3.zero);
            }
            else
            {
                if (!Shooter && Melee)
                {
                    commenceAttack = true;
                    Attack();
                }
                move = false;
            }
        }
    }

    void LateUpdate()
    {
        RageChecker();
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
    void RageChecker()
    {
        if (!Enraged && !rage)
        {           
            damage = originalDamage;
            movementSpeed = originalMoveSpeed;
        }
    }
    public override void RageModifierVoid()
    {
        base.RageModifierVoid();
        if (rage)
        {
            damage *= rageModifier;
            movementSpeed *= rageModifier;
        }
    }

    IEnumerator prepareToAttack(float attackDuration)
    {
        yield return new WaitForSeconds(attackDuration);
        AttackIsReady = true;
        StopAllCoroutines();
    }
}
