using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInvencible : PowerUpBase
{
    protected override void StartPowerUp()
    {
        PlayerController.Instance.invencible = false;
        base.StartPowerUp();
        PlayerController.Instance.sfx.InvencibleSFX();
        PlayerController.Instance.invencible = true;
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.invencible = false;
    }
}
