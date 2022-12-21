using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    

    public void switchScenes(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void switchPage(GameObject img)
    {
        img.SetActive(true);
        gameObject.SetActive(false);
    }
}
