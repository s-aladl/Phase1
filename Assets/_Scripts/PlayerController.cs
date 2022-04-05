using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
      private int health=10;
    public bool active = true;
public TextMeshProUGUI healthText;

    float moveInput = 0;
    public int speed = 5;
    public int jumpForce = 6;

    public bool isGrounded = false;
    public bool isRight = true;

    public bool instantDefeat = false;

    // Checks to see if character is jumping or falling
    public bool isJumping = false;
    public bool isFalling = false;
    public float lastYpos = 0;
    public enum Animations {
        Idle = 0,
        Jumping = 1,
        Falling = 2,
        Walking = 3,
    }
    public bool respawn = false;
    public float timetorespawn = 2f;
    public float currentRespawnTime = 0;
public Vector2 startPos;
    public Animations currentAnim;
    public Animator anim;
    public LayerMask Ground;
    public Rigidbody2D squib;
    public SpriteRenderer rend;
    public bool jumpNow = false;

    // Start is called before the first frame update
    void Start()
    {


        startPos = transform.position;
        active = true;
        squib = GetComponent<Rigidbody2D>();

        currentAnim = Animations.Idle;
        anim = GetComponent<Animator>();
        ChangeAnimation(Animations.Idle);

        //squib = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        lastYpos = transform.position.y;

        SetHealthText();
    }

    // Update is called once per frame
    void Update()
    {
        if(respawn)
        {
            currentRespawnTime += Time.deltaTime;
            if (currentRespawnTime >= timetorespawn)
            {
                currentRespawnTime = 0;
                respawn = false;
                RespawnPlayer();
            }
        }

        if(!active)
        {
            return;
        }

        if (transform.position.y < -2)
        {
            SceneManager.LoadScene("Level1");
        }


        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpNow = true;
        }
    }

    void FixedUpdate()
    {
        // Makes the character jump
        if(jumpNow)
        {
            jumpNow = false;
            isGrounded = false;
            squib.velocity = Vector2.up * jumpForce;
        }
        else
        {
            isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f), 
            new Vector2(transform.position.x + 0.5f, transform.position.y - 0.51f), Ground);
        }

        // Checks to see if the chracter is on the ground
        if (transform.position.y > lastYpos && !isGrounded) {
            isJumping = true;
            isFalling = false;
            ChangeAnimation(Animations.Jumping);
        } else if (transform.position.y < lastYpos && !isGrounded) {
            isJumping = false;
            isFalling = true;
            ChangeAnimation(Animations.Falling);
        } else {
            isJumping = false;
            isFalling = false;
            ChangeAnimation(Animations.Idle);
        }

        lastYpos = transform.position.y;

        moveInput = Input.GetAxisRaw("Horizontal");

        // Flip character direction if either left or right
        if(moveInput == 1) {
            isRight = true;
        } else if (moveInput == -1) {
            isRight = false;
        } 

        rend.flipX = !isRight;
        

        
        squib.velocity = new Vector2(moveInput*speed,squib.velocity.y);
    }

    public void ChangeAnimation(Animations newAnim) {
        // Only changes the animation if not being currently used
        if (currentAnim != newAnim) {
            // Update current animation
            currentAnim = newAnim;
            // Change state variable to update the integer count
            anim.SetInteger("state", (int)currentAnim);
        }
    }

    /*****************************************************************************/
    // If the player touches the power up for speed boost
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Collectable") {
            SpeedUp();
            // Change look of character with the power up
            ChangeAnimation(Animations.Idle);
        }

        if (collision.tag == "Collectable2") {
            jumpUp();
        }
    }
    
    // Change the speed of the player
    void SpeedUp() {
        speed += 3;
    }

    // Change the jump speed of the player
    void jumpUp() {
        jumpForce += 3;
    }


     public void PlayerHit(int damage)
    {
        health = health- damage;
        SetHealthText();
        if(health<=0)
        {
            PlayerDefeated();

        }
    }

    public void PlayerDefeated()
    {
        active = false;
        squib.isKinematic = true;
        squib.velocity = Vector2.zero;
        respawn = true;

    }
     public void RespawnPlayer()
    {
        active = true; 
        squib.isKinematic = false;
        transform.position = startPos;
        health=10;
        SetHealthText();
    }

     void SetHealthText()
	{
		healthText.text = "Health: " + health.ToString();//keeps track of point count
	
		
		
	}
}
