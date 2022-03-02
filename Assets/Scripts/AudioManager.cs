using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip flyClip;
    public AudioClip pointClip;
    public AudioClip dieClip;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
    }
    public void PlayFly()
    {
        audioSource.clip = flyClip;
        audioSource.Play();
    }

    public void PlayDie()
    {
        audioSource.clip = dieClip;
        audioSource.Play();
    }

    public void PlayPoint()
    {
        audioSource.clip = pointClip;
        audioSource.Play();
    }
}

