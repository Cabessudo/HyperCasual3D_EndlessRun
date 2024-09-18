using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusicSlider : AudioChangeVolumeBase
{

    // Start is called before the first frame update
    void Start()
    {
        LoadVolume(audioSave.musicVolume);
    }

    public override void ChageVolume(float f)
    {
        base.ChageVolume(f);
        audioSave.musicVolume = f;
    }   
}
