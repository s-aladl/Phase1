using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartianController : MonoBehaviour
{

    public Vector2[] coords;

    // Set path and speed variables
    public int path = 0;
    public float speed = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Takes current position value and moves toward the next position
        transform.position = Vector2.MoveTowards(transform.position, coords[path], speed = Time.deltaTime);

        if(transform.position.x == coords[path].x && transform.position.y == coords[path].y) {
            path++;

            // Resets list to first location (to make it move back and forth)
            if(path >= coords.Length) 
            {
                path = 0;
            }
        }
    }
}
