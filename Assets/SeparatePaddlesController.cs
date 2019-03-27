using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class SeparatePaddlesController : ScriptableObject
{
    private Paddle paddle;
    private List<Vector2> paddlePositions;
    private List<float> paddleRotations;
    private SinglePaddleController leftPaddle;
    private SinglePaddleController rightPaddle;

    public void init(Paddle copyFromPaddle)
    {
        paddlePositions = new List<Vector2> { new Vector2(-5f, -3f), new Vector2(1f, -3f), new Vector2(5f, -3f) };
        paddleRotations = new List<float> { -15f, 15f, 0f };
        leftPaddle = ScriptableObject.CreateInstance<SinglePaddleController>();
        leftPaddle.init(copyFromPaddle, paddlePositions[0], paddleRotations[0], 0);
        rightPaddle = ScriptableObject.CreateInstance<SinglePaddleController>();
        rightPaddle.init(copyFromPaddle, paddlePositions[1], paddleRotations[1], 1);
    }

    public void update(BallController ballController)
    {
        leftPaddle.update(ballController);
        rightPaddle.update(ballController);
    }

    //public float getPaddleRewards()
    //{
    //    return leftPaddle.getPaddleRewards() + rightPaddle.getPaddleRewards();

    //}

    public void reset()
    {
        leftPaddle.reset();
        rightPaddle.reset();
    }


    //public string getStateString()
    //{
    //    var state = "Paddles";
    //    state += leftPaddle.getStateString();
    //    state += rightPaddle.getStateString();

    //    return state;
    //}

    //public void applyMotion(List<Vector2> motions)
    //{
    //    leftPaddle.applyMotion(motions[0]);
    //    rightPaddle.applyMotion(motions[1]);
    //}

}