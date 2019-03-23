using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class PaddleController : ScriptableObject
{
    List<Paddle> paddles = new List<Paddle>();
    private List<Vector2> paddlePositions;
    private List<float> paddleRotations;

    public void init(int numPaddles, Paddle paddle)
    {
        paddlePositions = new List<Vector2> { new Vector2(-5f, -3f), new Vector2(1f, -3f), new Vector2(5f, -3f) };
        paddleRotations = new List<float> { -15f, 15f, 0f };
        for (int i = 0; i < numPaddles; i++)
        {
            Paddle newPaddle = Instantiate(paddle, paddlePositions[i], Quaternion.identity);
            newPaddle.paddleName = "Paddle" + i;
            //newPaddle.originalRotation = new Quaternion(paddleRotations[i], paddleRotations[i], 0, 0);
            //newPaddle.originalRotation = new Quaternion(100, 0, 0, 0);
            newPaddle.originalRotation = Quaternion.Euler(new Vector3(0, 0, paddleRotations[i]));
            this.paddles.Add(newPaddle);
        }


    }

    public float getPaddleRewards()
    {
        float score = 0;
        for (int i = 0; i < paddles.Count; i++)
        {
            score += paddles[i].reward;
            paddles[i].reward = 0;
        }
        return score;

    }

    public void reset()
    {
        for (int i = 0; i < paddles.Count; i++)
        {
            paddles[i].transform.position = paddlePositions[i];
            paddles[i].transform.rotation = Quaternion.identity;
            paddles[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            paddles[i].GetComponent<Rigidbody2D>().angularVelocity = 0;
        }
    }


    public string getStateString()
    {
        var state = "Paddles|x";
        var posScale = 1;
        var velocityScale = 2;
        foreach (Paddle paddle in this.paddles)
        {
            var pos = paddle.transform.position;
            var velocity = paddle.GetComponent<Rigidbody2D>().velocity;
            state += (int)(pos.x * posScale) + "_y";
            state += (int)(pos.y * posScale) + "_dx";
            state += (int)(velocity.x * velocityScale) + "_dy";
            state += (int)(velocity.y * velocityScale) + "|";
        }
        return state;
    }

    public void applyMotion(List<Vector2> motions)
    {
        for(int i = 0; i < this.paddles.Count; i++)
        {
            var paddle = this.paddles[i];
            var motion = motions[i];
            paddle.GetComponent<Rigidbody2D>().AddForce(motion);

            //locking rotation for now.
            paddle.transform.rotation = paddle.originalRotation;
            paddle.GetComponent<Rigidbody2D>().angularVelocity = 0;

        }
    }

}