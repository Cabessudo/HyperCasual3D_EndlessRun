using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Save;

public class CoinsFlyValues : MonoBehaviour
{
    
    public int coinsToAdd = 7;
    public float delayToLand = 1;

    void Start()
    {
        for(int i = 0; i < SaveManager.Instance?.GetPowerupSaveByType(PowerupType.Fly).currLevel; i++)
        {
            delayToLand += .7f;
            coinsToAdd += 3;
        }
    }
}
