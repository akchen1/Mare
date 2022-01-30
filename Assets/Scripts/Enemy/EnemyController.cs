using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    public AIPath ai;
    public Rigidbody2D rbody;
    public bool inRange;
    public Shoot shoot;
    public AIDestinationSetter aiDest;
    public bool isFrozen;

    private const string DARK_ENEMY_MOVE = "dark_enemy_move";
    private const string DARK_ENEMY_ATTACK = "dark_enemy_attack";
    private const string DARK_ENEMY_FROZEN = "dark_enemy_frozen";
    private const string LIGHT_ENEMY_MOVE = "light_enemy_move";
    private const string LIGHT_ENEMY_ATTACK = "light_enemy_attack";
    private const string LIGHT_ENEMY_FROZEN = "light_enemy_frozen";

    private string currentAnimation;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
        shoot = GetComponent<Shoot>();
        rbody = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFrozen)
        {
            if (gameObject.tag == "DarkEnemy")
            {
                ChangeAnimationState(DARK_ENEMY_FROZEN);
            }
            else if (gameObject.tag == "LightEnemy")
            {
                ChangeAnimationState(LIGHT_ENEMY_FROZEN);
            }
            return;
        }
        else if (!isFrozen && !inRange)
        {
            if (gameObject.tag == "DarkEnemy")
            {
                ChangeAnimationState(DARK_ENEMY_MOVE);
            }
            else if (gameObject.tag == "LightEnemy")
            {
                ChangeAnimationState(LIGHT_ENEMY_MOVE);
            }
            return;
        }

        if (inRange)
        {
            if (gameObject.tag == "DarkEnemy")
            {
                ChangeAnimationState(DARK_ENEMY_ATTACK);
            }
            else if (gameObject.tag == "LightEnemy")
            {
                ChangeAnimationState(LIGHT_ENEMY_ATTACK);
            }
            shoot.ShootWeapon();
        }
    }

    public void setSpeed(float speed)
    {
        ai.maxSpeed = speed;
    }

    public void SetType(string tag)
    {
        if (tag == "LightEnemy")
        {
            gameObject.tag = "LightEnemy";
            ChangeAnimationState(LIGHT_ENEMY_MOVE);

            if (StateManager.worldState == 0)
            {
                Freeze();
            }
            else
            {
                Unfreeze();
            }
        }
        else if (tag == "DarkEnemy")
        {
            gameObject.tag = "DarkEnemy";
            ChangeAnimationState(DARK_ENEMY_MOVE);

            if (StateManager.worldState == 1)
            {
                Freeze();
            }
            else
            {
                Unfreeze();
            }
        }
    }

    public void Freeze()
    {
        isFrozen = true;
        shoot.enabled = false;
        ai.canMove = false;
        ai.SetPath(null);
        rbody.bodyType = RigidbodyType2D.Static;

        gameObject.layer = 7;

        if (gameObject.tag == "LightEnemy")
        {
            ChangeAnimationState(LIGHT_ENEMY_FROZEN);
        }
        else if (gameObject.tag == "DarkEnemy")
        {
            ChangeAnimationState(DARK_ENEMY_FROZEN);
        }
    }

    public void Unfreeze()
    {
        isFrozen = false;
        shoot.enabled = true;
        ai.maxSpeed = 1;
        ai.canMove = true;
        rbody.bodyType = RigidbodyType2D.Dynamic;
        aiDest.target = GameObject.FindGameObjectWithTag("Player").transform;
        gameObject.layer = 0;
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
