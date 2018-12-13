using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagneticInput_MainMenu : MonoBehaviour {

    private MagneticController magneticController;
    [SerializeField]
    private GvrReticlePointer reticlePointer;

    private void Awake()
    {
        Application.targetFrameRate = 120;
        magneticController = new MagneticController();
    }

    private void Update()
    {
        if(magneticController.CheckMagneticSensor())
        {
            
        }
    }
}
