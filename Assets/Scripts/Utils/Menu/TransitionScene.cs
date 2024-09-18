using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Save;

public class TransitionScene : MonoBehaviour
{
    public Ease ease = Ease.Linear;
    public GameObject above;
    public GameObject below;
    public float duration = .7f;
    public float delay = .5f;
    [SerializeField] private float move = 1000;

    void Start()
    {
        OpenScene();
    }

    public void OpenScene()
    {
        above.transform.DOMoveY(move, duration).SetDelay(delay).SetEase(ease).SetRelative();
        below.transform.DOMoveY(-move, duration).SetDelay(delay).SetEase(ease).SetRelative();
    }
    
    public void CloseScene(float delay = 0)
    {
        above.transform.DOMoveY(-move, duration).SetDelay(delay).SetEase(ease).SetRelative();
        below.transform.DOMoveY(move, duration).SetDelay(delay).SetEase(ease).SetRelative();
        SaveManager.Instance?.Save();
    }
}
