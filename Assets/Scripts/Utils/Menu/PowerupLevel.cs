using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Save;
using DG.Tweening;

public class PowerupLevel : MonoBehaviour
{
    public PowerupType powerupType;
    private SaveManager save;
    public LevelButtonsAnim anim;

    [Header("Audio")]
    public AudioMenu sfx;
    public AudioSource sfxLock;
    public AudioSource sfxUnlock;

    [Header("PowerUp Level")]
    //Level
    public int level;
    public Image[] PWUPLvl;

    //Max Lvl
    public Image lvlMaxText;
    public Image lvlMaxTextColor;

    //Shop
    public Image PWUPButtonIcon;
    public GameObject ValueIcon;
    public TextMeshProUGUI increaseCoinText;
    public int PWUPLvlValue = 50;

    //Button
    private bool check;
    private bool canInteract;
    

    [Header("Anim")]
    //Button
    public float duration = 0.1f;
    public float scale = 1.1f;
    //PowerUp Level
    public Image[] lvlPWUPColor;
    //Button Increase Level
    public Image lockedButton;
    public Image lockImage;
    public Image lockedButtonColor;

    void Start()
    {
        save = SaveManager.Instance?.GetComponent<SaveManager>();
        CheckMaxLvl();        

        Invoke(nameof(FloatAnim), .5f);

        if(save.GetPowerupSaveByType(powerupType).unlockAnim)
        ButtonUnLocked();
    }

    void Update()
    {

        //after press the charr button, the unlock will become true, and if unlock anim is false will trigger the unlock anim
        if(save.GetPowerupSaveByType(powerupType).unlock && !save.GetPowerupSaveByType(powerupType).unlockAnim)
        Invoke(nameof(UnLockIncreasePWUP), 1); 

        ValueCoinText();

        if(!check)
        CheckPWUPLvl();        
    }

    public void Interact()
    {
        if(!Settings.Instance.onSettings)
        {
            if(PWUPLvlValue > save.saveLayout.coins.value || !canInteract || !save.GetPowerupSaveByType(powerupType).unlock)
            {
                sfx.Locked();
                anim.LockedButton(transform);
            }
            else if(PWUPLvlValue <= save.saveLayout.coins.value && canInteract && save.GetPowerupSaveByType(powerupType).unlock && !anim.once)
            {   
                sfx.LevelUp(); 
                anim.once = true;
                StartCoroutine(CoinAnim());
                anim.BounceButton(transform, scale, duration);     
                anim.UnlockedAnim(PWUPLvl[level], lvlPWUPColor[level], .3f);
                save.GetPowerupSaveByType(powerupType).currLevel += 1; 
                CheckPWUPLvl();
                CheckMaxLvl();     
            }
        }
    }

    //Show the value text of the PWUP and check the curr value depending of the curr lvl
    void ValueCoinText()
    {
        increaseCoinText.SetText("x " + PWUPLvlValue);

        for(int i = level; i < save.GetPowerupSaveByType(powerupType).currLevel; i++)
        {
            PWUPLvlValue += 25;
            level += 1;
        }
    }

    // let the color of the curr lvl appear 
    void CheckPWUPLvl()
    {
        for(int i = 0; i < level; i++)
        {
            lvlPWUPColor[i].enabled = false;
        }

        check = true;
    }

    //Check if the lvl is maxed and need to show max text, and hide the buttons and coin value, and if can interact 
    void CheckMaxLvl()
    {
        if(save.GetPowerupSaveByType(powerupType).currLevel >= PWUPLvl.Length)
        {
            canInteract = false;
            lvlMaxText.gameObject.SetActive(true);
            anim.UnlockedAnim(lvlMaxText, lvlMaxTextColor, .2f);
            PWUPButtonIcon.enabled = false;
            ValueIcon.SetActive(false);
        }
        else if(save.GetPowerupSaveByType(powerupType).currLevel < PWUPLvl.Length)
        canInteract = true;
    }

    #region Anim
    
    void UnLockIncreasePWUP()
    {
        //Unlock the button to increase the pwup level (anim)
        sfxLock.Play();
        sfxUnlock.Play();
        anim.UnLockIncreasePWUPLvl(lockedButton, lockedButtonColor, 1);
        anim.Unlock(lockImage, 20, 1);
        anim.SpawnBouncing(ValueIcon.transform, 1.3f);
        save.GetPowerupSaveByType(powerupType).unlockAnim = true; //To not triggered again
    }

    void ButtonUnLocked()
    {
        lockImage.enabled = false;
        lockedButtonColor.enabled = false;
        lockedButton.sprite = anim.trueIncreaseButton;
        ValueIcon.transform.localScale = Vector3.one;
    }

    void FloatAnim()
    {
        StartCoroutine(anim.FloatAnim(PWUPLvl));
    }

    public IEnumerator CoinAnim()
    {
        for(int i = PWUPLvlValue; i > 0; i--)
        {
            save.saveLayout.coins.value--;
            yield return new WaitForSeconds(.01f);
        }
    }

    void OnDestroy()
    {
        foreach(var p in PWUPLvl) p?.transform.DOKill();
        lockImage?.DOKill();
        ValueIcon?.transform.DOKill();
        lockedButton?.DOKill();
        lockedButtonColor?.DOKill();
        lvlMaxText?.DOKill();
        lvlMaxTextColor?.DOKill();
        transform.DOKill();
    }
    #endregion
}