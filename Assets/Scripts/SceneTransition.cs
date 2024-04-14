using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // private bool enterAllowed;
    public string sceneToLoad;  //changed from private
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start() {
        audioManager.PlaySFX(audioManager.storeSound);
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger) {
            playerStorage.initialValue = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
   

    // private void OnTriggerEnter2D(Collider2D collider) 
    // {
    //     if (collider.tag == "Player" && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("StarterRoom")) 
    //     {
    //         sceneToLoad = "Store";
    //         enterAllowed = true;
    //     }
    //     else if (collider.tag == "Player" && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Store")) 
    //     {
    //         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            
    //     }

    //     // else if (collision.GetComponent<LevelDoor>())
    //     // {
    //     //     sceneToLoad = "Room1";
    //     //     enterAllowed = true;
    //     // }
    // }
    
    // private void OnTriggerExit2D(Collider2D collider) 
    // {
    //     // if (collider.GetComponent<StoreDoor>() || collision.GetComponent<LevelDoor>()) 
    //     if (collider.tag == "Player")
    //     {
    //         enterAllowed = false;
    //     }
    // }
    // // // Update is called once per frame
    // private void Update()
    // {
    //     if (enterAllowed && Input.GetKey(KeyCode.E)) 
    //     if (Input.GetKey(KeyCode.E)) 
    //     {
    //         SceneManager.LoadScene(sceneToLoad);
    //     }
    // }
}
