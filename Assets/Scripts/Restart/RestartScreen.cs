using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Ebac.Core.Singleton;

public class RestartScreen : Singleton<RestartScreen>
{

    [Header("Restart Text")]
    public TextMeshProUGUI restartText;

    //Arrived On Time or You're Fired
    public GameObject cogratulationsText;
    public GameObject failText; 

    [Header("Audio")]
    public AudioMenu sfxUI;

    [Header("Parameters")]
    private bool showIcon;
    private Ease ease = Ease.Linear;
    public Ease easeBounce = Ease.OutBounce;
    public Ease easeBack = Ease.OutBack;
    public Ease easeInBack = Ease.InBack;
    public bool showCoins;
    public float iconIn;
    private int index;
    private bool once;

    public enum EndType
    {
        Finsh,
        Obstacle,
        Car,
        Pedestrian
    }

    public List<EndTypeSetup> endSetup;
    private EndTypeSetup currEndSetup;

    void Update()
    {
        //Hide Icon and Show Coin Anim
        if(Input.GetKeyDown(KeyCode.Mouse0) && showIcon)
        {
            showIcon = false;
            HideIcon();   
            sfxUI.Disappear();
        }
    }

    public void ChooseEndType(EndType choseType, float delay = 1)
    {
        Settings.Instance.HideAndShowPauseButtonIcon();
        currEndSetup = endSetup.Find(x => choseType == x.endType);
        index = Random.Range(0, currEndSetup.phrase.Length);
        currEndSetup.icon.transform.DOMoveY(iconIn, .3f).SetEase(ease).SetDelay(delay).OnComplete(
            delegate
            {
                sfxUI.Appear();
                WinOrLoseText();
                currEndSetup.icon.transform.DOScale(Vector3.one, .5f).SetEase(easeBounce).OnComplete(
                    delegate
                    {
                        ShowText();
                    }
                );
            });
    }

    

    

    #region Restart Text

    void ShowText()
    {
        restartText.text = currEndSetup.phrase[index];
        restartText.transform.DOScale(Vector3.one, 1).SetEase(easeBack);
        WinOrLoseText();
    }

    void WinOrLoseText()
    {
        if(currEndSetup.endType == EndType.Finsh)
        cogratulationsText.transform.DOScale(Vector3.one, 1).SetEase(easeBack).OnComplete(
            delegate{showIcon = true;});
        else
        failText.transform.DOScale(Vector3.one, 1).SetEase(easeBack).OnComplete(
            delegate{showIcon = true;});
    }

    void HideIcon()
    {
        if(!once)
        {
            once = true;
            currEndSetup.icon.transform.DOScale(Vector3.zero, .7f).SetEase(easeInBack).OnComplete(
            delegate{showCoins = true;});
            restartText.transform.DOScale(Vector3.zero, .7f).SetEase(easeInBack);
            if(currEndSetup.endType == EndType.Finsh)
            cogratulationsText.transform.DOScale(Vector3.zero, .7f).SetEase(easeInBack);
            else
            failText.transform.DOScale(Vector3.zero, .7f).SetEase(easeInBack);
        }
    }

    #endregion

    void OnDestroy()
    {
        cogratulationsText?.transform.DOKill();
        failText.transform.DOKill();
        restartText?.transform.DOKill();
        currEndSetup?.icon.transform.DOKill();
    }
}

[System.Serializable]
public class EndTypeSetup
{
    public RestartScreen.EndType endType;
    public GameObject icon; 
    public string[] phrase;
}
