using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFallingController : MonoBehaviour {

    private ObstacleSelector obstacleSelector;

    private void Awake()
    {
        obstacleSelector = GetComponentInParent<ObstacleSelector>();
    }

    public IEnumerator FallObstacle()
    {
        //transform.position = new Vector3(Random.Range(13.0f, 70.0f), transform.position.y, transform.position.z);
        float end_y = transform.position.y - 350.0f;
        float fallingSpeed = Random.Range(3.0f, 20.0f);
        float rotationSpeed = Random.Range(10.0f, 30.0f);
        float rotation_x = Random.Range(0.0f, 100.0f);
        rotation_x = (rotation_x < 50.0f ? 1.0f : -1.0f);
        float rotation_y = Random.Range(0.0f, 100.0f);
        rotation_y = (rotation_y < 50.0f ? 1.0f : -1.0f);
        float rotation_z = Random.Range(0.0f, 100.0f);
        rotation_z = (rotation_z < 50.0f ? 1.0f : -1.0f);

        while (transform.position.y > end_y)
        {
            transform.Translate(new Vector3(0.0f, -fallingSpeed * Time.deltaTime, 0.0f), Space.World);
            transform.Rotate(new Vector3(rotationSpeed * rotation_x * Time.deltaTime, rotationSpeed * rotation_y * Time.deltaTime, rotationSpeed * rotation_z * Time.deltaTime));
            yield return null;
        }

        obstacleSelector.IncreaseIndexOfInstantiatable(this);
        gameObject.SetActive(false);
    }
}
