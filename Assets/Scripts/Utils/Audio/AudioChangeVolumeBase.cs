using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Save;

public class AudioChangeVolumeBase : MonoBehaviour
{
    public AudioMixer group;
    public string floatParam;
    public Slider slider;
    public AudioSave audioSave;
    public TMPro.TextMeshProUGUI volumeText;
    protected int volumeIntText;

    public void LoadVolume(float volume)
    {
        slider.value = volume; 
    }

    public virtual void ChageVolume(float f)
    {
        group?.SetFloat(floatParam, f);
        volumeIntText = (int)f + 80;
        volumeText.SetText("" + volumeIntText);
    }

    void OnDestroy()
    {
        // SaveManager.Instance?.SaveInTheListByTheType(SaveType.Audio);
    }
}
