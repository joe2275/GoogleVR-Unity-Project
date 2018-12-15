using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationSynchronizer : MonoBehaviour {
    [SerializeField]
    private Transform playerTransform;

    private PlayerCameraController cameraController;

    private float distanceOfPlayer;

    private void Awake()
    {
        cameraController = FindObjectOfType<PlayerCameraController>();
        distanceOfPlayer = playerTransform.localPosition.y;
    }

    private void Update()
    {
        float distance = Vector2.Distance(Vector2.zero, new Vector2(playerTransform.localPosition.x, playerTransform.localPosition.y - distanceOfPlayer));

        transform.position = new Vector3(transform.position.x + distance * Mathf.Sin(-transform.eulerAngles.z * Mathf.PI / 180.0f),
            transform.position.y + distance * Mathf.Cos(-transform.eulerAngles.z * Mathf.PI / 180.0f), transform.position.z + playerTransform.localPosition.z);
        playerTransform.localPosition = new Vector3(0.0f, distanceOfPlayer, 0.0f);
        if (!cameraController.isStop && !cameraController.isFalling)
        {
            transform.Translate(0.0f, 0.0f, 0.005f, Space.World);
        }
    }


}
