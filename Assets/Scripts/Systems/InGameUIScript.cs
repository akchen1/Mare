using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    //public static bool alive;

    public GameObject reset;
    public GameObject menu;
    public GameObject finalScore;
    public GameObject highScore;

    private bool fadeIn;


    #if (UNITY_IOS || UNITY_ANDROID)
        [Header("Mobile")]
        [SerializeField] private Image SwitchWorldsButtonLight;
        [SerializeField] private Image SwitchWorldsButtonDark;
        [SerializeField] private GameObject MobileUI;
    #endif


    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        //alive = true;
        fadeIn = false;
        cd1.GetComponent<Image>().sprite = filled;
        cd2.GetComponent<Image>().sprite = filled;
        cd3.GetComponent<Image>().sprite = filled;
        cd4.GetComponent<Image>().sprite = filled;
        cd5.GetComponent<Image>().sprite = filled;


        reset.GetComponent<TextMeshProUGUI>().alpha = 0f;
        reset.GetComponent<Button>().interactable = false;
        menu.GetComponent<TextMeshProUGUI>().alpha = 0f;
        menu.GetComponent<Button>().interactable = false;
        finalScore.GetComponent<TextMeshProUGUI>().alpha = 0f;
        highScore.GetComponent<TextMeshProUGUI>().alpha = 0f;



        #if (UNITY_IOS || UNITY_ANDROID)
        MobileUI.SetActive(true);
        #endif

    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 5f)
        {
            timer += Time.deltaTime;
        }

        AbilityIconGraphic();      

        if (StateManager.PLAYERSTATE == Constants.PLAYERSTATE.ALIVE)
        {
            UpdateScore();
        }
        
        if (fadeIn)
        {
            reset.GetComponent<TextMeshProUGUI>().alpha = Mathf.MoveTowards(reset.GetComponent<TextMeshProUGUI>().alpha, 1f, 0.25f * Time.deltaTime);
            menu.GetComponent<TextMeshProUGUI>().alpha = Mathf.MoveTowards(menu.GetComponent<TextMeshProUGUI>().alpha, 1f, 0.25f * Time.deltaTime);
            finalScore.GetComponent<TextMeshProUGUI>().alpha = Mathf.MoveTowards(finalScore.GetComponent<TextMeshProUGUI>().alpha, 1f, 0.25f * Time.deltaTime);
            highScore.GetComponent<TextMeshProUGUI>().alpha = Mathf.MoveTowards(finalScore.GetComponent<TextMeshProUGUI>().alpha, 1f, 0.25f * Time.deltaTime);

        }

    }

    private void UpdateScore()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = ((int)ScoreController.score).ToString();
        finalScore.GetComponent<TextMeshProUGUI>().text = "  Final Score" + System.Environment.NewLine + ((int)ScoreController.score).ToString();
        highScore.GetComponent<TextMeshProUGUI>().text = "  Personal Best" + System.Environment.NewLine + ((int)ScoreController.GetHighScore()).ToString();
    }

    public void Cast()
    {
        timer = 0f;
        Swap();
    }

    private void AbilityIconGraphic()
    {
        #if (UNITY_IOS || UNITY_ANDROID)
            float progress = timer / 5f;
        
            SwitchWorldsButtonDark.fillAmount = StateManager.WORLDSTATE == Constants.WORLDSTATE.BLACK ? progress : 1f - progress;
            SwitchWorldsButtonLight.fillAmount = StateManager.WORLDSTATE == Constants.WORLDSTATE.BLACK ? 1f - progress : progress;
        #endif
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
    }

    public void Swap()
    {
        if (StateManager.WORLDSTATE == Constants.WORLDSTATE.BLACK)
        {
            cd1.GetComponent<Image>().color = new Color(255, 255, 255);
            cd2.GetComponent<Image>().color = new Color(255, 255, 255);
            cd3.GetComponent<Image>().color = new Color(255, 255, 255);
            cd4.GetComponent<Image>().color = new Color(255, 255, 255);
            cd5.GetComponent<Image>().color = new Color(255, 255, 255);

            scoreText.GetComponent<TextMeshProUGUI>().color = new Color(255, 255, 255);
        }
        else if (StateManager.WORLDSTATE == Constants.WORLDSTATE.WHITE)
        {
            cd1.GetComponent<Image>().color = new Color(0, 0, 0);
            cd2.GetComponent<Image>().color = new Color(0, 0, 0);
            cd3.GetComponent<Image>().color = new Color(0, 0, 0);
            cd4.GetComponent<Image>().color = new Color(0, 0, 0);
            cd5.GetComponent<Image>().color = new Color(0, 0, 0);

            scoreText.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0);
        }
    }

    public void Hide()
    {
        cd1.GetComponent<Image>().CrossFadeAlpha(0f, 1f, false);
        cd2.GetComponent<Image>().CrossFadeAlpha(0f, 1f, false);
        cd3.GetComponent<Image>().CrossFadeAlpha(0f, 1f, false);
        cd4.GetComponent<Image>().CrossFadeAlpha(0f, 1f, false);
        cd5.GetComponent<Image>().CrossFadeAlpha(0f, 1f, false);

        scoreText.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0f, 1f, false);

        #if (UNITY_IOS || UNITY_ANDROID)
            MobileUI.SetActive(false);
        #endif
    }

    public void Show()
    {
        reset.GetComponent<TextMeshProUGUI>().alpha = 0.1f;
        menu.GetComponent<TextMeshProUGUI>().alpha = 0.1f;

        finalScore.GetComponent<TextMeshProUGUI>().alpha = 0.1f;
        highScore.GetComponent<TextMeshProUGUI>().alpha = 0.1f;


        fadeIn = true;

        reset.GetComponent<Button>().interactable = true;
        menu.GetComponent<Button>().interactable = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
