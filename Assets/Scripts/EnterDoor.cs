using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
    private bool enterAllowed;
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
   

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "Player") 
        {
            //SceneManager.LoadScene(sceneToLoad);
            enterAllowed = true;
        }
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
            playerStorage.initialValue = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
