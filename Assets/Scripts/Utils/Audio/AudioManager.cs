using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource sfxPlayer;
    public AudioSource sfxPWUP;
    public AudioSource sfxFirstTimeGetPWUP;
    public AudioSource sfxObstacles;

    [Header("SFX")]
    //PWUPs
    public AudioClip speedup;
    public AudioClip invencible;
    public AudioClip magnet;
    public AudioClip ballonInflation;
    public AudioClip ballonExplode;
    public AudioClip appearFirstPWUP;
    public AudioClip disappearFirstPWUP;

    //Obstacles
    public AudioClip explodeObstacle;
    public AudioClip explodeCar;
    public AudioClip carHonk;
    public AudioClip pedestrianHey;

    //Player
    public AudioClip hit;
    public AudioClip coin;

    public float volume = 0.1f;

    #region PWUPS
    public void SpeedUpSFX()
    {
        sfxPWUP.PlayOneShot(speedup, volume);
    }

    public void InvencibleSFX()
    {
        sfxPWUP.PlayOneShot(invencible, volume);
    }

    public void MagnetSFX()
    {
        sfxPWUP.PlayOneShot(magnet, volume);
    }

    public void ShowAndHideFirstTimeGetPWUPSFX(bool appear = true)
    {
        if(appear)
        sfxFirstTimeGetPWUP.PlayOneShot(appearFirstPWUP);
        else
        sfxFirstTimeGetPWUP.PlayOneShot(disappearFirstPWUP);
    }

    public void FlySFX(bool ballonAppear = true)
    {
        if(ballonAppear)
        sfxPWUP.PlayOneShot(ballonInflation);
        else
        sfxPWUP.PlayOneShot(ballonExplode, .1f);
    }
    #endregion

    #region Obstacles
    public void CarHonkSFX()
    {
        sfxObstacles.PlayOneShot(carHonk, volume);
    }

    public void ExplodeSFX(bool obstacle = false)
    {
        if(!obstacle)
        sfxObstacles.PlayOneShot(explodeCar, volume);
        else
        sfxObstacles.PlayOneShot(explodeObstacle, volume);
    }

    public void PedestrianSFX()
    {
        sfxObstacles.PlayOneShot(pedestrianHey, volume);
    }
    #endregion

    #region Player
    public void HitSFX()
    {
        sfxPlayer.PlayOneShot(hit, .25f);
    }

    public void CoinSFX()
    {
        sfxPlayer.PlayOneShot(coin);
    }

    #endregion
}
