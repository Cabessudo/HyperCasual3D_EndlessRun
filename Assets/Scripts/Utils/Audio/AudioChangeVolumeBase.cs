using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Save;

public class AudioChangeVolumeBase : MonoBehaviour
{
    public SaveManager _save;
    public AudioMixer group;
    public string floatParam;
    public Slider slider;
    public TMPro.TextMeshProUGUI volumeText;
    protected int volumeIntText;
    // protected float normalizedVolume;

    public virtual void Start()
    {
        _save = SaveManager.Instance?.GetComponent<SaveManager>();
    }

    public void LoadVolume(float volume)
    {
        ChangeVolume(volume);
    }

    public virtual void ChangeVolume(float f)
    {
        float normalizedVolume = ((f + 60) / 60) * 100;
        group?.SetFloat(floatParam, f);
        volumeIntText = Mathf.RoundToInt(normalizedVolume);
        volumeText.SetText(volumeIntText.ToString());
        slider.value = f;
    }
}
