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
    public AudioClip skeletonDeath;
    public AudioClip skeletonDamage;
    public AudioClip skeletonDamage2;
    public AudioClip skeletonDamage3;
    public AudioClip playerDamage;
    public AudioClip itemSound;
    public AudioClip inventorySound;
    public AudioClip storeMusic;
    public AudioClip backgroundMusic;



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
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip) {
        SFXSource.PlayOneShot(clip);
    }


}
