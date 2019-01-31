using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour {

    [SerializeField]
    List<AudioClip> BreakBlockSounds;

    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public void PlaySoundBreakBlock() {
        audioSource.clip = BreakBlockSounds[Random.Range(0, BreakBlockSounds.Count-1)];
        Debug.Log(BreakBlockSounds.Count);
        audioSource.Play();
    }
}
