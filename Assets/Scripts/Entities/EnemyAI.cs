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
    public float midHeight = 0.75f;
    public GameObject exclamationObject; // Temporary visual object (Use to show if the enemy is detecting the player within out side max distance and vice versa)

    bool move = false;
    public bool commenceAttack { get; private set; } // 'Get' means to use as reference and 'private set' means it can be changed only inside this script

    [Header("Enemy Class")]
    public bool Melee;
    public bool Shooter;

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

    private Rigidbody2D rb;

    [HideInInspector]
    public GameObject player { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        exclamationObject.SetActive(false);
        CheckForTarget();
        if(!Shooter && !Melee)
        {
            Debug.LogError(this.gameObject.name + "'s class hasn't been set!");
            return;
        }
    }

    private void Update()
    {   
        //Checks for the players distance from the enemy
        CheckDistance();
        //Moves the Entity
        Movement();
    }

    void CheckForTarget()
    {
        //Checks for the playerManager and sets the player as the target for the enemy
        if (PlayerManager.instance.playerInstance != null)
        {
            player = PlayerManager.instance.playerInstance;
        }
        else
        {
            // If there isn't a PlayerManager it will give an error
            Debug.LogError(PlayerManager.instance.name + " Is either not found or its missing!");
        }
    }

    void CheckDistance()
    {
        //Specifies to check the distance on the x-axis
        Vector2 self = new Vector2(transform.position.x, 0);
        Vector2 other = new Vector2(player.transform.position.x, 0);

        float pos = Vector2.Distance(self, other);

        //Specifies to check the height between entities
        float selfHeight = transform.position.y + maxHeight;
        float otherHeight = player.transform.position.y;
        // When the player is on the same height or below the enemy

        if (pos <= maxDistance && selfHeight >= otherHeight && player != null) 
        {
            exclamationObject.SetActive(true);
            FaceTarget();
            // Attack from distance
            if(Shooter && !Melee)
            {
                // []Check if the player is above the enemy at a certain height make the reference transform object inside the enemy rotate only 45 degrees 
                // []The referene is only valid when the enemy is set to a shooter class
                commenceAttack = true;
                Attack();
            }
            if (pos <= minDistance)
            {
                //Stop moving
                // Attack from close 
                if(!Shooter && Melee)
                {
                    commenceAttack = true;
                    Attack();
                }
                move = false;
                transform.Translate(Vector3.zero);
            }
            else
            {
                if (pos >= minDistance)
                {
                    //Keep moving
                    move = true;
                }
            }
        }
        else
        {
            if (pos >= maxDistance || selfHeight <= otherHeight) //When player is on top or above the enemy
            {
                exclamationObject.SetActive(false);
                move = false; // Stops moving when player is outside the max distance
                commenceAttack = false;
                transform.Translate(Vector3.zero);
            }
            else
            {
                if (player.activeSelf == false)
                {
                    move = false; // When player is set to false the enemy will stop 
                    commenceAttack = false;
                    transform.Translate(Vector3.zero);
                }
            }

        }
    }

    // Might need some rework to make use of Rigidbody velocity or other

    //[] Make a temporary movement pause feature
    [HideInInspector]
    public bool faceRight = true;

    void FaceTarget()
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
        Vector2 targetPos = new Vector2(player.transform.position.x, rb.position.y); // Checks for the position of the player
        if (move == true)
        {
            //Movement
            transform.position = Vector2.MoveTowards(transform.position, targetPos, movementSpeed * Time.deltaTime);
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
}

