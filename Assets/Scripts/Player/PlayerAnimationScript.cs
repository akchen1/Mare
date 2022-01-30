using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{

    Rigidbody2D rbody;
    private const string PLAYER_DARK_IDLE = "player_dark_idle";
    private const string PLAYER_DARK_IDLE_BACK = "player_dark_idle_back";
    private const string PLAYER_DARK_IDLE_LEFT = "player_dark_idle_left";
    private const string PLAYER_DARK_IDLE_RIGHT = "player_dark_idle_right";

    private const string PLAYER_DARK_MOVE = "player_dark_move";
    private const string PLAYER_DARK_MOVE_BACK = "player_dark_move_back";
    private const string PLAYER_DARK_MOVE_LEFT = "player_dark_move_left";
    private const string PLAYER_DARK_MOVE_RIGHT = "player_dark_move_right";

    private const string PLAYER_LIGHT_IDLE = "player_idle";
    private const string PLAYER_LIGHT_IDLE_BACK = "player_idle_back";
    private const string PLAYER_LIGHT_IDLE_LEFT = "player_idle_left";
    private const string PLAYER_LIGHT_IDLE_RIGHT = "player_idle_right";

    private const string PLAYER_LIGHT_MOVE = "player_move";
    private const string PLAYER_LIGHT_MOVE_BACK = "player_move_back";
    private const string PLAYER_LIGHT_MOVE_LEFT = "player_move_left";
    private const string PLAYER_LIGHT_MOVE_RIGHT = "player_move_right";

    public string currentAnimation;
    public string[] stateNames;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentAnimation = PLAYER_LIGHT_IDLE;
        stateNames = new string[] {PLAYER_DARK_IDLE, PLAYER_DARK_IDLE_BACK, PLAYER_DARK_IDLE_LEFT, PLAYER_DARK_IDLE_RIGHT,
            PLAYER_DARK_MOVE, PLAYER_DARK_MOVE_BACK, PLAYER_DARK_MOVE_LEFT, PLAYER_DARK_MOVE_RIGHT, PLAYER_LIGHT_IDLE, PLAYER_LIGHT_IDLE_BACK, PLAYER_LIGHT_IDLE_LEFT,
            PLAYER_LIGHT_IDLE_RIGHT, PLAYER_LIGHT_MOVE, PLAYER_LIGHT_MOVE_BACK, PLAYER_LIGHT_MOVE_LEFT, PLAYER_LIGHT_MOVE_RIGHT };
    }

    // Update is called once per frame
    void Update()
    {
        if (rbody.velocity.x < 0)
        {
            // Look Left
            if (StateManager.worldState == 0)
            {
                ChangeAnimationState(PLAYER_DARK_MOVE_LEFT);
            }
            else
            {
                ChangeAnimationState(PLAYER_LIGHT_MOVE_LEFT);
            }
            
        }
        else if (rbody.velocity.x > 0)
        {
            // Look Right
            if (StateManager.worldState == 0)
            {
                ChangeAnimationState(PLAYER_DARK_MOVE_RIGHT);
            }
            else
            {
                ChangeAnimationState(PLAYER_LIGHT_MOVE_RIGHT);
            }
        }

        else if (rbody.velocity.y < 0)
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
                ChangeAnimationState(PLAYER_DARK_MOVE_BACK);
            }
            else
            {
                ChangeAnimationState(PLAYER_LIGHT_MOVE_BACK);
            }
        }

        if (rbody.velocity == Vector2.zero)
        {
            animator.SetBool("stop", true);
            // No Movement
            if (StateManager.worldState == 0)
            {
                if (currentAnimation == PLAYER_LIGHT_IDLE)
                    ChangeAnimationState(PLAYER_DARK_IDLE);
                else if (currentAnimation == PLAYER_LIGHT_IDLE_BACK)
                    ChangeAnimationState(PLAYER_DARK_IDLE_BACK);
                else if (currentAnimation == PLAYER_LIGHT_IDLE_LEFT)
                    ChangeAnimationState(PLAYER_DARK_IDLE_LEFT);
                else if (currentAnimation == PLAYER_LIGHT_IDLE_RIGHT)
                    ChangeAnimationState(PLAYER_DARK_IDLE_RIGHT);
            }
            else
            {
                if (currentAnimation == PLAYER_DARK_IDLE)
                    ChangeAnimationState(PLAYER_LIGHT_IDLE);
                else if (currentAnimation == PLAYER_DARK_IDLE_BACK)
                    ChangeAnimationState(PLAYER_LIGHT_IDLE_BACK);
                else if (currentAnimation == PLAYER_DARK_IDLE_LEFT)
                    ChangeAnimationState(PLAYER_LIGHT_IDLE_LEFT);
                else if (currentAnimation == PLAYER_DARK_IDLE_RIGHT)
                    ChangeAnimationState(PLAYER_LIGHT_IDLE_RIGHT);
            }
        } else
        {
            animator.SetBool("stop", false);
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
