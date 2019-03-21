using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public int numBalls = 1;
    public int numPaddles = 1;
    BallController ballController;
    PaddleController paddleController;
    public Paddle paddle;
    public Ball ball;
    qLearning qlearner;
    // Start is called before the first frame update
    void Start()
    {
        ballController = new BallController(numBalls, ball);
        paddleController = new PaddleController(numPaddles, paddle);
        qlearner = new qLearning(10, 10);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
