using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayEventTrigger : MonoBehaviour {
    private bool isFocused;
    private MagneticController magneticController;

    private void Awake()
    {
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
    }

    public void Click()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
