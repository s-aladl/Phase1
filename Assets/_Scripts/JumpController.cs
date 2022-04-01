using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    public bool facingRight = false;
    public LayerMask Ground;

    public bool isGrounded = false;
    public bool isFalling = false;
    public bool isJumping = false;

    public float jumpForceX = 2f;
    public float jumpForceY = 4f;

    public float lastYPos = 0f;

    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    public float idleTime = 2f;
    public float currentIdleTime = 0;
    public bool isIdle = true;

    // Start is called before the first frame update
    void Start()
    {
        lastYPos = transform.position.y;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
         if (transform.position.y < -1)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        if(isIdle)
        {
            currentIdleTime += Time.deltaTime;
            if (currentIdleTime >= idleTime)
            {
                currentIdleTime = 0;
                Jump();
                facingRight = !facingRight;
                spriteRenderer.flipX = facingRight;
            }
        }
        
        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f), new Vector2(transform.position.x + 0.5f, transform.position.y - 0.51f), Ground);

        if (isGrounded && !isJumping)
        {
            isFalling = false;
            isJumping = false;
            isIdle = true;
        }
        else if (transform.position.y > lastYPos && !isGrounded && !isIdle)
        {
            isJumping = true;
            isFalling = false;
        }
        else if (transform.position.y < lastYPos && !isGrounded && !isIdle)
        {
            isJumping = false;
            isFalling = true;
        }
    }

    void Jump()
    {
        isJumping = true;
        isIdle = false;
        int direction = 0;
        if (facingRight)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        rb.velocity = new Vector2(jumpForceX * direction, jumpForceY);
    }
}
