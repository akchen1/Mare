using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUIScript : MonoBehaviour
{
    public GameObject cd1;
    public GameObject cd2;
    public GameObject cd3;
    public GameObject cd4;
    public GameObject cd5;

    public Sprite empty;
    public Sprite filled;
    
    private float timer;

    public GameObject scoreText;
    public float score;

    Transform player;
    public float scale;
    EdgeCollider2D bounds;
    float boundsRadius;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;

        cd1.GetComponent<Image>().sprite = filled;
        cd2.GetComponent<Image>().sprite = filled;
        cd3.GetComponent<Image>().sprite = filled;
        cd4.GetComponent<Image>().sprite = filled;
        cd5.GetComponent<Image>().sprite = filled;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        bounds = GameObject.FindGameObjectWithTag("Bounds").GetComponent<EdgeCollider2D>();
        boundsRadius = bounds.bounds.extents.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 5f)
        {
            timer += Time.deltaTime;
        }

        if (timer >= 5f)
        {
            cd1.GetComponent<Image>().sprite = filled;
            cd2.GetComponent<Image>().sprite = filled;
            cd3.GetComponent<Image>().sprite = filled;
            cd4.GetComponent<Image>().sprite = filled;
            cd5.GetComponent<Image>().sprite = filled;
        }
        else if (timer >= 4f)
        {
            cd1.GetComponent<Image>().sprite = filled;
            cd2.GetComponent<Image>().sprite = filled;
            cd3.GetComponent<Image>().sprite = filled;
            cd4.GetComponent<Image>().sprite = filled;
            cd5.GetComponent<Image>().sprite = empty;
        }
        else if (timer >= 3f)
        {
            cd1.GetComponent<Image>().sprite = filled;
            cd2.GetComponent<Image>().sprite = filled;
            cd3.GetComponent<Image>().sprite = filled;
            cd4.GetComponent<Image>().sprite = empty;
            cd5.GetComponent<Image>().sprite = empty;
        }
        else if (timer >= 2f)
        {
            cd1.GetComponent<Image>().sprite = filled;
            cd2.GetComponent<Image>().sprite = filled;
            cd3.GetComponent<Image>().sprite = empty;
            cd4.GetComponent<Image>().sprite = empty;
            cd5.GetComponent<Image>().sprite = empty;
        }
        else if (timer >= 1f)
        {
            cd1.GetComponent<Image>().sprite = filled;
            cd2.GetComponent<Image>().sprite = empty;
            cd3.GetComponent<Image>().sprite = empty;
            cd4.GetComponent<Image>().sprite = empty;
            cd5.GetComponent<Image>().sprite = empty;
        }
        else
        {
            cd1.GetComponent<Image>().sprite = empty;
            cd2.GetComponent<Image>().sprite = empty;
            cd3.GetComponent<Image>().sprite = empty;
            cd4.GetComponent<Image>().sprite = empty;
            cd5.GetComponent<Image>().sprite = empty;
        }

        float radius = Mathf.Sqrt(player.position.x * player.position.x + player.position.y * player.position.y);
        if (radius/boundsRadius <= 0.4)
        {
            scale = 1;
        } else if (radius/boundsRadius <= 0.8)
        {
            scale = 0.5f;
        } else
        {
            scale = 0;
        }

        score += Time.deltaTime * scale;
        scoreText.GetComponent<TextMeshProUGUI>().text = ((int)score).ToString();
    }

    public void Cast()
    {
        timer = 0f;
        if (StateManager.worldState == 0)
        {
            cd1.GetComponent<Image>().color = new Color(255, 255, 255);
            cd2.GetComponent<Image>().color = new Color(255, 255, 255);
            cd3.GetComponent<Image>().color = new Color(255, 255, 255);
            cd4.GetComponent<Image>().color = new Color(255, 255, 255);
            cd5.GetComponent<Image>().color = new Color(255, 255, 255);

            scoreText.GetComponent<TextMeshProUGUI>().color = new Color(255, 255, 255);
        }
        else if (StateManager.worldState == 1)
        {
            cd1.GetComponent<Image>().color = new Color(0, 0, 0);
            cd2.GetComponent<Image>().color = new Color(0, 0, 0);
            cd3.GetComponent<Image>().color = new Color(0, 0, 0);
            cd4.GetComponent<Image>().color = new Color(0, 0, 0);
            cd5.GetComponent<Image>().color = new Color(0, 0, 0);

            scoreText.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0);
        }
    }
}
