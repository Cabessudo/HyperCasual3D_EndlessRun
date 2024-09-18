using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Ebac.Core.Singleton;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Settings : Singleton<Settings>
{

    [Header("Audio")]
    public AudioMenu sfx;

    [Header("Settings")]
    //In Game
    public GameObject pauseButton;
    public TMPro.TextMeshProUGUI cantOpenWarn;
    public LoadScene restart;
    
    //Pause
    public GameObject pauseCase;
    public GameObject engine;
    public List<GameObject> image;
    public GameObject backGround;

    //Audio Case
    public GameObject AudioCase;
    public List<GameObject> backAudioButton;

    //Parameters
    private Ease easeLinear = Ease.Linear;
    private Ease easeBack = Ease.OutBack;
    private float duration = .1f;
    private int index;
    private float whiteColor = 225;
    private int zero = 0;
    private int one = 1;
    public bool onSettings;

    #region Menu
    public void Hide()
    {
        if(onSettings)
        {
            sfx.Click();
            backGround.SetActive(false);
            StartCoroutine(HideAnim(image));
            onSettings = false;
        }
    }

    public void Show()
    {
        if(!onSettings)
        {
            sfx.Click();
            backGround.SetActive(true);
            onSettings = true;
            StartCoroutine(ShowAnim(image));
            if(engine != null) SpinEngine();
        }
    }

    void SpinEngine()
    {
        engine.transform.DORotate(new Vector3(zero,zero,-260), .5f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
    }

    public  void HideAndShowPauseButtonIcon(bool show = false)
    {
        if(engine != null)
        engine.SetActive(show);
        if(pauseButton != null)
        pauseButton.SetActive(show);
    }
    #endregion

    #region In Game
    public void ShowInGame()
    {
        if(!PlayerController.Instance.hasPWUP && !PlayerController.Instance.isFlying)
        Show();
        else
        Warn();
    }

    void Warn()
    {
        sfx.Locked();
        cantOpenWarn.color = new Color(whiteColor, whiteColor, whiteColor, one);
        cantOpenWarn.DOColor(new Color(whiteColor, whiteColor, whiteColor, zero), one).SetDelay(one).SetEase(easeLinear);
    }
    #endregion

    #region Settings Buttons
    IEnumerator HideAnim(List<GameObject> obj, float delay = 0)
    {
        for(int i = obj.Count; i > 0; i--)
        {
            index = i - 1;
            obj[index].transform.DORotate(new Vector3(-90, zero, zero), duration).SetDelay(delay).SetEase(easeLinear);

            StartCoroutine(sfx.DisappearRoutine(obj.Count));
            yield return new WaitForSeconds(duration);
        }
    }

    IEnumerator ShowAnim(List<GameObject> obj, float delay = 0)
    {
        
        for(int i = 0; i < obj.Count; i++)
        {
            StartCoroutine(sfx.AppearRoutine(obj.Count));
            index = i;
            obj[index].transform.DORotate(new Vector3(zero, zero, zero), duration).SetDelay(delay).SetEase(easeLinear);

            yield return new WaitForSeconds(duration);
        }
    }


    public void ShowAndHideAudioCase(bool show)
    {
        if(show)
        {
            pauseCase.transform.DOScale(0, duration).SetEase(easeBack).OnComplete(
                delegate
                {
                    AudioCase.transform.DOScale(1, duration).SetEase(easeBack).OnComplete(
                    delegate
                    {
                        StartCoroutine(ShowAnim(backAudioButton, duration));
                    });
                });
            
        }
        else
        {
            StartCoroutine(HideAnim(backAudioButton));
            AudioCase.transform.DOScale(zero, duration).SetDelay(duration).SetEase(easeBack).OnComplete(
                delegate
                {
                    pauseCase.transform.DOScale(1, duration).SetEase(easeBack);
                }
            );
        }
    }

    public void Restart()
    {
        sfx.Click();
        restart.Restart();
    }

    public void Exit()
    {
        sfx.Click();
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif 
    }
    #endregion
}
