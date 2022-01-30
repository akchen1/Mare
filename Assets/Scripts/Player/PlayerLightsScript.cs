using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightsScript : MonoBehaviour
{
    public GameObject left;
    public GameObject right;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<SpriteRenderer>().sprite.name.Contains("left"))
        {
            LeftLight();
        }
        else if (GetComponent<SpriteRenderer>().sprite.name.Contains("right"))
        {
            RightLight();
        }
        else
        {
            BothLight();
        }
    }

    public void LeftLight()
    {
        left.SetActive(true);
        right.SetActive(false);
    }

    public void RightLight()
    {
        left.SetActive(false);
        right.SetActive(true);
    }

    public void BothLight()
    {
        left.SetActive(true);
        right.SetActive(true);
    }
}
