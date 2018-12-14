using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticController : MonoBehaviour
{
    public static MagneticController instance = null;

    private const float MIN_CORRECTION_MAGNETIC_FORCE = 3.0f;
    private const float MAX_CORRECTION_MAGNETIC_FORCE = 25.0f;
    private const float THRESHOLD_MAGNETIC_FORCE = 1.1f;
    private const float MIN_CORRECTION_ACCELERATION = 5.0f;
    private const float THRESHOLD_ACCELERATION = 0.2f;

    private float normalizedMagnet;
    private float magnetForce;
    private float acceleration;
    private Vector3 prevAcceleration;

    private bool isChecked;

    private void Awake()
    {
        isChecked = false;

        Application.targetFrameRate = 120;
        Input.compass.enabled = true;
        prevAcceleration = Input.acceleration;
        magnetForce = Input.compass.rawVector.magnitude;
        acceleration = prevAcceleration.magnitude;
        normalizedMagnet = magnetForce;
        instance = this;
    }

    public bool CheckMagneticSensor()
    {
        Vector3 currentMagneticForce = Input.compass.rawVector;
        //Vector3 currentAcceleration = Input.acceleration - prevAcceleration;
        //prevAcceleration = Input.acceleration;

        magnetForce = (magnetForce * (MIN_CORRECTION_MAGNETIC_FORCE - 1) + currentMagneticForce.magnitude) / MIN_CORRECTION_MAGNETIC_FORCE;
        normalizedMagnet = (normalizedMagnet * (MAX_CORRECTION_MAGNETIC_FORCE - 1) + currentMagneticForce.magnitude) / MAX_CORRECTION_MAGNETIC_FORCE;

        //acceleration = (acceleration * (MIN_CORRECTION_ACCELERATION - 1) + currentAcceleration.magnitude) / MIN_CORRECTION_ACCELERATION;

        if (/*acceleration < THRESHOLD_ACCELERATION &&*/ magnetForce / normalizedMagnet > THRESHOLD_MAGNETIC_FORCE)
        {
            if (isChecked == false)
            {
                isChecked = true;
                return true;
            }
        }
        else
        {
            isChecked = false;
        }
        return false;
    }
}
