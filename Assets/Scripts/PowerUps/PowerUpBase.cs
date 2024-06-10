using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : ItemCollatablesBase
{
    public SOPowerupsLvls soPWUPLevel;
    public MeshRenderer powerUp;
    protected ParticleSystem particle;
    public float duration;
    public string particleTag;
    //First Time Collect PWUP
    public Sprite pwupSprite;
    public string pwupTxt;
    public int pwupRotation;

    public virtual void Start()
    {
        particle = GameObject.FindGameObjectWithTag(particleTag).GetComponent<ParticleSystem>();
        powerUp = GetComponent<MeshRenderer>();

        for(int i = 0; i < soPWUPLevel.currLevel; i++)
        {
            duration += 1;
        }
    }

    public override void OnCollect()
    {
        StartPowerUp();
        soPWUPLevel.amountCollected++;
    }

    public override void Collect()
    {
        base.Collect();
        if(obj != null)
        {
            obj.SetActive(false);
        }
    }

    protected virtual void StartPowerUp()
    {
        PlayerController.Instance.currPWUPSCollecteds++;
        Debug.Log("Start Power Up!!!");
        powerUp.enabled = false;
        if(particle != null) particle.Play();
        Invoke(nameof(EndPowerUp), duration);
        Invoke(nameof(StopParticle), duration / 2);
        if(soPWUPLevel.amountCollected <= 0)
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
