using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Rigidbody variable used to create constant horizontal movement
    Rigidbody2D rbody;
    Animator animator;

    // Direction to shoot
    Vector3 shootDirection;

    // Speed of bullet
    public int bulletSpeed = 20;

    // Default Damage for now, this needs to be changed from ProjectileShootingScript.
    public int damage;

    private string currentAnimation;
    private const string DARK_BULLET_INITIATE = "dark_bullet_initiate";
    private const string DARK_BULLET_TRAVEL = "dark_bullet_travel";
    private const string LIGHT_BULLET_INITIATE = "light_bullet_initiate";
    private const string LIGHT_BULLET_TRAVEL = "light_bullet_travel";

    public bool initate_finish = false;

    public AudioScript aScript;
    public InGameUIScript uiScript;

    // Start is called before the first frame update
    void Start()
    {
        // Find rigidbody
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        uiScript = GameObject.FindGameObjectWithTag("UI").GetComponent<InGameUIScript>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Transform playerLocation = player.transform;
        shootDirection = new Vector3(playerLocation.position.x, playerLocation.position.y, 0) - transform.position;

        shootDirection.Normalize();

        if (shootDirection != Vector3.zero)
        {
            float angle = -(Mathf.Atan2(shootDirection.y, -shootDirection.x) * Mathf.Rad2Deg) -90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        

        if (gameObject.tag == "DarkEnemy")
        {
            ChangeAnimationState(LIGHT_BULLET_INITIATE);
        } else
        {
            ChangeAnimationState(DARK_BULLET_INITIATE);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (initate_finish)
        {
            // Set velocity to shoot bullet
            rbody.velocity = shootDirection * bulletSpeed;
            if (gameObject.tag == "DarkEnemy")
            {
                ChangeAnimationState(LIGHT_BULLET_TRAVEL);
            }
            else
            {
                ChangeAnimationState(DARK_BULLET_TRAVEL);
            }
            initate_finish = false;
        }
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Kill Player
            aScript.PlayPlayerHit();

            collision.gameObject.GetComponent<PlayerAnimationScript>().Dead();

            aScript.PlayGameOver();
            Destroy(this.gameObject);
            return;
        }
        // if hits light enemy
        bool enemyHit = (collision.gameObject.tag == "LightEnemy" && gameObject.tag == "DarkEnemy") || (collision.gameObject.tag == "DarkEnemy" && gameObject.tag == "LightEnemy");
        if (enemyHit)
        {
            // kill
            aScript.PlayEnemyHit();
            ScoreController.UpdateScore(5f);
            collision.gameObject.GetComponent<EnemyController>().isDead = true;
            Destroy(this.gameObject);
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            Destroy(this.gameObject);
        }
    }


    // This is also a built in unity function that checks if the object is no longer visible by any camera
    // Using this, we will delete the bullet when it is off-screen, to make sure the game doesn't lag with a shit ton of bullets
    // If you're testing the game without the game being maximized on play, the scene preview window will count as a camera
    private void OnBecameInvisible()
    {
        // Destroy this specific instance of the bullet
        Destroy(this.gameObject);
    }

    private void flip()
    {
        transform.localScale = transform.localScale * -1;
    }

    // Set's the bullet Damage
    public void setDamage(int dam)
    {
        damage = dam;
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
