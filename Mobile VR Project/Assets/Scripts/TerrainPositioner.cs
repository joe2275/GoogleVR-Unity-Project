using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPositioner : MonoBehaviour {

    [SerializeField]
    private Transform startCliff;
    [SerializeField]
    private Transform currentCliff;

    private Transform obstacleSelector;

    private void Awake()
    {
        obstacleSelector = FindObjectOfType<ObstacleSelector>().transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            startCliff.position = currentCliff.position;
            currentCliff.localPosition = new Vector3(0.0f, currentCliff.localPosition.y + 200.0f, 0.0f);
            obstacleSelector.localPosition = new Vector3(obstacleSelector.localPosition.x, obstacleSelector.localPosition.y + 100.0f, obstacleSelector.localPosition.z);
        }
    }
}
