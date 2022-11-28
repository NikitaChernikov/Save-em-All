using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private float musicLvl;
    [SerializeField] private GameObject[] soundImage;

    private bool isSound = true;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MainMusic"))
        {
            if (PlayerPrefs.GetFloat("MainMusic") == -80)
            {
                isSound = false;
                soundImage[0].SetActive(false);
                soundImage[1].SetActive(true);
            }
            masterMixer.SetFloat("Main", PlayerPrefs.GetFloat("MainMusic"));
        }
        else
        {
            masterMixer.SetFloat("Main", 0);
        }
    }

    public void SoundOnOff()
    {
        if (isSound)
        {
            isSound = false;
            soundImage[0].SetActive(false);
            soundImage[1].SetActive(true);
            musicLvl = -80;
            SetMusicVolume();
        }
        else
        {
            isSound = true;
            soundImage[0].SetActive(true);
            soundImage[1].SetActive(false);
            musicLvl = 0;
            SetMusicVolume();
        }
    }

    private void SetMusicVolume()
    {
        masterMixer.SetFloat("Main", musicLvl);
        PlayerPrefs.SetFloat("MainMusic", musicLvl);
    }

}
