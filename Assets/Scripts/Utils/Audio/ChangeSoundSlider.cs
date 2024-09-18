using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSoundSlider : AudioChangeVolumeBase
{
    // Start is called before the first frame update
    void Start()
    {
        LoadVolume(audioSave.soundVolume);
    }

    public override void ChageVolume(float f)
    {
        base.ChageVolume(f);
        audioSave.soundVolume = f;
    }
}
