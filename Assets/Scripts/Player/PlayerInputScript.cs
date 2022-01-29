using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementScript))]
public class PlayerInputScript : MonoBehaviour
{
    private MovementScript mScript;

    // Start is called before the first frame update
    void Start()
    {
        mScript = GetComponent<MovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // Move Up
            mScript.MoveNorth();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // Move Down
            mScript.MoveSouth();
        }
        else
        {
            // Stop
            mScript.StopLatitude();
        }

        if (Input.GetKey(KeyCode.A))
        {
            // Move Left
            mScript.MoveWest();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // Move Right
            mScript.MoveEast();
        }
        else
        {
            // Stop
            mScript.StopLongitude();
        }
    }
}
