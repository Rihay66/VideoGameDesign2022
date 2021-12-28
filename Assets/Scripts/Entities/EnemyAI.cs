using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    [Header("Parameters")]
    public float movementSpeed = 6f;
    public float minDistance = 2f;
    public float maxDistance = 10f;
    public float maxHeight = 1.5f;
    public GameObject exclamationObject; // Temporary visual object (Use to show if the enemy is detecting the player within out side max distance and vice versa)

    [HideInInspector]
    public bool move = false;

    [HideInInspector]
    public bool commenceAttack;  // 'Get' means to use as reference and 'private set' means it can be changed only inside this script

    [Header("Rage Values")]
    //values for the purpose of returning to normal values after rage
    [HideInInspector]
    public float originalMoveSpeed; 
    [HideInInspector]
    public float originalDamage;

    public float rageModifier = 1.3f;
    public float maxTimeToRage;
    [HideInInspector]
    //time to rage is kept as an int for the purpose of using system threading sleep
    public float timeToRage;

    [Header("Enemy Class")]
    public bool Melee;
    public bool Shooter;
    [Header("Enemy Abilities")]
    public bool Enraged;

    public bool rage { get; private set; } = false;

    private void OnValidate()
    {
        if(Melee && Shooter)
        {
            Melee = false;
            Shooter = false;
            Debug.LogWarning("Enemy set to more than one class, make sure it's set one class!");
            return;
        }else if(!Melee && !Shooter)
        {
            Debug.LogWarning("Enemy is not set to a class");
        }
    }

    public Rigidbody2D rb { get; private set; }

    [HideInInspector]
    public GameObject player { get; private set; }

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        exclamationObject.SetActive(false);
        originalMoveSpeed = movementSpeed;
        timeToRage = Random.Range(30f, maxTimeToRage);
    }

    void CheckForPhysics()
    {
        if (rb == null)
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
            Debug.LogWarning(this.name + " is not getting the rigidbody correctly");
            return;
        }
    }

    void FixedUpdate()
    {
        CheckForPhysics();
        if(player != null)
        {
            //Checks for the players distance from the enemy
            CheckDistance();
            //Moves the Entity
            Movement();
        }
        else
        {
            CheckForTarget();
        }
    }

    void CheckForTarget()
    {
        //Checks for the playerManager and sets the player as the target for the enemy
        if (PlayerManager.instance.playerInstance != null)
        {
            player = PlayerManager.instance.playerInstance;
            if(Enraged == true)
            {
                StartCoroutine(Rage());
                print("Calling rage");
            }
        }
        else
        {
            // If there isn't a PlayerManager it will give an error
            Debug.LogError(PlayerManager.instance.name + " Is either not found or its missing!");
            return;
        }
    }

    public virtual void CheckDistance()
    {
        // Meant to be overwritten
        // Specify what the Enemy does depending on the player's distance
        //Specifies to check the distance on the x-axis
        Vector2 self = new Vector2(transform.position.x, 0);
        Vector2 other = new Vector2(player.transform.position.x, 0);

        float pos = Vector2.Distance(self, other);

        //Specifies to check the height between entities
        float selfHeight = transform.position.y + maxHeight;
        float otherHeight = player.transform.position.y;

        // When the player is on the same height or below the enemy
        if (pos >= maxDistance || selfHeight <= otherHeight) //When player is on top or above the enemy
        {
            exclamationObject.SetActive(false);
            move = false; // Stops moving when player is outside the max distance
            commenceAttack = false;
            //transform.Translate(Vector2.zero);
        }
        else
        {
            if (player.activeSelf == false)
            {
                move = false; // When player is set to false the enemy will stop 
                commenceAttack = false;
                //transform.Translate(Vector2.zero);
            }
        }
    }

    // Might need some rework to make use of Rigidbody velocity or other

    //[] Make a temporary movement pause feature
    [HideInInspector]
    public bool faceRight = true;

    public void FaceTarget()
    {
        if(player != null)
        {
            if(transform.position.x < player.transform.position.x && !faceRight)
            {
                // Debug.Log("Player is in front of the enemy");
                //transform.localScale = new Vector3(1f, 1f, 1f);
                faceRight = !faceRight;

                transform.Rotate(0f, 180f, 0f);

            }else if(transform.position.x > player.transform.position.x && faceRight)
            {
                //Debug.Log("PLayer is in behind the enemy");
                //transform.localScale = new Vector3(-1f, 1f, 1f);
                faceRight = !faceRight;

                transform.Rotate(0f, 180f, 0f);
            }
        }
    }

    void Movement()
    {
        //Vector2 targetPos = new Vector2(player.transform.position.x, rb.position.y); // Checks for the position of the player
        if (move == true)
        {
            //Movement
            //transform.position = Vector2.MoveTowards(transform.position, targetPos, movementSpeed * Time.deltaTime);

            if (transform.position.x < player.transform.position.x)
            {
                //move right
                rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
            }
            else if (transform.position.x > player.transform.position.x)
            {
                //move left
                rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
            }

        }
    }

    public virtual void Attack()
    {
        // Shoot or do melee attacks
        // This is meant to be overwritten
       // if (player.activeSelf)
      //  {
      //      Debug.Log("Attacking " + player.name);
       // }
    }
    public virtual void RageModifierVoid()
    {
        //mod rage...
        Debug.Log("Modifying the enemy!");
    }
    IEnumerator Rage()
    {
        yield return new WaitForSeconds(timeToRage);
        Debug.Log(this.name + " is enraged!");
        rage = true;
        RageModifierVoid();
        // stopping the coroutine
        StopCoroutine(Rage());
    }


}