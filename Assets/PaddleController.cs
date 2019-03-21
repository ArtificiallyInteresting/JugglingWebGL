using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class PaddleController : MonoBehaviour
{
    List<Paddle> paddles = new List<Paddle>();

    public PaddleController(int numPaddles, Paddle paddle)
    {
        var paddlePositions = new List<List<float>> { new List<float> { -5f, -3f }, new List<float> { 5f, -3f }, new List<float> { 0f, -3f } };
        for (int i = 0; i < numPaddles; i++)
        {
            Paddle newPaddle = Instantiate(paddle, new Vector3(paddlePositions[i][0], paddlePositions[i][1], 0), Quaternion.identity);
            this.paddles.Add(newPaddle);
        }


    }

}