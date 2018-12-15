using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private GameObject gameOverImage;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private AudioSource audioSource;

    public bool isStop { get; private set; }
    public bool isFalling { get; private set; }

    private MagneticController magneticController;
    private int currentScore;

    private void Start()
    {
        isStop = false;
        isFalling = false;
        currentScore = -20;
        magneticController = MagneticController.instance;
    }

    private void Update()
    {
        checkCameraRotation();

        if (magneticController.CheckMagneticSensor() || (Input.touchCount != 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetKeyDown(KeyCode.A))
        {
            Click();
        }

        checkScore();
    }

    private void checkScore()
    {
        int updatedScore = (int)transform.position.y;
        if(currentScore < updatedScore)
        {
            currentScore = updatedScore;
            scoreText.text = "Score : " + currentScore;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9 && !isFalling)
        {
            playerAnimator.enabled = true;
            isFalling = true;
            playerAnimator.SetTrigger("falling");
            StartCoroutine(falling());
            gameOverImage.SetActive(true);
            audioSource.Play();
        }
    }

    private IEnumerator falling()
    {
        float verticalVelocity = 0.0f;
        float horizontalVelocity = 5.0f;
        while(true)
        {
            verticalVelocity += 0.1f;
            if (horizontalVelocity < +Mathf.Epsilon)
            {
                horizontalVelocity -= 0.001f;
            }
            rotationSynchronizer.Translate(new Vector3(0.0f, -verticalVelocity*Time.deltaTime, -horizontalVelocity*Time.deltaTime), Space.World);
            yield return null;
        }
    }

    private void checkCameraRotation()
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
            magneticController.CheckMagneticSensor();
            playerRb.isKinematic = true;
            playerAnimator.enabled = false;
        }
        else
        {
            isStop = false;
            magneticController.CheckMagneticSensor();
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
