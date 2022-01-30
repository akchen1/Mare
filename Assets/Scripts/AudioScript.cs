using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioScript : MonoBehaviour
{
    public AudioClip phase_switch;
    public AudioClip shoot;
    public AudioClip object_destroyed;
    public AudioClip object_hit;
    public AudioClip player_hit;
    public AudioClip light_theme;
    public AudioClip dark_theme;

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

    public void PlayPhaseSwitch()
    {
        effects.PlayOneShot(phase_switch);
    }

    public void PlayShoot()
    {
        effects.PlayOneShot(shoot);
    }

    public void PlayObjectDestroyed()
    {
        effects.PlayOneShot(object_destroyed);
    }

    public void PlayObjectHit()
    {
        effects.PlayOneShot(object_hit);
    }

    public void PlayPlayerHit()
    {
        effects.PlayOneShot(player_hit);
    }

    public void SwitchTracks()
    {
        float trackTime = music.time;

        if (music.clip == light_theme)
        {
            music.clip = dark_theme;
        }
        else if (music.clip == dark_theme)
        {
            music.clip = light_theme;
        }

        music.time = trackTime;
        music.Play();
    }
}
