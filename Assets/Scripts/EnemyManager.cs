using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> lightEnemies;
    public List<GameObject> darkEnemies;

    public GameObject enemy;
    public float spawnTimer;
    private float timer;

    Vector3 bounds1;
    Vector3 bounds2;

    // Start is called before the first frame update
    void Start()
    {
        timer = spawnTimer;

        Transform[] bounds = gameObject.GetComponentsInChildren<Transform>();
        bounds1 = bounds[0].position;
        bounds2 = bounds[1].position;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = spawnTimer;
            Spawn();
        }
    }

    public void Spawn()
    {
        Vector3 spawnPos = new Vector3(Random.Range(bounds1.x, bounds2.x), Random.Range(bounds1.y, bounds2.y), 0);
        GameObject obj = Instantiate(enemy, spawnPos, Quaternion.identity);

        int num = Random.Range(0, 2);
        if (num == 0)
        {

            obj.GetComponent<EnemyController>().SetType("DarkEnemy");
            darkEnemies.Add(obj);
            return;
        }

        obj.GetComponent<EnemyController>().SetType("LightEnemy");
        lightEnemies.Add(obj);
        return;
    }

    public void ActivateLightEnemies()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("LightEnemy");

        if (list.Length > 0)
        {
            for (int i = 0; i < list.Length - 1; i++)
            {
                // Set enemy to normal movement and stuff
                list[i].GetComponent<Shoot>().enabled = true;
                list[i].GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").transform;
                list[i].GetComponent<EnemyController>().isFrozen = false;
            }
        }
    }

    public void ActivateDarkEnemies()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("DarkEnemy");

        if (list.Length > 0)
        {
            for (int i = 0; i < list.Length - 1; i++)
            {
                // Set enemy to normal movement and stuff
                list[i].GetComponent<Shoot>().enabled = true;
                list[i].GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").transform;
                list[i].GetComponent<EnemyController>().isFrozen = false;
            }
        }
    }

    public void DeactivateLightEnemies()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("LightEnemy");

        if (list.Length > 0)
        {
            for (int i = 0; i < list.Length - 1; i++)
            {
                // Set enemy to not move and stuff
                list[i].GetComponent<Shoot>().enabled = false;
                list[i].GetComponent<AIDestinationSetter>().target = null;
                list[i].GetComponent<AIPath>().SetPath(null);
                list[i].GetComponent<EnemyController>().isFrozen = true;
            }
        }
    }

    public void DeactivateDarkEnemies()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("DarkEnemy");

        if (list.Length > 0)
        {
            for (int i = 0; i < list.Length - 1; i++)
            {
                // Set enemy to not move and stuff
                list[i].GetComponent<Shoot>().enabled = false;
                list[i].GetComponent<AIDestinationSetter>().target = null;
                list[i].GetComponent<AIPath>().SetPath(null);
                list[i].GetComponent<EnemyController>().isFrozen = true;
            }
        }
    }
}
