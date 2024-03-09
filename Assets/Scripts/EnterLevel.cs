using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLevel : MonoBehaviour
{
    private bool enterAllowed;
    private string sceneToLoad;
   

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "Player" && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("StarterRoom")) 
        {
            sceneToLoad = "Room1";
            enterAllowed = true;
        }
        // else if (collider.tag == "Player") 
        // {
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        // }

        // else if (collision.GetComponent<LevelDoor>())
        // {
        //     sceneToLoad = "Room1";
        //     enterAllowed = true;
        // }
    }
    
    private void OnTriggerExit2D(Collider2D collider) 
    {
        // if (collider.GetComponent<StoreDoor>() || collision.GetComponent<LevelDoor>()) 
        if (collider.tag == "Player")
        {
            enterAllowed = false;
        }
    }
    // // Update is called once per frame
    private void Update()
    {
        if (enterAllowed && Input.GetKey(KeyCode.E)) 
        //if (Input.GetKey(KeyCode.E)) 
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
