using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioMixerGroup Mixer;
    [SerializeField] private GameObject backgroundToggle;
    [SerializeField] private GameObject effectToggle;

    private void Start()
    {

        if (backgroundToggle != null)
        {
            if (PlayerPrefs.GetInt("BackgroundMusic") == 0)
            {
                backgroundToggle.GetComponent<Toggle>().isOn = false;
            }
        }else
        {
            ChangeBackgroundSound(PlayerPrefs.GetInt("BackgroundMusic") == 0 ? false : true);
        }


        if (effectToggle != null)
        {
            if (PlayerPrefs.GetInt("EffectSound") == 0)
            {
                effectToggle.GetComponent<Toggle>().isOn = false;
            }
        }else
        {
            ChangeEffectSound(PlayerPrefs.GetInt("EffectSound") == 0 ? false : true);

        }

    }

    public void ChangeBackgroundSound(bool enabled)
    {
        if (enabled)
        {
            Mixer.audioMixer.SetFloat("BackgroundMusic", 0);
            PlayerPrefs.SetInt("BackgroundMusic", 1);
        }
        else
        {
            Mixer.audioMixer.SetFloat("BackgroundMusic", -80);
            PlayerPrefs.SetInt("BackgroundMusic", 0);
        }
    }

    public void ChangeEffectSound(bool enabled)
    {
        if (enabled)
        {
            Mixer.audioMixer.SetFloat("EffectSound", 0);
            PlayerPrefs.SetInt("EffectSound", 1);
        }
        else
        {
            Mixer.audioMixer.SetFloat("EffectSound", -80);
            PlayerPrefs.SetInt("EffectSound", 0);
        }
    }

    

}
