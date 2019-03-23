using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : ScriptableObject
{
    List<Ball> balls = new List<Ball>();
    private List<Vector2> ballPositions;

    public void init(int numBalls, Ball ball)
    {
        ballPositions = new List<Vector2> { new Vector2(-5f, 3.5f), new Vector2(5f, 0f), new Vector2(-5f, -3.5f) };
        for (int i = 0; i < numBalls; i++)
        {
            Ball newBall = Instantiate(ball, ballPositions[i], Quaternion.identity);
            this.balls.Add(newBall);
        }

    }

    public string getStateString()
    {
        var state = "Balls|x";
        var posScale = .8;
        var velocityScale = 1;
        foreach (Ball ball in this.balls)
        {
            var pos = ball.transform.position;
            var velocity = ball.GetComponent<Rigidbody2D>().velocity;
            state += (int)(pos.x * posScale) + "_y";
            state += (int)(pos.y * posScale) + "_dx";
            state += (int)(velocity.x * velocityScale) + "_dy";
            state += (int)(velocity.y * velocityScale) + "|";
        }
        return state;
    }


    public void reset()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].transform.position = ballPositions[i];
            balls[i].transform.rotation = Quaternion.identity;
            balls[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            balls[i].GetComponent<Rigidbody2D>().angularVelocity = 0;
        }
    }

    public float averageBallHeight()
    {
        float height = 0f;
        for (int i = 0; i < balls.Count; i++)
        {
            height += balls[i].transform.position.y;
        }
        return height;
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
