using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioChangeVolume : MonoBehaviour
{
    public AudioMixer group;
    public string floatParam;
    public Slider slider;
    public SOInt volumeType;
    public TMPro.TextMeshProUGUI volumeText;
    private int volume;

    void Update()
    {
        volume = volumeType.value + 80;
        volumeText.SetText("" + volume);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Load the volume, and change the volume cause of the move of the slider
        slider.value = volumeType.value;
    }

    public void ChageVolume(float f)
    {
        group.SetFloat(floatParam, f);
        volumeType.value = Mathf.RoundToInt(f);
    }
}
