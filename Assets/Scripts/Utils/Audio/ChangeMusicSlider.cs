using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusicSlider : AudioChangeVolumeBase
{

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        LoadVolume(_save.saveLayout.audio.musicVolume);
    }

    public override void ChangeVolume(float f)
    {
        base.ChangeVolume(f);
        _save.saveLayout.audio.musicVolume = f;
    }   
}
