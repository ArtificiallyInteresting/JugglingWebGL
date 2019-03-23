using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public string paddleName;
    public int reward;
    public Quaternion originalRotation;
    // Start is called before the first frame update
    void Start()
    {
        reward = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball.lastHitBy == paddleName)
        {
            reward = -100;
        } else
        {
            reward = 100;
        }
        ball.lastHitBy = paddleName;
    }
}
