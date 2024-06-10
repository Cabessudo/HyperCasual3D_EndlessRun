using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFly : PowerUpBase
{
    [Header("Fly")]
    public DG.Tweening.Ease easeFly = DG.Tweening.Ease.OutBack;
    public SOFlyPowerUp SOFly;
    public float flyHeight = 3;
    private int coinsToAddDefault = 7;

    [Header("Change Color")]
    public List<Material> colors;
    public MeshRenderer mesh;

    [Header("Coins")]
    public GameObject coinCase;

    public override void Start()
    {
        base.Start();

        //PWUP Fly Level
        SOFly.coinsToAdd = coinsToAddDefault;
        SOFly.delayToLand = duration;
        for(int i = 0; i < soPWUPLevel.currLevel; i++)
        {
            SOFly.delayToLand += .25f;
            SOFly.coinsToAdd += 4;
        }

        //Set Ballon Random Color
        mesh.material = colors[Random.Range(0, colors.Count)];
    }

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.Fly(flyHeight, SOFly.flyDuration, SOFly.delayToLand, easeFly);
        SpawnCoinCase();
    }

    void SpawnCoinCase()
    {
        Vector3 coinPos = new Vector3(0, flyHeight + 2.5f, transform.position.z);
        Instantiate(coinCase, coinPos, coinCase.transform.rotation);
    }
}
