using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public int numBalls = 1;
    public Ball ball;
    List<Ball> balls = new List<Ball>();

    // Start is called before the first frame update
    void Start()
    {
        var positions = new List<List<float>> { new List<float> { -5f, 1.5f }, new List<float> { -5f, 2.5f }, new List<float> { -5f, 3.5f }};
        for(int i = 0; i < numBalls; i++)
        {
            Ball ball = Instantiate(this.ball, new Vector3(positions[i][0], positions[i][1], 0), Quaternion.identity);
            this.balls.Add(ball);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
