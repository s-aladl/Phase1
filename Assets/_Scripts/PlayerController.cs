using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float moveInput = 0;
    public int speed = 4;
    public int jumpForce = 4;

    public bool isGrounded = false;
    public bool isRight = true;

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
    public Animations currentAnim;
    public Animator anim;

    public LayerMask Ground;
    public Rigidbody2D squib;
    public SpriteRenderer rend;
    public bool jumpNow = false;

    // Start is called before the first frame update
    void Start()
    {
        currentAnim = Animations.Idle;
        anim = GetComponent<Animator>();
        ChangeAnimation(Animations.Idle);

        squib = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        lastYpos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
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
        }

        lastYpos = transform.position.y;

        moveInput = Input.GetAxisRaw("Horizontal");

        // Flip character direction
        if(moveInput == 1) {
            isRight = true;
        } else if (moveInput == -1) {
            isRight = false;
        } else if (moveInput == 0 && isGrounded) {
            ChangeAnimation(Animations.Idle);
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
}
