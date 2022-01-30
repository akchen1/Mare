using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer sprite;

    private string currentAnimation;
    private const string DARK_TOWER = "tower_dark";
    private const string LIGHT_TOWER = "tower_light";

    public bool isTower;
    // Start is called before the first frame update
    void Start()
    {
        if (!isTower)
        {
            return;
        }
        ChangeAnimationState(LIGHT_TOWER);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTower)
        {
            return;
        }
        if (StateManager.worldState == 0)
        {
            ChangeAnimationState(DARK_TOWER);

        } else
        {
            ChangeAnimationState(LIGHT_TOWER);

        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        sprite.sortingOrder = 2;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        sprite.sortingOrder = 2;
    //    }
    //}

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
