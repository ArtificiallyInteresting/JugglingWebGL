using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    Main main;
    // Start is called before the first frame update
    void Start()
    {
        //main = (Main)GetComponent("Main");
        main = GameObject.Find("MainController").GetComponent<Main>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        main.outOfBounds(collision);
    }
}
