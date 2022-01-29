using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public GameObject lightWorld;
    public GameObject darkWorld;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchWorlds()
    {
        if (lightWorld.activeSelf && !darkWorld.activeSelf)
        {
            lightWorld.SetActive(false);
            darkWorld.SetActive(true);
            return;
        }

        else if (!lightWorld.activeSelf && darkWorld.activeSelf)
        {
            lightWorld.SetActive(true);
            darkWorld.SetActive(false);
            return;
        }
    }
}
