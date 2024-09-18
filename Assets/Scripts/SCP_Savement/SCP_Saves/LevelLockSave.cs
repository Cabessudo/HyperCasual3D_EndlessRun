using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Save
{
    public enum LevelType
    {
        Level_02,
        Level_03,
        Level_04,
        Level_05,
    }

    [System.Serializable]
    public class LevelLockSave : SaveBase
    {
        public LevelType levelType;
        //This script has to be in the level button lock, to know if, it's curr level button is locked or not.
        public bool unlocked;

        public override void Clear()
        {
            unlocked = false;
        }
    }
}
