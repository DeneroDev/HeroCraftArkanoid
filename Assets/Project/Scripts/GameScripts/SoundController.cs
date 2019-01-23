using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour {

    [SerializeField]
    AudioClip BreakBlockSounds;

    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public void PlaySoundBreakBlock() {
        audioSource.clip = BreakBlockSounds;
        audioSource.Play();
    }
}
