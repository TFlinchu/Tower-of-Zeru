using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    //public GameObject playerPrefab;
    GameObject playerInstance;
    public VectorValue playerStorage;
    public Vector2 playerPosition;
    //private Vector2 startPos;

    // Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        // startPos.x += 3.35f;
        // startPos.y -= 6.5f;
        //startPos = transform.position;
        //SpawnPlayer();
        //startPos = transform.position;
        //transform.position = startPos.initialValue(0,0);
        // SpawnPlayer();
        // playerInstance = Instantiate(playerPrefab); 
        // playerPrefab.transform.position = new Vector2(0,0);
       
    }


    // Update is called once per frame
    // void Update()
    // {
    // }

    // void SpawnPlayer() {
        // startPos.x += 3.35f;
        // startPos.y -= 6.5f;
        // startPos = transform.position;
        // playerInstance = Instantiate(playerPrefab); 
        // playerPrefab.transform.position = new Vector2(0,0);
        //startPos = new Vector2(0,0);
        //playerInstance = (GameObject)Instantiate(playerPrefab, transform.startPos, Quaternion.identity);
    // }
}
