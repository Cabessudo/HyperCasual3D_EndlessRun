using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFly : PowerUpBase
{
    [Header("Fly Parameters")]
    public DG.Tweening.Ease easeFly = DG.Tweening.Ease.OutBack;

    public CoinsFlyValues coinsFlyValues;
    public float flyHeight = 9;

    [Header("Change Color")]
    public List<Material> colors;
    public MeshRenderer mesh;

    [Header("Coins")]
    public GameObject coinCase;

    public override void Start()
    {
        base.Start();

        //Set Ballon Random Color
        mesh.material = colors[Random.Range(0, colors.Count)];
    }

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.Fly(flyHeight, duration, coinsFlyValues.delayToLand, easeFly);
        SpawnCoinCase();
    }

    void SpawnCoinCase()
    {
        var coinFly = Instantiate(coinCase);
        coinFly.transform.position = new Vector3(0, flyHeight, transform.position.z);
    }
}
