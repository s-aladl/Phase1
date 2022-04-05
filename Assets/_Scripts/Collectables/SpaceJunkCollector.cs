using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceJunkCollector : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Game gameControl;
    public int collectedJunk = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameControl.AddSpaceJunk(this);
        collectedJunk = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        spriteRenderer.enabled = true;
        
    }

    // If the player touches the power up
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Collected();
        }
    }

    // Counts that the power up has been collected
    void Collected()
    {
        collectedJunk += 1;
        
        spriteRenderer.enabled = false;
       
    }
}
