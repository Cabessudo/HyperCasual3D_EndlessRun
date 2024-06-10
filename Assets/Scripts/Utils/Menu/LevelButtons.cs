using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelButtons : MonoBehaviour
{
    public TransitionScene transition;
    
    [Header("Audio")]
    public AudioMenu sfx;

    [Header("Level")]
    public GameObject lvlButtonsCase;
    public LevelButtonsAnim anim;
    public SOLevelManager soLevelManager;
    public int level;
    private bool once;

    [Header("Anim")]
    private float duration = .1f;
    private float scale = 1.1f;

    public void Interact()
    {
        if(!Settings.Instance.onSettings)
        {
            if(level > soLevelManager.value)
            {
                sfx.Locked();
                anim.LockedButton(transform);
            }
            else if(level <= soLevelManager.value && !once)
            {
                Settings.Instance.HideAndShowPauseButtonIcon();
                sfx.Disappear();
                sfx.BusMoveSFX();
                once = true;

                foreach(var i in lvlButtonsCase.GetComponentsInChildren<Image>())
                anim.Hide(i, .25f);

                anim.BounceButton(transform, scale, duration);
                anim.MoveBusForwardAnim();
                transition.CloseScene(2);
                Invoke(nameof(Play), 5);
            }   
        }
    }

    public void Play()
    {
        if(level <= soLevelManager.value)
        {
            soLevelManager.currLevel = level;
            SceneManager.LoadScene(1);
        }
    }

    
}
