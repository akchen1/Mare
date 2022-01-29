using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    // Variable for keeping track of individual movespeed
    public float movespeed;

    // Variable to access individual rigidbody for movement
    private Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Function to move North
    public void MoveNorth()
    {
        rbody.velocity = new Vector3(rbody.velocity.x, movespeed, 0);
    }

    // Function to move South
    public void MoveSouth()
    {
        rbody.velocity = new Vector3(rbody.velocity.x, -movespeed, 0);
    }

    // Function to move East
    public void MoveEast()
    {
        rbody.velocity = new Vector3(movespeed, rbody.velocity.y, 0);
    }

    // Function to move West
    public void MoveWest()
    {
        rbody.velocity = new Vector3(-movespeed, rbody.velocity.y, 0);
    }

    // Function to stop movement on latitude axis
    public void StopLatitude()
    {
        rbody.velocity = new Vector3(rbody.velocity.x, 0, 0);
    }

    // Function to stop movement on longitude axis
    public void StopLongitude()
    {
        rbody.velocity = new Vector3(0, rbody.velocity.y, 0);
    }

    // Function to stop all movement
    public void StopMovement()
    {
        rbody.velocity = Vector3.zero;
    }


}
