using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudioScript : MonoBehaviour
{
    public AudioClip button_select;
    public AudioClip theme;

    public AudioSource effects;
    public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        int soundOnOff = PlayerPrefs.GetInt("sound", 1);
        if (soundOnOff == 1)
        {
            effects.volume = 0.25f;
        }
        else
        {
            effects.volume = 0;
        }

        int musicOnOff = PlayerPrefs.GetInt("music", 1);
        if (musicOnOff == 1)
        {
            music.volume = 0.5f;
        }
        else
        {
            music.volume = 0;
        }
    }

    public void PlayButtonSelect()
    {
        effects.PlayOneShot(button_select);
    }
}
