using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Save;

[System.Serializable]
public class AudioSave : SaveBase
{
    public float musicVolume;
    public float soundVolume;

    public override void Clear()
    {
        musicVolume = 0;
        soundVolume = 0;
    }
}
