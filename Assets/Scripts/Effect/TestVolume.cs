using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVolume : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip shootBullet;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayShootBulletSound()
    {
        audioSource.volume = AudioManager.instance.fXVolume;
        audioSource.PlayOneShot(shootBullet);
    }
}
