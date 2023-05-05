using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderValue : MonoBehaviour
{
    public Text sliderValue;
    public Slider slider;
    public int sliderNum;
    public AudioMixer mixer;

    private void Start()
    {
        float volume = 0;
        switch (sliderNum)
        {
            case 0:
                if (PlayerPrefs.HasKey("masterVolume"))
                {
                    volume = PlayerPrefs.GetFloat("masterVolume");
                    SetVolume(volume);
                }
                else
                {
                    volume = 1f;
                    SetVolume(volume);
                }
                break;
            case 1:
                if (PlayerPrefs.HasKey("musicVolume"))
                {
                    volume = PlayerPrefs.GetFloat("musicVolume");
                    SetVolume(volume);
                }
                else
                {
                    volume = 1f;
                    SetVolume(volume);
                }
                break;
            case 2:
                if (PlayerPrefs.HasKey("sfxVolume"))
                {
                    volume = PlayerPrefs.GetFloat("sfxVolume");
                    SetVolume(volume);
                }
                else
                {
                    volume = 1f;
                    SetVolume(volume);
                }
                break;
        }
        slider.value = volume;
    }
    // Update is called once per frame
    void Update()
    {
        sliderValue.text = (slider.value * 100).ToString("0");
    }


    public void SetVolume(float sliderValue)
    {
        if (sliderNum == 0)
        {
            mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("masterVolume", sliderValue);
        }
        else if (sliderNum == 1)
        {
            mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("musicVolume", sliderValue);
        }
        else if (sliderNum == 2)
        {
            mixer.SetFloat("FXVol", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("sfxVolume", sliderValue);
        }
        GameManager.Instance.SaveInfo();
    }

}
