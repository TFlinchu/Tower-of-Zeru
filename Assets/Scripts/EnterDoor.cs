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
    bool canEnter;

    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" && GameManager.instance.IsRoomCleared(test.roomNumber))
        {
            //SceneManager.LoadScene(sceneToLoad);
            enterAllowed = true;
            //audioManager.PlaySFX(audioManager.doorSound);
        }
        if (test.totalEnemies > test.enemiesKilled && !GameManager.instance.IsRoomCleared(test.roomNumber))
        {
            enterAllowed = false;
            Debug.Log("Door locked until conditions met");
            //audioManager.PlaySFX(audioManager.doorSound);
        }
        Debug.Log(GameManager.instance.IsRoomCleared(test.roomNumber));
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
        if (test.totalEnemies <= test.enemiesKilled)
        {
            GameManager.instance.ClearRoom(test.roomNumber);
        }
        if (GameManager.instance.IsRoomCleared(test.roomNumber))
        {
            canEnter = true;
        } else
        {
            canEnter = false;
        }
        if (Input.GetKey(KeyCode.E) && enterAllowed && canEnter)
        {
            audioManager.PlaySFX(audioManager.doorSound2);
            playerStorage.initialValue = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
            Debug.Log("All conditions met");
        }
        //Debug.Log("there are " + test.numOfEnemies + " enemies");   
    }
}