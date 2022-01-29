using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Rigidbody variable used to create constant horizontal movement
    Rigidbody2D rbody;

    // Direction to shoot
    Vector3 shootDirection;

    // Speed of bullet
    public int bulletSpeed = 20;

    // target tag
    public string target;

    public bool hasAnim;

    // Default Damage for now, this needs to be changed from ProjectileShootingScript.
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        // Find rigidbody
        rbody = GetComponent<Rigidbody2D>();

        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float playerHeight = player.GetComponent<PolygonCollider2D>().bounds.size.y / 2f;
        Transform playerLocation = player.transform;
        shootDirection = new Vector3(playerLocation.position.x, playerLocation.position.y, 0) - transform.position;
        

        shootDirection.Normalize();

        // Set velocity to shoot bullet
        rbody.velocity = shootDirection * bulletSpeed;
        //if (target == "Player" && shootDirection.x < 0)
        //{
        //    flip();
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }

    // This is a built in unity function that checks when the object collides with another object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
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
}
