using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OptionController : MonoBehaviour
{
    public enum Option
    {
        sound,
        music,
        lc,
        dm
    }

    public Option option;
    public Sprite on;
    public Sprite off;
    Image img;
    
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        img = GetComponent<Image>();
        int status = -1;
        if (option == Option.sound)
        {
            status = PlayerPrefs.GetInt("sound", 1);
            PlayerPrefs.SetInt("sound", status);
        }
        else if (option == Option.music)
        {
            status = PlayerPrefs.GetInt("music", 1);
            PlayerPrefs.SetInt("music", status);
        }
        else if (option == Option.lc)
        {
            status = PlayerPrefs.GetInt("lc", -1);
            PlayerPrefs.SetInt("lc", status);
        }
        else if (option == Option.dm)
        {
            status = PlayerPrefs.GetInt("dm", -1);
            PlayerPrefs.SetInt("dm", status);
        }
        img.sprite = status == 1 ? on : off;

    }

    public void toggle()
    {
        if (option == Option.sound)
        {
            int status = PlayerPrefs.GetInt("sound");
            status *= -1;
            PlayerPrefs.SetInt("sound", status);
            img.sprite = status == 1 ? on : off;
        } else if (option == Option.music)
        {
            int status = PlayerPrefs.GetInt("music");
            status *= -1;
            PlayerPrefs.SetInt("music", status);
            img.sprite = status == 1 ? on : off;
            
        }
        else if (option == Option.lc)
        {
            int status = PlayerPrefs.GetInt("lc");
            status *= -1;
            PlayerPrefs.SetInt("lc", status);
            img.sprite = status == 1 ? on : off;
        }
        else if (option == Option.dm)
        {
            int status = PlayerPrefs.GetInt("dm");
            status *= -1;
            PlayerPrefs.SetInt("dm", status);
            img.sprite = status == 1 ? on : off;
        }
    }
}
