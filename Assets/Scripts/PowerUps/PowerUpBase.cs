using System.Collections;
using System.Collections.Generic;
using Save;
using UnityEngine;

public class PowerUpBase : ItemCollatablesBase
{
    protected SaveManager save;
    public PowerupType pwupType;
    // public SOPowerupsLvls soPWUPLevel;
    protected MeshRenderer powerUpMesh;
    protected ParticleSystem particle;
    public float duration;
    public string particleTag;
    //First Time Collect PWUP
    public Sprite pwupSprite;
    public string pwupTxt;
    public int pwupRotation;

    public virtual void Start()
    {
        save = SaveManager.Instance?.GetComponent<SaveManager>();

        particle = GameObject.FindGameObjectWithTag(particleTag).GetComponent<ParticleSystem>();
        powerUpMesh = GetComponent<MeshRenderer>();

        for(int i = 0; i < save.GetPowerupSaveByType(pwupType).currLevel; i++)
        {
            duration += 1;
        }
    }

    public override void OnCollect()
    {
        StartPowerUp();
        save.GetPowerupSaveByType(pwupType).amountCollected++;
    }

    public override void Collect()
    {
        base.Collect();
    }

    protected virtual void StartPowerUp()
    {
        PlayerController.Instance.currPWUPSCollecteds++;
        Debug.Log("Start Power Up!!!");
        powerUpMesh.enabled = false;
        if(particle != null) particle.Play();
        Invoke(nameof(EndPowerUp), duration);
        Invoke(nameof(StopParticle), duration / 2);
        if(save.GetPowerupSaveByType(pwupType).amountCollected <= 0)
            ItemManager.Instance.FirstTimeCollect(pwupSprite, pwupTxt, pwupRotation);
    }

    protected virtual void EndPowerUp()
    {
        Debug.Log("End Power Up!!!");
        PlayerController.Instance.currPWUPSCollecteds--;
        Destroy(gameObject);
    }

    void StopParticle()
    {
        if(particle != null) particle.Stop();
    }
}
