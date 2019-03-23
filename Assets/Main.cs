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
    public int updateEveryXFrames = 4;
    private int frameNo;
    // Start is called before the first frame update
    void Start()
    {
        frameNo = 0;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        ballController = ScriptableObject.CreateInstance<BallController>();
        ballController.init(numBalls, ball);
        paddleController = ScriptableObject.CreateInstance<PaddleController>();
        paddleController.init(numPaddles, paddle);
        qlearner = new qLearning(10000);
    }

    // Update is called once per frame
    void Update()
    {
        if (frameNo++ % updateEveryXFrames == 0)
        {
            var reward = calculateReward();
            var state = discretizeState();
            qlearner.SendState(state, reward, false);
            var motion = qlearner.GetAction();
            //Debug.Log(motion[0]);
            //Debug.Log(motion[1]);
            this.paddleController.applyMotion(motion);
        }

    }

    string discretizeState()
    {
        var state = "";
        state += this.ballController.getStateString();
        state += this.paddleController.getStateString();
        return state;
    }

    float calculateReward()
    {
        float score = ballController.averageBallHeight();
        score += paddleController.getPaddleRewards();
        return score;
    }

    public void outOfBounds(Collider2D collision)
    {
        var state = discretizeState();
        qlearner.SendState(state, -9999999, true);
        this.paddleController.reset();
        this.ballController.reset();
    }
}
