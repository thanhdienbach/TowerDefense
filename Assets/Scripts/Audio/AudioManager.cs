using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region instance
    public static AudioManager instance;
    private void OnEnable()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public AudioClip gameMusic;
    public AudioClip menuMusic;
    public AudioSource audioSource;

    public float gameVolume;
    public float fXVolume;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Init(string sceneName)
    {
        
        if (sceneName == "MainMenuScene")
        {
            audioSource.clip = menuMusic;
        }
        else
        {
            audioSource.clip = gameMusic;
        }

        gameVolume = 0.8f;
        fXVolume = 0.8f;
        audioSource.volume = gameVolume;
        audioSource.Play();
    }

}
