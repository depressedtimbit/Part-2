using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoorTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    SceneLoader sceneLoader; 
    public GameObject sceneLoaderObject;
    
    void Start() 
    {
        sceneLoader = sceneLoaderObject.GetComponent<SceneLoader>();
    }
    void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            sceneLoader.LoadNextScene();
        }
    }
}
