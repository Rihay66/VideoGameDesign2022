using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BossStats))]
public class MinotaurAI : EnemyAI
{
    [Header("Boss Parameters")]
    //variables for phase changes
    public float fireRate;
    private float nextFire;
    public float damage;
    public float CQCminDistance; //When the boss phase requires alternate min distance for melee attacks
    public float midHeight = 1f; //Used to check if the player is at a certain height so the enemy can aim up
    [Header("Boss Attack Properties")]
    public GameObject projectilePrefab;
    public GameObject areaAttackPrefab;
    public Transform AttackPoint;
    public Transform AttackTransformParent;
    private bool AttackIsReady;

    private BossStats stats;

    private void Awake()
    {
        stats = gameObject.GetComponent<BossStats>();
    }

    public override void Attack()
    {
        base.Attack();
        if (commenceAttack != false && player != null)
        {
            Phases(commenceAttack);
        }
        else if (commenceAttack == false && player != null)
        {
            Phases(commenceAttack);
        }
    }

    public override void CheckDistance()
    {
        base.CheckDistance();

        // variables for use of distance checking
        Vector2 self = new Vector2(transform.position.x, 0);
        Vector2 other = new Vector2(player.transform.position.x, 0);

        float pos = Vector2.Distance(self, other);
 
        if (pos <= maxDistance)
        {
            commenceAttack = true;
            Attack();
            if (pos > minDistance)
            {
                //keep moving
                move = true;
            }
            // Move backwards
            if (pos <= minDistance)
            {
                commenceAttack = true;
                Attack();
                //Stop Moving
                move = false;
                if(pos < minDistance - 2.2f)
                {
                    //move backwards
                    print("Player is near the boss!");
                    rb.velocity = new Vector2(-movementSpeed * 1.3f, rb.velocity.y);
                }
            }
        }

    }

    bool lookUp = false;

    void Update()
    {
        //for the purpose of aligning the AI to the player
        FaceTarget();
        if (player != null && stats.currentHealth >= stats.MaxHealth)
        {
            //The height of the player
            float otherHeight = player.transform.position.y;
            //The height that adds in this position added with a height amount
            float centerHeight = transform.position.y + midHeight;

            if (otherHeight > centerHeight && !lookUp)
            {
                AttackTransformParent.Rotate(0, transform.rotation.y, 35f);
                lookUp = !lookUp;
                print("Looking up");
            }
            else if (otherHeight < centerHeight && lookUp)
            {
                AttackTransformParent.Rotate(0, transform.rotation.y, -35f);
                lookUp = !lookUp;
                print("Looking front");
            }
        }
    }

    void Phases(bool IsAttacking)
    {
        if(stats != null)
        {
            if (stats.currentHealth >= stats.MaxHealth) //Initial phase
            {
                //Do a shooting attack
                phase1Attack(IsAttacking);
            }
            else if (stats.currentHealth < stats.MaxHealth / 2f)//Second phase
            {
                //Do a melee attack
                setParameters();
                phase2Attack(IsAttacking);
            }
        }
        else
        {
            Debug.LogError("No Enemy Stats Script found on " + this.name);
            return;
        }
    }

    bool setParameters()
    {
        if(stats.Armor < 1f)
        {
            stats.Armor += 10f;
            damage *= 1.5f;
            minDistance = CQCminDistance;
            Debug.Log(this.name + " is now in second phase");
            return false;
        }
        return true;
    }

    //Attack Phases
    void phase1Attack(bool IsAttacking)
    {
        //[] shooting at the player
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

    void phase2Attack(bool IsAttacking)
    {
        //melee phase
        //summoning area attack to hit player.
        if (IsAttacking != false)
        {
            StartCoroutine(prepareToAttack(nextFire));

            if (AttackIsReady == true && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;

                //Spawns in a area attack
                GameObject areaAttack = Instantiate(areaAttackPrefab, gameObject.transform.position, Quaternion.identity);
                var area = areaAttack.GetComponent<DamageArea>();
                if (area != null)
                {
                    area.areaDamage = damage;
                    Debug.Log("Area Attack");
                }
                AttackIsReady = false;
            }
        }
    }

    IEnumerator prepareToAttack(float attackDuration)
    {
        yield return new WaitForSeconds(attackDuration);
        AttackIsReady = true;
        StopCoroutine(prepareToAttack(0f));
    }
}

