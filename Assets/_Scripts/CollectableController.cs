using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Game gameControl;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameControl.AddCollectable(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset() {
        spriteRenderer.enabled = true;
    }

    // If the player touches the power up
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            Collected();
        }
    }

    // Counts that the power up has been collected
    void Collected() {
        spriteRenderer.enabled = false;
    }
}
