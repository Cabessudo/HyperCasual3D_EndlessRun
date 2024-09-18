using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Save;


public class LevelButtons : MonoBehaviour
{
    private SaveManager save;
    public TransitionScene transition;
    
    [Header("Audio")]
    public AudioMenu sfx;

    [Header("Level")]
    public GameObject lvlButtonsCase;
    public LevelButtonsAnim anim;
    public int level;
    private bool once;

    [Header("Anim")]
    private float duration = .1f;
    private float scale = 1.1f;

    void Start()
    {
        save = SaveManager.Instance?.GetComponent<SaveManager>();
    }

    public void Interact()
    {
        if(!Settings.Instance.onSettings)
        {
            if(level > save.saveLayout.level.levelsUnlockeds)
            {
                sfx.Locked();
                anim.LockedButton(transform);
            }
            else if(level <= save.saveLayout.level.levelsUnlockeds && !once)
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
                SetLevel(); 
                Invoke(nameof(Load), 5);
            }   
        }
    }

    public void SetLevel()
    {
        if(level <= save.saveLayout.level.levelsUnlockeds)
        {
            save.saveLayout.level.currLevel = level;
            save.Save();
        }
    }

    void Load()
    {
        SceneManager.LoadScene(1);
    }
}
