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

    public void Move(Vector2 moveVector)
    {
        rbody.velocity = moveVector * movespeed;
    }
}
