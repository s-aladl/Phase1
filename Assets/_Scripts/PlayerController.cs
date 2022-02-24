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

    public LayerMask Ground;

    public Rigidbody2D squib;

    public SpriteRenderer rend;

    public bool jumpNow = false;

    // Start is called before the first frame update
    void Start()
    {
        squib = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
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
            isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f), new Vector2(transform.position.x + 0.5f, transform.position.y - 0.51f), Ground);
        }

        moveInput = Input.GetAxisRaw("Horizontal");

        // Flip character direction
        if(moveInput == 1) {
            isRight = true;
        } else if (moveInput == -1) {
            isRight = false;
        }

        rend.flipX = !isRight;
        

        
        squib.velocity = new Vector2(moveInput*speed,squib.velocity.y);
    }
}
