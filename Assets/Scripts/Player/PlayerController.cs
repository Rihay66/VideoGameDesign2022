using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public bool playerControllerPause = true;
    //player for easy referencing
    private GameObject player;
    // defines the player speed publicly which can be changed by other scripts
    [HideInInspector]
    public float playerSpeed;

    private Rigidbody2D rb;
    [HideInInspector]
    public float jumpPower;

    // public variable since it will be modded alot
    private bool isJumping;

    // controls
    private float leftRight;
    private float jump;

    // move clamper
    private bool xMoving = false;

    // variable to tell whether or not user is left or right
    private float turnDirection = 1f;
    bool faceRight = true;
    //particles and other artsy things
    [Header("Particle Parameters")]
    public Transform playerFeet;

    private ParticleSystem dustOnJump;
    private ParticleSystem dustOnImpact;

    private ParticleMaster master;

    private bool playerDisabled = true;

    // Start is called before the first frame update
    void Start()
    {
        // control initalize
        StartCoroutine(enableThePlayer());
        rb = GetComponent<Rigidbody2D>();
        master = gameObject.GetComponent<ParticleMaster>();
        isJumping = false;
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
        if (master != null)
        {
            if (master.entity != gameObject)
            {
                master.setEntity(gameObject);
            }
        }
        else if (master == null)
        {
            Debug.LogError("No ParticleMaster script found on " + gameObject.name);
            return;
        }
        dustOnJump = Resources.Load<ParticleSystem>("Particles/dustOnJump");
        dustOnImpact = Resources.Load<ParticleSystem>("Particles/dustOnImpact");
        if(playerControllerPause == true)
        {
            StartCoroutine(enableThePlayer());
        }
        else
        {
            playerDisabled = false;
        }
    }

    IEnumerator enableThePlayer()
    {
        yield return new WaitForSeconds(20f);
        playerDisabled = false;
        StopAllCoroutines();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(playerDisabled != true)
        {
            //putting in controls so they can actively update
            leftRight = Input.GetAxis("Horizontal");
            jump = Input.GetAxis("Jump");
            //controls
            xControls();
            Jumping();
            MoveClamp();
            spriteMover();
            //sprite mover and bullet direction detector
            SpriteProjectileMover();
        }
    }
    private void xControls()
    {
        // asking when leftright be moving so can move player
        if (leftRight != 0)
        {
            rb.AddForce(playerSpeed * transform.right, ForceMode2D.Impulse);
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -playerSpeed, playerSpeed), rb.velocity.y);
            xMoving = true;
        }
        else
        // killing velocity
        {
            xMoving = false;
        }
        if (xMoving != true)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }
    private void Jumping()
    {
        //this is meant for jump controls for the player. 
        if (jump != 0 && isJumping == false)
        {
            //adding the force to properly jump
            rb.AddForce(Time.deltaTime * jumpPower * jump * transform.up, ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -jumpPower, jumpPower));
            isJumping = true;
            //particle master reference for dustOnJump effect
            if(master != null)
            {
                master.InstantiateParticle(dustOnJump, "movement", "dustOnJump", playerFeet);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //when the user hits the ground with the tag ground it will reset the jumps
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;

            if(master != null)
            {
                master.InstantiateParticle(dustOnImpact, "movement", "dustOnJump", playerFeet);
            }
        }
    }
    private void MoveClamp()
    {
        // this serves the purpose of forcing the player to be clamped to these speeds in order to not exceed the speeds set in the program.
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -playerSpeed, playerSpeed), rb.velocity.y);

        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -1000, 2*jumpPower));
    }
    private void SpriteProjectileMover()
    {

        //sprite mover and bullet direction detector
        if (leftRight > 0)
        {
            turnDirection = 1f;
        }
        else if (leftRight < 0)
        {
            turnDirection = -1f;
        }
    }
    private void spriteMover()
    {
  
        if (turnDirection == -1f && faceRight)
        {
            faceRight = !faceRight;
            transform.Rotate(0f, 180f, 0f);

        }
        else if (turnDirection == 1f && !faceRight)
        {
            faceRight = !faceRight;
            transform.Rotate(0f, 180f, 0f);

        }
    }
   
}
