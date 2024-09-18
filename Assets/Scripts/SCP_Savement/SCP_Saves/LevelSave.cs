using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Save;

[System.Serializable]
public class LevelSave : SaveBase
{
    public int levelsUnlockeds; //Just to check if the level is higher than the levels available to not enter
    public int currLevel; //To spawn the Pieces of this level, in the Item Manager.

    public override void Clear()
    {
        levelsUnlockeds = 0;
        currLevel = 0;
    }
}
