using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartianController : MonoBehaviour
{

    public Vector2[] coords;

    // Set path and speed variables
    public int path = 0;
    public float speed = 2;

    // Set variables to track enemy movement
    float moveInput = 0;
    public bool isRight = true;
    public SpriteRenderer rend;
    //public Rigidbody2D martian;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        //martian = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Takes current position value and moves toward the next position
        transform.position = Vector2.MoveTowards(transform.position, coords[path], speed * Time.deltaTime);

        if (transform.position.y < -1)
        {
            Destroy(this.gameObject);
        }

        if(transform.position.x == coords[path].x && transform.position.y == coords[path].y) {
            path++;

            // Resets list to first location (to make it move back and forth)
            if(path >= coords.Length) 
            {
                path = 0;
            }
        }
    }


    /*******************************************************************/
    // Change direction of the enemy
    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        // Flip character direction if either left or right
        if(moveInput == 1) {
            isRight = true;
        } else if (moveInput == -1) {
            isRight = false;
        } 

        rend.flipX = !isRight;

        //martian.velocity = new Vector2(moveInput*speed,martian.velocity.y);
    }



}
