using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> lightEnemies;
    public List<GameObject> darkEnemies;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public void AddLightEnemy(GameObject obj)
    {
        lightEnemies.Add(obj);
    }

    public void AddDarkEnemy(GameObject obj)
    {
        darkEnemies.Add(obj);
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
        }
    }

    public void ActivateDarkEnemies()
    {
        for (int i = 0; i < darkEnemies.Count; i++)
        {
            // Set enemy to normal movement and stuff
        }
    }

    public void DeactivateLightEnemies()
    {
        for (int i = 0; i < lightEnemies.Count; i++)
        {
            // Set enemy to not move and stuff
        }
    }

    public void DeactivateDarkEnemies()
    {
        for (int i = 0; i < darkEnemies.Count; i++)
        {
            // Set enemy to not move and stuff
        }
    }
}
