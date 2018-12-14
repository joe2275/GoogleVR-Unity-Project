using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AudioSource))]
public class DoorEventTrigger : MonoBehaviour {

    private Animator doorAnimator;
    private AudioSource doorAudioSource;
    private bool isFocused;
    private MagneticController magneticController;

    [SerializeField]
    private AudioClip focusedDoorSound;
    [SerializeField]
    private AudioClip clickedDoorSound;

    private void Awake()
    {
        doorAnimator = GetComponent<Animator>();
        doorAudioSource = GetComponent<AudioSource>();
        isFocused = false;
    }
    private void Start()
    {
        magneticController = MagneticController.instance;
    }

    private void Update()
    {
        if(isFocused && magneticController.CheckMagneticSensor())
        {
            Click();
        }
    }

    public void Focus(bool isFocused)
    {
        this.isFocused = isFocused;
        doorAnimator.SetBool("focused", isFocused);
        doorAudioSource.clip = focusedDoorSound;
        doorAudioSource.Play();
    }

    public void Click()
    {
        doorAnimator.SetTrigger("clicked");
        doorAudioSource.clip = clickedDoorSound;
        doorAudioSource.Play();
    }

    public void AppQuit()
    {
        Application.Quit();
    }
}
