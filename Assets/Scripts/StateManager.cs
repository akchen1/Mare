using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public GameObject lightWorld;
    public GameObject darkWorld;

    public EnemyManager eScript;

    // 0 = dark, 1 = light
    public static int worldState;

    // Start is called before the first frame update
    void Start()
    {
        worldState = 1;
        lightWorld.SetActive(true);
        darkWorld.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("flip");
            // Flip World
            SwitchWorlds();
        }
    }

    public void SwitchWorlds()
    {
        if (worldState == 0)
        {
            // Switch to light
            lightWorld.SetActive(true);
            darkWorld.SetActive(false);
            worldState = 1;

            // Activate opposite enemies
            eScript.ActivateDarkEnemies();
            eScript.DeactivateLightEnemies();
            return;
        }

        else if (worldState == 1)
        {
            // Switch to dark
            lightWorld.SetActive(false);
            darkWorld.SetActive(true);
            worldState = 0;

            // Activate opposite enemies
            eScript.ActivateLightEnemies();
            eScript.DeactivateDarkEnemies();
            return;
        }
    }
}
