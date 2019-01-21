using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    [SerializeField]
    List<AudioClip> BreakBlockSounds;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundBreakSound() {
        var random = (int)Random.Range(0, 1.99f);
        audioSource.clip = BreakBlockSounds[random];
        audioSource.Play();
      
    }
}
