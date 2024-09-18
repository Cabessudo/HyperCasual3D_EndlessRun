using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Save;
using System.Linq;

public class MenuManager : MonoBehaviour
{
    private SaveManager save;

    [Header("Audio")]
    public AudioMenu sfx;

    [Header("Buttons")]
    //Level
    public GameObject mainButtons;
    public GameObject levelsButtonsCase;
    public LevelButtonsSetups[] lvlButtonSetup;
    public int buttonsAvailable;

    //PWUP Level
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
    private float inPwupX = 0;
    private float outPwupX = -1000;
    

    void Start()
    {
        save = SaveManager.Instance?.GetComponent<SaveManager>();
        
        StartCoroutine(anim.FloatAnim(iconsCase));
        foreach(var lvlButtons in levelsButtonsCase.GetComponentsInChildren<Image>())
        Invoke(nameof(LevelUnlocked), 1);
        StartCoroutine(anim.FloatAnim(iconsCase));
    }

    #region  Level Buttons
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

    void UnlockLvl()
    {
        for(int i = 0; i < save.saveLayout.level.levelsUnlockeds; i++)
        {
            if(!save.GetLevelLockByType(lvlButtonSetup[i].lockLevel).unlocked)
            {
                save.GetLevelLockByType(lvlButtonSetup[i].lockLevel).unlocked = true;
                Invoke(nameof(UnlockSFX), .8f);
                anim.UnlockedAnim(lvlButtonSetup[i].levelIcon, lvlButtonSetup[i].lockedButtonColor, .5f, 1);
                anim.Unlock(lvlButtonSetup[i].lockImage, 50, 1);
            }
        }
    }

    public void LevelUnlocked()
    {
        for(int i = 0; i < save.saveLayout.level.levelsUnlockeds; i++)
        {
            if(save.GetLevelLockByType(lvlButtonSetup[i].lockLevel).unlocked)
            {
                lvlButtonSetup[i].lockImage.enabled = false;
                lvlButtonSetup[i].lockedButtonColor.enabled = false;
            }
        }
        
    }

    #endregion

    #region  Powerup Buttons
    public void ShowCharPWUP(bool main)
    {
        if(!Settings.Instance.onSettings)
        {
            if(main)
            {
                StartCoroutine(sfx.DisappearRoutine(charPWUPs.Length));
                cam.ChangeCam(main);
                StartCoroutine(anim.ShowAndHideCharPWUPs(charPWUPs, outPwupX, .1f));
                foreach(var b in mainButtons.GetComponentsInChildren<Image>())
                spawn.Spawn(b.transform, delayToShow);
            }
            else
            {
                StartCoroutine(sfx.AppearRoutine(charPWUPs.Length));
                cam.ChangeCam(main);
                StartCoroutine(anim.ShowAndHideCharPWUPs(charPWUPs, inPwupX, .1f));
                UnlockPWUPLvl(); //When go to the character pwups, check if the pwup has alredy get taken 
                foreach(var b in mainButtons.GetComponentsInChildren<Image>())
                anim.Hide(b, duration);
            }
        }
    }

    void UnlockPWUPLvl()
    {
        //Its's only triggered after press the charr button to show it
        foreach(var pwups in save.saveLayout.powerups)
        {
            if(pwups.amountCollected >= 1) 
            pwups.unlock = true;
        }
    }

    #endregion
    

    void UnlockSFX()
    {
        sfx.Unlock();
    }

    void OnDestroy()
    {
        foreach(var c in charPWUPs) c?.transform.DOKill();
        foreach(var i in iconsCase) i?.transform.DOKill();
    }

    [System.Serializable]
    public class LevelButtonsSetups
    {
        public LevelType lockLevel;
        public Image levelIcon;
        public Image lockImage;
        public Image lockedButtonColor;
    }
}
