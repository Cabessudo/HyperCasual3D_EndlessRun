using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Save
{
    public enum PowerupType
    {
        Speed_Up,
        Invencible,
        Magnet,
        Fly
    }

    [System.Serializable]
    public class PowerupSave : SaveBase
    {
        public PowerupType pwupType;
        //This script has to be in the charr button increase level, to know the curr level, 
        //if it's unlocked to trigger the anim and to upgrade the pwup
        public int amountCollected;
        public int currLevel;
        public bool unlock; //gets true when the amount it's equal or higher than one, to help trigger the unlock anim.
        public bool unlockAnim; //when unlocked, and press the charr button trigger the unlock anim.

        public override void Clear()
        {
            amountCollected = 0;
            currLevel = 0;
            unlock = false;
            unlockAnim = false;
        }
    }
}