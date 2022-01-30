using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public EnemyManager eScript;

    // 0 = dark, 1 = light
    public static int worldState;
    public float cooldown;
    private float timer;

    public Camera cam;
    public GameObject lights;

    public AudioScript aScript;
    public InGameUIScript uiScript;

    // Start is called before the first frame update
    void Start()
    {
        worldState = 1;
        cam.backgroundColor = Color.white;
        lights.SetActive(false);

        timer = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && timer <= 0)
        {
            // Flip World
            SwitchWorlds();
            uiScript.Cast();
            timer = cooldown;
        }
    }

    public void SwitchWorlds()
    {
        aScript.SwitchTracks();
        aScript.PlayPhaseSwitch();

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
