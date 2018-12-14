using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour {
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private Transform cameraChildTransform;
    [SerializeField]
    private Transform headTransform;

    [SerializeField]
    private Transform rotationSynchronizer;
    [SerializeField]
    private Animator playerAnimator;
    [SerializeField]
    private Rigidbody playerRb;
    private bool isStop;

    private MagneticController magneticController;


    private void Start()
    {
        isStop = false;
        magneticController = MagneticController.instance;
    }

    private void Update()
    {
        CheckCameraRotation();

        if (magneticController.CheckMagneticSensor() || Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Click();
        }

        if(!isStop)
        {
            playerRb.transform.Translate(new Vector3(0.0f, 0.0f, 0.005f), Space.World);
        }
    }

    public void CheckCameraRotation()
    {
        float cameraChild_x = cameraChildTransform.position.x - cameraTransform.position.x;
        float cameraChild_y = cameraChildTransform.position.y - cameraTransform.position.y;
        float cameraChild_z = cameraChildTransform.position.z - cameraTransform.position.z;
        transform.position = new Vector3(headTransform.position.x + cameraChild_x, headTransform.position.y + cameraChild_y, headTransform.position.z + cameraChild_z);
    }

    public void Click()
    {
        //Vector3 currentAngle = cameraTransform.eulerAngles;
        //rotationSynchronizer.eulerAngles = new Vector3(0.0f, 0.0f, cameraTransform.eulerAngles.y);
        //cameraTransform.eulerAngles = currentAngle;

        if(!isStop)
        {
            isStop = true;
            playerRb.isKinematic = true;
            playerAnimator.enabled = false;
        }
        else
        {
            isStop = false;
            StartCoroutine(RotateToAngle());
            playerRb.isKinematic = false;
            playerAnimator.enabled = true;
        }
    }

    public IEnumerator RotateToAngle()
    {
        Vector3 currentAngle = cameraTransform.eulerAngles;
        Vector3 currentSynchronizerAngle;

        if(currentAngle.z < -180.0f)
        {
            currentAngle = new Vector3(currentAngle.x, currentAngle.y, currentAngle.z + 360.0f);
        }
        else if(currentAngle.z > 180.0f)
        {
            currentAngle = new Vector3(currentAngle.x, currentAngle.y, currentAngle.z - 360.0f);
        }

        while(!isStop)
        {
            currentSynchronizerAngle = rotationSynchronizer.eulerAngles;

            if (currentSynchronizerAngle.z < -180.0f)
            {
                currentSynchronizerAngle = new Vector3(currentSynchronizerAngle.x, currentSynchronizerAngle.y, currentSynchronizerAngle.z + 360.0f);
            }
            else if (currentSynchronizerAngle.z > 180.0f)
            {
                currentSynchronizerAngle = new Vector3(currentSynchronizerAngle.x, currentSynchronizerAngle.y, currentSynchronizerAngle.z - 360.0f);
            }

            if (currentSynchronizerAngle.z < currentAngle.z - 1.0f)
            {
                rotationSynchronizer.Rotate(new Vector3(0.0f, 0.0f, 1.0f), Space.World);
            }
            else if(currentSynchronizerAngle.z > currentAngle.z + 1.0f)
            {
                rotationSynchronizer.Rotate(new Vector3(0.0f, 0.0f, -1.0f), Space.World);
            }
            else
            {
                break;
            }
            
            yield return null;
        }

        yield break;
    }
}
