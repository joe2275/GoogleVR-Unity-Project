using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {
    private MagneticClicker mag;
    public Transform boxTransform;
	// Use this for initialization
	void Start () {
        mag = new MagneticClicker();
        mag.init();
	}
	
	// Update is called once per frame
	void Update () {
        mag.magUpdate(Input.acceleration, Input.compass.rawVector);
        if(mag.clicked())
        {
            boxTransform.Rotate(new Vector3(0.0f, 45.0f, 0.0f), Space.World);
        }
	}
}
