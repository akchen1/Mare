using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightsScript : MonoBehaviour
{
    public GameObject left;
    public GameObject right;

    private bool done;
    // Start is called before the first frame update
    void Start()
    {
        done = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            if (GetComponent<SpriteRenderer>().sprite.name.Contains("left"))
            {
                RightLight();
            }
            else if (GetComponent<SpriteRenderer>().sprite.name.Contains("right"))
            {
                LeftLight();
            }
            else
            {
                BothLight();
            }
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

    public void NoLight()
    {
        left.SetActive(false);
        right.SetActive(false);
        done = true;
    }
}
