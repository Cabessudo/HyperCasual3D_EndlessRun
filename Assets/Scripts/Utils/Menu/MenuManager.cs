using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    
    public SOLevelManager soLevelManager;

    [Header("Audio")]
    public AudioMenu sfx;

    [Header("Buttons")]
    //Level
    public GameObject mainButtons;
    public GameObject levelsButtonsCase;
    public LevelButtonsSetups[] lvlButtonSetup;
    public int buttonsAvailable;

    //PWUP Level
    public GameObject[] currCoin;
    public GameObject[] charPWUPs;
    public Image[] iconsCase;

    [Header("Anim")]
    public LevelButtonsAnim anim;
    public MenuCam cam;
    public SpawnHelper spawn;
    public Ease ease = Ease.Linear;
    public float duration = 1;
    public float delayToShow = 1;
    //PWUP Level
    private float inX = -375;
    private float outX = -1050;
    //UnLock to Increase PWUP's
    public SOPowerupsLvls[] soPowerupsLvls;
    
    

    void Start()
    {
        StartCoroutine(anim.FloatAnim(iconsCase));
        foreach(var lvlButtons in levelsButtonsCase.GetComponentsInChildren<Image>())
        Invoke(nameof(LevelUnlocked), 1);
        StartCoroutine(anim.FloatAnim(iconsCase));
    }

    public void ShowLevels(bool main)
    {
        //if main is false show lvls buttons else show the main buttons
        if(!Settings.Instance.onSettings)
        {
            if(!main)
            {
                Invoke(nameof(UnlockLvl), 0);
                sfx.Appear();
                levelsButtonsCase.SetActive(true);
                foreach(var l in levelsButtonsCase.GetComponentsInChildren<Image>())
                spawn.Spawn(l.transform);
                foreach(var b in mainButtons.GetComponentsInChildren<Image>())
                anim.Hide(b, duration);
            }
            else
            {
                sfx.Disappear();
                foreach(var b in mainButtons.GetComponentsInChildren<Image>())
                spawn.Spawn(b.transform);
                foreach(var l in levelsButtonsCase.GetComponentsInChildren<Image>())
                anim.Hide(l, duration);
            }
        }
        
    }

    public void ShowCharPWUP(bool main)
    {
        if(!Settings.Instance.onSettings)
        {
            if(main)
            {
                StartCoroutine(sfx.DisappearRoutine(charPWUPs.Length));
                cam.ChangeCam(main);
                StartCoroutine(anim.ShowAndHideCharPWUPs(charPWUPs, outX, .1f));
                StartCoroutine(anim.ShowAndHideCharPWUPs(currCoin, 750, .3f));
                foreach(var b in mainButtons.GetComponentsInChildren<Image>())
                spawn.Spawn(b.transform, delayToShow);
            }
            else
            {
                StartCoroutine(sfx.AppearRoutine(charPWUPs.Length));
                cam.ChangeCam(main);
                StartCoroutine(anim.ShowAndHideCharPWUPs(charPWUPs, inX, .1f));
                StartCoroutine(anim.ShowAndHideCharPWUPs(currCoin, 450, .3f));
                UnlockPWUPLvl();
                foreach(var b in mainButtons.GetComponentsInChildren<Image>())
                anim.Hide(b, duration);
            }
        }
    }

    void UnlockPWUPLvl()
    {
        foreach(var pwups in soPowerupsLvls)
        {
            if(pwups.amountCollected >= 1)
            pwups.unlock = true;
        }
    }

    void UnlockLvl()
    {
        for(int i = 0; i < soLevelManager.value; i++)
        {
            if(!lvlButtonSetup[i].unlocked.b)
            {
                lvlButtonSetup[i].unlocked.b = true;
                Invoke(nameof(UnlockSFX), .8f);
                anim.UnlockedAnim(lvlButtonSetup[i].levelIcon, lvlButtonSetup[i].lockedButtonColor, .5f, 1);
                anim.Unlock(lvlButtonSetup[i].lockImage, 50, 1);
            }
        }
    }

    public void LevelUnlocked()
    {
        for(int i = 0; i < soLevelManager.value; i++)
        {
            if(lvlButtonSetup[i].unlocked.b)
            {
                lvlButtonSetup[i].lockImage.enabled = false;
                lvlButtonSetup[i].lockedButtonColor.enabled = false;
            }
        }
        
    }

    void UnlockSFX()
    {
        sfx.Unlock();
    }

    [System.Serializable]
    public class LevelButtonsSetups
    {
        public Image levelIcon;
        public Image lockImage;
        public Image lockedButtonColor;
        public SOBool unlocked;
    }
}
