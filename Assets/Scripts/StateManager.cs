using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public GameObject lightWorld;
    public GameObject darkWorld;

    // 0 = dark, 1 = light
    public int worldState;

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
        
    }

    public void SwitchWorlds()
    {
        if (worldState == 1)
        {
            lightWorld.SetActive(true);
            darkWorld.SetActive(false);
            return;
        }

        else if (worldState == 0)
        {
            lightWorld.SetActive(false);
            darkWorld.SetActive(true);
            return;
        }
    }
}
