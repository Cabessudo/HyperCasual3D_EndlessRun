using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollectCoin : PowerUpBase
{
    [Header("Collect Coin")]
    public float sizeAmout;

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.Magnet(true, sizeAmout);
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.Magnet(false);
    }
}
