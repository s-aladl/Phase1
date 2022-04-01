using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadDetectScript : MonoBehaviour
{
    GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        Enemy= gameObject.transform.parent.gameObject;
        
    }

   private void OnCollisionEnter2D(Collision2D collision) 
    {
        GetComponent<Collider2D>().enabled = false; 
        Enemy.GetComponent<SpriteRenderer>().flipY = true; 
        Enemy.GetComponent<Collider2D>().enabled = false; 
        Vector3 movement = new Vector3(Random.Range(-40,-70), Random.Range(-400,-500));
        Enemy.transform.position += movement * Time.deltaTime; 
    }
    
}
