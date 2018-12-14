using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagneticInput_MainMenu : MonoBehaviour {

    private MagneticController magneticController;

    private GvrEventExecutor eventExecutor;

    private void Awake()
    {
        Application.targetFrameRate = 120;
        magneticController = new MagneticController();
    }

    private void Start()
    {
        eventExecutor = GvrPointerInputModule.FindEventExecutor();
    }

    private void Update()
    {
        if(magneticController.CheckMagneticSensor())
        {
            
        }
    }
}
