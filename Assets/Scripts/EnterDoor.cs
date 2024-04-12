using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
    public bool enterAllowed;
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public SpawnManager test; 

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "Player" && SceneManager.GetActiveScene().name == "StarterRoom" || SceneManager.GetActiveScene().name == "Store") 
        {
            //SceneManager.LoadScene(sceneToLoad);
            enterAllowed = true;
            Debug.Log("Player is in " + SceneManager.GetActiveScene());
        }
        else if (collider.tag == "Player" && test.numOfEnemies == 0) 
        {
            //SceneManager.LoadScene(sceneToLoad);
            enterAllowed = true;
            Debug.Log("Player killed all enemies");
        }
        if (test.numOfEnemies > 0) {
            enterAllowed = false;
            Debug.Log("Door locked until conditions met");
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
        if (enterAllowed && Input.GetKey(KeyCode.E) && test.numOfEnemies == 0)
        //if (Input.GetKey(KeyCode.E)) 
        {
            playerStorage.initialValue = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
            Debug.Log("All conditions met");
        }
        //Debug.Log("there are " + test.numOfEnemies + " enemies");  
    }
}
