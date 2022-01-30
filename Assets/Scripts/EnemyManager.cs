using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTimer;
    private float timer;
    private float clockTime;

    Vector3 bounds1;
    Vector3 bounds2;

    public AudioScript aScript;

    // Start is called before the first frame update
    void Start()
    {
        timer = spawnTimer;
        clockTime = 30;
        Transform[] bounds = gameObject.GetComponentsInChildren<Transform>();
        bounds1 = bounds[0].position;
        bounds2 = bounds[1].position;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        clockTime -= Time.deltaTime;
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = spawnTimer;
            Spawn();
        }

        if (clockTime <= 0 && spawnTimer > 2)
        {
            spawnTimer -= 1;
            clockTime = 30;
        }
    }

    public void Spawn()
    {
        for (int i = 0; i < (int)Random.Range(1, 3); i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(bounds1.x, bounds2.x), Random.Range(bounds2.y, bounds1.y), 0);

            GameObject obj = Instantiate(enemy, spawnPos, Quaternion.identity);
            obj.GetComponent<Shoot>().aScript = aScript;

            int num = Random.Range(0, 2);
            if (num == 0)
            {

                obj.GetComponent<EnemyController>().SetType("DarkEnemy");
                obj.layer = 9;

                AstarPath.active.Scan();
                return;
            }

            obj.GetComponent<EnemyController>().SetType("LightEnemy");
            obj.layer = 8;

            AstarPath.active.Scan();
        }
        return;
    }

    public void ActivateLightEnemies()
    {
        print("ativate light");
        GameObject[] list = GameObject.FindGameObjectsWithTag("LightEnemy");
        
        if (list.Length > 0)
        {
            for (int i = 0; i < list.Length; i++)
            {
                // Set enemy to normal movement and stuff
                try
                {
                    list[i].GetComponent<EnemyController>().Unfreeze();

                } catch { }
            }
        }
    }

    public void ActivateDarkEnemies()
    {
        print("activate dark");
        GameObject[] list = GameObject.FindGameObjectsWithTag("DarkEnemy");

        if (list.Length > 0)
        {
            for (int i = 0; i < list.Length; i++)
            {
                // Set enemy to normal movement and stuff
                try
                {
                    list[i].GetComponent<EnemyController>().Unfreeze();
                } catch { }
            }
        }
    }
    
    public void DeactivateLightEnemies()
    {
        print("Deactivate light");
        GameObject[] list = GameObject.FindGameObjectsWithTag("LightEnemy");

        if (list.Length > 0)
        {
            for (int i = 0; i < list.Length; i++)
            {
                // Set enemy to not move and stuff
                try
                {
                    list[i].GetComponent<EnemyController>().Freeze();
                } catch { }
            }
        }
    }

    public void DeactivateDarkEnemies()
    {
        print("Deactivate dark");
        GameObject[] list = GameObject.FindGameObjectsWithTag("DarkEnemy");

        if (list.Length > 0)
        {
            for (int i = 0; i < list.Length; i++)
            {
                // Set enemy to not move and stuff
                try
                {
                    list[i].GetComponent<EnemyController>().Freeze();
                } catch
                {

                }
            }
        }
    }
}
