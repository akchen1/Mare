using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{

    EnemyController dad;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        dad = GetComponentInParent<EnemyController>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "Player")
        {

            RaycastHit2D hit = Physics2D.Linecast(dad.transform.position, player.transform.position, (1 << 6) | (1 << 3));

            if (hit.collider != null && (hit.collider.name == "Player" || hit.collider.name == "Enemy"))
            {
                dad.setSpeed(0);
                dad.inRange = true;
            }
            
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {

            RaycastHit2D hit = Physics2D.Linecast(dad.transform.position, player.transform.position, (1<<6)|(1<<3));
            if (hit.collider != null && (hit.collider.name == "Player" || hit.collider.name == "Enemy"))
            {
                dad.setSpeed(0);
                dad.inRange = true;

            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dad.setSpeed(1);
        dad.inRange = false;
    }
}
