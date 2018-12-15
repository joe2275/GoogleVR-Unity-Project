using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSelector : MonoBehaviour {
    [SerializeField]
    private Transform leftPivot;
    [SerializeField]
    private Transform rightPivot;

    private ObstacleFallingController[] obstacles;

    private float timeTaken;
    private float timeTerm;
    private int countOfLevelUp;

    private Queue<ObstacleFallingController> obstacleQueue;

    private void Awake()
    {
        obstacles = GetComponentsInChildren<ObstacleFallingController>(true);
        obstacleQueue = new Queue<ObstacleFallingController>();
        foreach(ObstacleFallingController obstacle in obstacles)
        {
            obstacleQueue.Enqueue(obstacle);
        }
        timeTaken = 0.0f;
        timeTerm = 10.0f;
        countOfLevelUp = 0;
    }

    private void Update()
    {
        timeTaken += Time.deltaTime;

        if(timeTaken > timeTerm && obstacleQueue.Count != 0)
        {
            timeTaken = 0.0f;
            ObstacleFallingController obstacle = obstacleQueue.Dequeue();
            countOfLevelUp++;
            obstacle.transform.position = new Vector3(Random.Range(leftPivot.position.x, rightPivot.position.x), transform.position.y, transform.position.z);
            obstacle.gameObject.SetActive(true);
            StartCoroutine(obstacle.FallObstacle());

            if(countOfLevelUp > 5)
            {
                countOfLevelUp = 0;
                timeTerm = (timeTerm - 1.0f > 0.0f + Mathf.Epsilon ? timeTerm - 1.0f : 0.0f);
            }
        }
    }

    public void IncreaseIndexOfInstantiatable(ObstacleFallingController obstacle)
    {
        obstacleQueue.Enqueue(obstacle);
    }
}
