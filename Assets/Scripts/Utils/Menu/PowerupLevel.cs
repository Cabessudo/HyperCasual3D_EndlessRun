using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerupLevel : MonoBehaviour
{
    public LevelButtonsAnim anim;

    [Header("Audio")]
    public AudioMenu sfx;
    public AudioSource sfxLock;
    public AudioSource sfxUnlock;

    [Header("PowerUp Level")]
    //Level
    public int level;
    private int nextLevel = 1;
    public SOPowerupsLvls soPowerupsLvls;
    public Image[] PWUPLvl;

    //Max Lvl
    public Image lvlMaxText;
    public Image lvlMaxTextColor;

    //Shop
    public Image PWUPButtonIcon;
    public GameObject ValueIcon;
    public SOInt soCoins;
    public TextMeshProUGUI increaseCoinText;
    public int PWUPLvlValue = 50;

    //Button
    private bool once;
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
        CheckMaxLvl();        

        Invoke(nameof(FloatAnim), .5f);

        if(soPowerupsLvls.unlockAnim)
        ButtonUnLocked();
    }

    void Update()
    {
        if(soPowerupsLvls.unlock && !soPowerupsLvls.unlockAnim)
        Invoke(nameof(UnLockIncreasePWUP), 1);


        ValueCoinText();

        if(!check)
        CheckPWUPLvl();        
    }

    public void Interact()
    {
        if(!Settings.Instance.onSettings)
        {
            if(PWUPLvlValue > soCoins.value || !canInteract || !soPowerupsLvls.unlock)
            {
                sfx.Locked();
                anim.LockedButton(transform);
            }
            else if(PWUPLvlValue <= soCoins.value && canInteract && soPowerupsLvls.unlock && !anim.once)
            {   
                sfx.LevelUp(); 
                anim.once = true;
                StartCoroutine(CoinAnim());
                anim.BounceButton(transform, scale, duration);     
                anim.UnlockedAnim(PWUPLvl[level], lvlPWUPColor[level], .3f);
                soPowerupsLvls.currLevel += 1; 
                CheckPWUPLvl();
                CheckMaxLvl();        
            }
        }
    }

    //Show the value text of the PWUP and check the curr value depending of the curr lvl
    void ValueCoinText()
    {
        increaseCoinText.SetText("x " + PWUPLvlValue);

        for(int i = level; i < soPowerupsLvls.currLevel; i++)
        {
            PWUPLvlValue += 25;
            level += nextLevel;
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
        if(soPowerupsLvls.currLevel >= PWUPLvl.Length)
        {
            canInteract = false;
            lvlMaxText.gameObject.SetActive(true);
            anim.UnlockedAnim(lvlMaxText, lvlMaxTextColor, .2f);
            PWUPButtonIcon.enabled = false;
            ValueIcon.SetActive(false);
        }
        else if(soPowerupsLvls.currLevel < PWUPLvl.Length)
        canInteract = true;
    }
    
    void UnLockIncreasePWUP()
    {
        sfxLock.Play();
        sfxUnlock.Play();
        anim.UnLockIncreasePWUPLvl(lockedButton, lockedButtonColor, 1);
        anim.Unlock(lockImage, 20, 1);
        anim.SpawnBouncing(ValueIcon.transform, 1.3f);
        soPowerupsLvls.unlockAnim = true;
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
            soCoins.value--;
            yield return new WaitForSeconds(.01f);
        }
    }
}