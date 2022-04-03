using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public List<CollectableController> speedBoost = new List<CollectableController>();
     public List<HealthController> healthBoost = new List<HealthController>();


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game started");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Add more to Later !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public void RestartLevel() {
        for (int i = 0; i < speedBoost.Count; i++) {
            speedBoost[i].Reset();
        }

         for (int i = 0; i < healthBoost.Count; i++) {
            healthBoost[i].Reset();
        }

    }

    // Create method for the collectable power up
    public void AddCollectable(CollectableController collectableController) {
        speedBoost.Add(collectableController);
    } 


     public void AddHealth(HealthController healthConroller) {
        healthBoost.Add(healthConroller);
    } 
}
