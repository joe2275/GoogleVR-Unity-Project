using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoorEventTrigger : MonoBehaviour {

    private Animator doorAnimator;
    private bool isFocused;

    private void Awake()
    {
        doorAnimator = GetComponent<Animator>();
        isFocused = false;
    }

    public void Focus(bool isFocused)
    {
        this.isFocused = isFocused;
        doorAnimator.SetBool("focused", isFocused);
    }

    public void Click()
    {
        if(isFocused)
        {
            doorAnimator.SetTrigger("clicked");
            // Show Animation and Load Scene
        }
    }
}
