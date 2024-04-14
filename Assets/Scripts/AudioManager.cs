using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-----Audio Source-----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-----Audio Clip-----")]
    public AudioClip doorSound;
    public AudioClip spellSound;
    public AudioClip buttonPressed;
    public AudioClip menuMusic;
    public AudioClip storeSound;
    public AudioClip doorSound2;

    public AudioClip winSound;

    public static AudioManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
        

    private void Start() {
        //musicSource.clip = doorSound;
        //musicSource.Play();
    }

    public void PlayMenuMusic() {
        musicSource.clip = menuMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip) {
        SFXSource.PlayOneShot(clip);
    }

}