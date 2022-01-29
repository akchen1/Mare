using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    public AIPath ai;
    // Start is called before the first frame update
    public bool inRange;
    private Shoot shoot;
    
    void Start()
    {
        inRange = false;
        shoot = GetComponent<Shoot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            shoot.ShootWeapon();
        }
    }

    public void setSpeed(float speed)
    {
        ai.maxSpeed = speed;
    }


}
