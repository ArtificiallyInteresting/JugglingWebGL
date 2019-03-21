using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    List<Ball> balls = new List<Ball>();

    public BallController(int numBalls, Ball ball)
    {
        var ballPositions = new List<List<float>> { new List<float> { -5f, -1.5f }, new List<float> { -5f, 0f }, new List<float> { -5f, 3.5f } };
        for (int i = 0; i < numBalls; i++)
        {
            Ball newBall = Instantiate(ball, new Vector3(ballPositions[i][0], ballPositions[i][1], 0), Quaternion.identity);
            this.balls.Add(newBall);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
