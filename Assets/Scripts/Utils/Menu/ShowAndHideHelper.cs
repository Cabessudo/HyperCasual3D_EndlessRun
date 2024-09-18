using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShowAndHideHelper : MonoBehaviour
{
    public float showX;
    public float hideX;

    [Header("Anim Parameters")]
    private Ease ease = Ease.Linear;
    public float duration;

    public void Show()
    {
        transform.DOLocalMoveX(showX, duration).SetEase(ease);
    }

    public void Hide()
    {
        transform.DOLocalMoveX(hideX, duration).SetEase(ease);
    }
}
