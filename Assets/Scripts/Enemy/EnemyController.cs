using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    public AIPath ai;

    public bool inRange;
    private Shoot shoot;

    public bool isFrozen;

    private const string DARK_ENEMY_MOVE = "dark_enemy_move";
    private const string DARK_ENEMY_ATTACK = "dark_enemy_attack";
    private const string DARK_ENEMY_FROZEN = "dark_enemy_frozen";
    private const string LIGHT_ENEMY_MOVE = "light_enemy_move";
    private const string LIGHT_ENEMY_ATTACK = "light_enemy_attack";
    private const string LIGHT_ENEMY_FROZEN = "light_enemy_frozen";

    private string currentAnimation;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
        shoot = GetComponent<Shoot>();

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
        }
        else if (tag == "DarkEnemy")
        {
            gameObject.tag = "DarkEnemy";
            ChangeAnimationState(DARK_ENEMY_MOVE);
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
