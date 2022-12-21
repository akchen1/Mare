using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static float highScore;
    public static float score;
    public static float scale;

    [SerializeField] private Transform player;
    EdgeCollider2D bounds;
    float boundsRadius;

    void Start()
    {
        bounds = GameObject.FindGameObjectWithTag("Bounds").GetComponent<EdgeCollider2D>();
        boundsRadius = bounds.bounds.extents.x;
        highScore = PlayerPrefs.GetFloat(Constants.HIGHSCORE_KEY, 0f);
        score = 0;
        StartCoroutine(UpdateHighScore());
    }

    private void Update()
    {
        CalculateScale();
        UpdateScore(Time.deltaTime);
    }

    public static void UpdateScore(float unscaledValue)
    {
        score += unscaledValue * scale;
    }

    private void CalculateScale()
    {
        float radius = Mathf.Sqrt(player.position.x * player.position.x + player.position.y * player.position.y);
        if (radius / boundsRadius <= 0.4)
        {
            scale = 1;
        }
        else if (radius / boundsRadius <= 0.8)
        {
            scale = 0.5f;
        }
        else
        {
            scale = 0;
        }
    }

    private IEnumerator UpdateHighScore()
    {
        yield return new WaitUntil(() => StateManager.PLAYERSTATE == Constants.PLAYERSTATE.DEAD);
        if (score > highScore)
        {
            PlayerPrefs.SetFloat(Constants.HIGHSCORE_KEY, score);
        }
    }

    public static float GetHighScore()
    {
        return score > highScore ? score : highScore;
    }
}
