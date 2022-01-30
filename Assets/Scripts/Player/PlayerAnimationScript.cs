using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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

    private const string PLAYER_DEAD = "player_dead";

    public string currentAnimation;
    public string[] stateNames;

    Animator animator;

    public InGameUIScript iScript;
    public EnemyManager eScript;
    public PolygonCollider2D poly;
    public Camera cam;
    public PlayerLightsScript lScript;

    private bool zooming;
    private float x;
    private float y;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        poly = GetComponent<PolygonCollider2D>();
        lScript = GetComponent<PlayerLightsScript>();

        zooming = false;
        x = 0f;
        y = 0f;

        currentAnimation = PLAYER_LIGHT_IDLE;
        stateNames = new string[] {PLAYER_DARK_IDLE, PLAYER_DARK_IDLE_BACK, PLAYER_DARK_IDLE_LEFT, PLAYER_DARK_IDLE_RIGHT,
            PLAYER_DARK_MOVE, PLAYER_DARK_MOVE_BACK, PLAYER_DARK_MOVE_LEFT, PLAYER_DARK_MOVE_RIGHT, PLAYER_LIGHT_IDLE, PLAYER_LIGHT_IDLE_BACK, PLAYER_LIGHT_IDLE_LEFT,
            PLAYER_LIGHT_IDLE_RIGHT, PLAYER_LIGHT_MOVE, PLAYER_LIGHT_MOVE_BACK, PLAYER_LIGHT_MOVE_LEFT, PLAYER_LIGHT_MOVE_RIGHT };
    }

    // Update is called once per frame
    void Update()
    {
        if (zooming)
        {
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, 2f, 2f * Time.deltaTime);

            x = Mathf.MoveTowards(x, transform.position.x + 0.02f, 0.01f * Time.deltaTime);
            y = Mathf.MoveTowards(y, transform.position.y - 0.47f, 0.235f / 1.5f * 2 * Time.deltaTime);
            cam.transform.position = new Vector3(x, y, -10);
        }

        if (!InGameUIScript.alive)
        {
            return;
        }
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

    public void Dead()
    {
        ChangeAnimationState(PLAYER_DEAD);
        rbody.constraints = RigidbodyConstraints2D.FreezeAll;
        poly.enabled = false;
        eScript.canSpawn = false;
        InGameUIScript.alive = false;
        iScript.Hide();
        lScript.NoLight();
        x = transform.position.x;
        y = transform.position.y;
        zooming = true;

        GameObject[] list = GameObject.FindGameObjectsWithTag("DarkEnemy");
        if (list.Length > 0)
        {
            for (int i = 0; i < list.Length; i++)
            {
                // Stop all enemies
                try
                {
                    Destroy(list[i]);
                }
                catch { }
            }
        }

        list = GameObject.FindGameObjectsWithTag("LightEnemy");
        if (list.Length > 0)
        {
            for (int i = 0; i < list.Length; i++)
            {
                // Stop all enemies
                try
                {
                    Destroy(list[i]);
                }
                catch { }
            }
        }

        if (StateManager.worldState == 0)
        {
            StateManager.worldState = 1;
            iScript.Swap();
            gameObject.GetComponentInChildren<Camera>().backgroundColor = Color.white;
        }

        iScript.Show();
        
    }
}
