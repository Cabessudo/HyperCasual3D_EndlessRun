using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Save;

[System.Serializable]
public class CoinsSave : SaveBase
{
    public CoinsSave coinsSave;
    public int value;

    public override void Clear()
    {
        value = 0;
    }
}
