using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject quitButton;
    [SerializeField] private RectTransform[] menuButtons;

    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_STANDALONE
            quitButton.GetComponent<TextMeshProUGUI>().enabled = true;
            quitButton.GetComponent<Button>().enabled = true;
        #endif

        #if UNITY_ANDROID || UNITY_IOS
            foreach (RectTransform rect in menuButtons)
            {
                rect.sizeDelta = new Vector2(rect.rect.width, rect.rect.height) / 0.7f;
            }
        #endif
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
