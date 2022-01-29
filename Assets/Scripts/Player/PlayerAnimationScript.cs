using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{

    Rigidbody2D rbody;
    private const string PLAYER_LIGHT_IDLE = "player_idle";
    private const string PLAYER_DARK_IDLE = "player_dark_idle";
    private const string PLAYER_LIGHT_MOVE = "player_move";
    private const string PLAYER_DARK_MOVE = "player_dark_move";

    private string currentAnimation;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentAnimation = PLAYER_LIGHT_IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        if (rbody.velocity.x < 0)
        {
            // Look Left
            if (StateManager.worldState == 0)
            {
                ChangeAnimationState(PLAYER_DARK_MOVE);
            }
            else
            {
                ChangeAnimationState(PLAYER_LIGHT_MOVE);
            }
            
        }
        else if (rbody.velocity.x > 0)
        {
            // Look Right
            if (StateManager.worldState == 0)
            {
                ChangeAnimationState(PLAYER_DARK_MOVE);
            }
            else
            {
                ChangeAnimationState(PLAYER_LIGHT_MOVE);
            }
        }

        if (rbody.velocity.y < 0)
        {
            // Look Down
            if (StateManager.worldState == 0)
            {
                ChangeAnimationState(PLAYER_DARK_MOVE);
            }
            else
            {
                ChangeAnimationState(PLAYER_LIGHT_MOVE);
            }
        }
        else if (rbody.velocity.y > 0)
        {
            // Look Up
            if (StateManager.worldState == 0)
            {
                ChangeAnimationState(PLAYER_DARK_MOVE);
            }
            else
            {
                ChangeAnimationState(PLAYER_LIGHT_MOVE);
            }
        }

        if (rbody.velocity == Vector2.zero)
        {
            // No Movement
            if (StateManager.worldState == 0)
            {
                ChangeAnimationState(PLAYER_DARK_IDLE);
            }
            else
            {
                ChangeAnimationState(PLAYER_LIGHT_IDLE);
            }
        }
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentAnimation == newState)
        {
            return;
        }
        animator.Play(newState);
        currentAnimation = newState;
    }
}
