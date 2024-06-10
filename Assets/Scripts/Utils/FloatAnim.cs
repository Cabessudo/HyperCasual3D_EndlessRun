using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatAnim : MonoBehaviour
{
    public float y = 10;
    public float delay;
    private float duration = 1;
    public Ease ease = Ease.Linear;

    void Start()
    {
        transform.DOLocalMoveY(y, duration).SetDelay(delay).SetEase(ease).SetLoops(-1, LoopType.Yoyo).SetRelative();
    }
}
