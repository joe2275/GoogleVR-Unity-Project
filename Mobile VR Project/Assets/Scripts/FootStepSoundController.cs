using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootStepSoundController : MonoBehaviour {

    [SerializeField]
    private AudioClip[] footStepSounds;

    private AudioSource audioSource;

    private int index;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        index = 0;
    }

    public void PlayFootStepSound()
    {
        audioSource.clip = footStepSounds[index];
        audioSource.Play();
        index = (index + 1 >= footStepSounds.Length ? 0 : index + 1);
    }

}
