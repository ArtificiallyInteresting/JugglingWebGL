using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;


public class SinglePaddleController : ScriptableObject
{
    private Paddle paddle;
    private singlePaddleQLearning qlearner;

    public void init(Paddle copyFromPaddle, Vector2 position, float rotation, int paddleNo)
    {
        paddle = Instantiate(copyFromPaddle, position, Quaternion.identity);
        paddle.paddleName = "Paddle" + paddleNo;
        paddle.originalRotation = Quaternion.Euler(new Vector3(0, 0, rotation));
        paddle.originalPosition = position;
        paddleNo += 1;
        qlearner = new singlePaddleQLearning(40000);
    }

    public void update(BallController ballController)
    {
        var state = getOneBallStateString(ballController);
        var reward = getOneBallReward(ballController);
        qlearner.SendState(state, reward, false);
        var motion = qlearner.GetAction();
        this.applyMotion(motion);
    }

    public float getPaddleRewards()
    {
        var reward = paddle.reward;
        paddle.reward = 0;
        return reward;

    }

    public void reset()
    {
        qlearner.SendState("Failure", -9999999, true);
        paddle.transform.position = paddle.originalPosition;
        paddle.transform.rotation = Quaternion.identity;
        paddle.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        paddle.GetComponent<Rigidbody2D>().angularVelocity = 0;
    }


    //public string getStateString()
    //{
    //    var state = "|x";
    //    var posScale = 1;
    //    var velocityScale = 2;
    //    var pos = paddle.transform.position;
    //    var velocity = paddle.GetComponent<Rigidbody2D>().velocity;
    //    state += (int)(pos.x * posScale) + "_y";
    //    state += (int)(pos.y * posScale) + "_dx";
    //    state += (int)(velocity.x * velocityScale) + "_dy";
    //    state += (int)(velocity.y * velocityScale) + "|";
    //    return state;
    //}

    public string getOneBallStateString(BallController ballController)
    {
        Ball closestBall = ballController.getClosestBall(paddle.transform.position);
        var state = "|x";
        var posScale = .25;
        var pos = paddle.transform.position;
        var velocity = paddle.GetComponent<Rigidbody2D>().velocity;
        state += (int)(pos.x * posScale) + "_y";
        state += (int)(pos.y * posScale);
        state += closestBall.getRelativeStateString(pos, velocity);
        //Debug.Log(state);
        return state;
    }

    public float getOneBallReward(BallController ballController)
    {
        //var scale = .1f;
        //Ball closestBall = ballController.getClosestBall(paddle.transform.position);
        //var reward = 0f;
        //var velocity = closestBall.GetComponent<Rigidbody2D>().velocity;
        //reward += Math.Abs(velocity.x) + velocity.y;
        //reward *= scale;
        var reward = getPaddleRewards();
        //Debug.Log(reward);
        return reward;
    }

    public void applyMotion(Vector2 motion)
    {
        
        paddle.GetComponent<Rigidbody2D>().AddForce(motion);

        //locking rotation for now.
        paddle.transform.rotation = paddle.originalRotation;
        paddle.GetComponent<Rigidbody2D>().angularVelocity = 0;
        
    }

}