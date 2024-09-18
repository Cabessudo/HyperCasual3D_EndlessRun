using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFly : PowerUpBase
{
    [Header("Fly Parameters")]
    public DG.Tweening.Ease easeFly = DG.Tweening.Ease.OutBack;

    public float flyHeight = 11;
    public int coinsToAdd = 7;
    public float delayToLand = 1;

    [Header("Change Color")]
    public List<Material> colors;
    public MeshRenderer mesh;

    [Header("Coins")]
    public GameObject coinCase;

    public override void Start()
    {
        base.Start();

        //PWUP Fly Level
        for(int i = 0; i < save.GetPowerupSaveByType(pwupType).currLevel; i++)
        {
            duration += .25f;
            coinsToAdd += 4;
        }

        //Set Ballon Random Color
        mesh.material = colors[Random.Range(0, colors.Count)];
    }

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.Fly(flyHeight, duration, delayToLand, easeFly);
        SpawnCoinCase();
    }

    void SpawnCoinCase()
    {
        coinCase.SetActive(true);
    }
}
