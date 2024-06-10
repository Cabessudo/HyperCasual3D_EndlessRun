using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource sfxOne;
    public AudioSource sfxTwo;
    public AudioSource song;

    [Header("SFX")]
    public AudioClip click;
    public AudioClip appear;
    public AudioClip disappear;
    public AudioClip cash;
    public AudioClip levelup;
    public AudioClip unlock;
    public AudioClip locked;
    public AudioClip bus;
    //Game 
    public AudioClip coin;

    public float volume = .1f;

    #region  SFX
    public void Appear()
    {
        sfxOne.PlayOneShot(appear);
    }

    public void Disappear()
    {
        sfxOne.PlayOneShot(disappear);
    }

    public IEnumerator AppearRoutine(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            sfxOne.PlayOneShot(appear, .1f);
            yield return new WaitForSeconds(volume);
        }
    }

    public IEnumerator DisappearRoutine(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            sfxOne.PlayOneShot(disappear, .8f);
            yield return new WaitForSeconds(volume);
        }
    }

    public void Click()
    {
        sfxOne.PlayOneShot(click, volume);
    }

    public void CoinClick()
    {
        sfxOne.PlayOneShot(coin, volume);
    }
    #endregion

    #region  Level Buttons
    public void Locked()
    {
        sfxOne.PlayOneShot(locked);
    }

    public void Unlock()
    {
        sfxOne.PlayOneShot(levelup, .3f);
        sfxTwo.PlayOneShot(unlock, .5f);
    }

    public void LevelUp()
    {
        sfxOne.PlayOneShot(cash);
        sfxTwo.PlayOneShot(levelup, .1f);
    }

    public void BusMoveSFX()
    {
        song.Stop();
        sfxTwo.PlayOneShot(bus);
    }

    #endregion


}
