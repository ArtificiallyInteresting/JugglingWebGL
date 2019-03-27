using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public int numBalls = 1;
    public int numPaddles = 1;
    BallController ballController;
    SeparatePaddlesController paddleController;
    public Paddle paddle;
    public Ball ball;
    public int updateEveryXFrames = 10;
    private int frameNo;
    // Start is called before the first frame update
    void Start()
    {
        frameNo = 0;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        ballController = ScriptableObject.CreateInstance<BallController>();
        ballController.init(numBalls, ball);
        paddleController = ScriptableObject.CreateInstance<SeparatePaddlesController>();
        //paddleController.init(numPaddles, paddle);
        paddleController.init(paddle);
    }

    // Update is called once per frame
    void Update()
    {
        if (frameNo++ % updateEveryXFrames == 0)
        {
            //var reward = calculateReward();
            //var state = discretizeState();
            paddleController.update(ballController);
        }

    }

    //string discretizeState()
    //{
    //    var state = "";
    //    state += this.ballController.getStateString();
    //    state += this.paddleController.getStateString();
    //    return state;
    //}

    //float calculateReward()
    //{
    //    float score = ballController.averageBallHeight();
    //    score += paddleController.getPaddleRewards();
    //    return score;
    //}

    public void outOfBounds(Collider2D collision)
    {
        //var state = discretizeState();
        //qlearner.SendState(state, -9999999, true);
        this.paddleController.reset();
        this.ballController.reset();
    }
}
