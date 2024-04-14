using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start() {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene ==  SceneManager.GetSceneByName ("Menu")) {
            audioManager.PlayMenuMusic();
        }
       // menuMusicManager.PlayMenuMusic();
        // else if (currentScene == SceneManager.GetSceneByName ("StarterRoom")) {
        //     audioManager.
        // }
        
    }
    public void startGame() {
        //SceneManager.LoadScene(1);
        audioManager.PlaySFX(audioManager.buttonPressed);
        StartCoroutine(DelaySceneLoad());
    }

    public void quitGame() {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    IEnumerator DelaySceneLoad()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }
}
