using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public string lastHitBy = "";

    public string getStateString()
    {
        var posScale = .8;
        var velocityScale = 1;
        var state = "x";
        var pos = this.transform.position;
        var velocity = this.GetComponent<Rigidbody2D>().velocity;
        state += (int)(pos.x * posScale) + "_y";
        state += (int)(pos.y * posScale) + "_dx";
        state += (int)(velocity.x * velocityScale) + "_dy";
        state += (int)(velocity.y * velocityScale) + "|";

        return state;
    }
    public string getRelativeStateString(Vector3 relativeTo, Vector2 relativeVelocity)
    {
        var posScale = .4;
        var velocityScale = .5;
        var state = "x";
        var pos = this.transform.position;
        var velocity = this.GetComponent<Rigidbody2D>().velocity;
        state += (int)((pos.x - relativeTo.x) * posScale) + "_y";
        state += (int)((pos.y - relativeTo.x) * posScale) + "_dx";
        state += (int)((velocity.x - relativeVelocity.x) * velocityScale) + "_dy";
        state += (int)((velocity.y - relativeVelocity.y) * velocityScale) + "|";

        return state;
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
