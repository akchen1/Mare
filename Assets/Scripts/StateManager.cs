using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public EnemyManager eScript;

    // 0 = dark, 1 = light
    public static int worldState;

    public Camera cam;
    public GameObject lights;

    // Start is called before the first frame update
    void Start()
    {
        worldState = 1;
        cam.backgroundColor = Color.white;
        lights.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Flip World
            SwitchWorlds();
        }
    }

    public void SwitchWorlds()
    {
        if (worldState == 0)
        {
            // Switch to light
            worldState = 1;
            cam.backgroundColor = Color.white;
            lights.SetActive(false);

            // Activate opposite enemies
            eScript.ActivateLightEnemies();
            eScript.DeactivateDarkEnemies();

            AstarPath.active.Scan();
            return;
        }

        else if (worldState == 1)
        {
            // Switch to dark
            worldState = 0;
            cam.backgroundColor = Color.black;
            lights.SetActive(true);

            // Activate opposite enemies
            eScript.ActivateDarkEnemies();
            eScript.DeactivateLightEnemies();

            AstarPath.active.Scan();
            return;
        }
    }
}
