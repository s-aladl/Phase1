using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int iLevel;
    public string sLevel;

    public bool loadLevel = false;
    public bool collectedEvery = false;
    

    void Start()
    {
        
    }

    
    void Update()
    {
        //PlayerController.countSpaceJunk;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //GameObject collisionGameObject = collision.gameObject;

        if((collision.tag == "Player"))
        {
            LoadScene();
        }
    }

    void LoadScene()
    {
        if (loadLevel)
        {
            SceneManager.LoadScene(iLevel);
        } else
        {
            SceneManager.LoadScene(sLevel);
        }
    }
}
