using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> lightEnemies;
    public List<GameObject> darkEnemies;

    public GameObject lightEnemy;
    public GameObject darkEnemy;
    public float spawnTimer;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = spawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        // if in dark world
        if (StateManager.worldState == 0)
        {
            DeactivateLightEnemies();
            ActivateDarkEnemies();
        }
        // if in light world
        else
        {
            ActivateLightEnemies();
            DeactivateDarkEnemies();
        }

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Spawn();
            timer = spawnTimer;
        }
    }

    public void Spawn()
    {
        int num = Random.Range(0, 2);
        print(num);
        if (num == 0)
        {
            darkEnemies.Add(Instantiate(darkEnemy, new Vector3(0, 0, 0), Quaternion.identity));
            return;
        }

        lightEnemies.Add(Instantiate(lightEnemy, new Vector3(0, 0, 0), Quaternion.identity));
        return;
    }

    public void ClearLightEnemies()
    {
        for (int i = 0; i < lightEnemies.Count; i++)
        {
            Destroy(lightEnemies[i]);
        }
        lightEnemies.Clear();
    }

    public void ClearDarkEnemies()
    {
        for (int i = 0; i < darkEnemies.Count; i++)
        {
            Destroy(darkEnemies[i]);
        }
        darkEnemies.Clear();
    }

    public void ActivateLightEnemies()
    {
        for (int i = 0; i < lightEnemies.Count; i++)
        {
            // Set enemy to normal movement and stuff
            lightEnemies[i].GetComponent<Shoot>().enabled = true;
            lightEnemies[i].GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void ActivateDarkEnemies()
    {
        for (int i = 0; i < darkEnemies.Count; i++)
        {
            // Set enemy to normal movement and stuff
            darkEnemies[i].GetComponent<Shoot>().enabled = true;
            darkEnemies[i].GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void DeactivateLightEnemies()
    {
        for (int i = 0; i < lightEnemies.Count; i++)
        {
            // Set enemy to not move and stuff
            lightEnemies[i].GetComponent<Shoot>().enabled = false;
            lightEnemies[i].GetComponent<AIDestinationSetter>().target = null;
            lightEnemies[i].GetComponent<AIPath>().SetPath(null);
        }
    }

    public void DeactivateDarkEnemies()
    {
        for (int i = 0; i < darkEnemies.Count; i++)
        {
            // Set enemy to not move and stuff
            darkEnemies[i].GetComponent<Shoot>().enabled = false;
            darkEnemies[i].GetComponent<AIDestinationSetter>().target = null;
            darkEnemies[i].GetComponent<AIPath>().SetPath(null);
        }
    }
}
