using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnHelper : MonoBehaviour
{
    [Header("Anim Start")]
    public Ease ease = Ease.InOutBounce;
    public float durationScale = 1.1f;

    public void Spawn(Transform t, float delay = .1f)
    {
        t.transform.localScale = Vector3.zero;
        t.transform.DOScale(Vector3.one, durationScale).SetDelay(delay).SetEase(ease);
    }

    public void Hide(Transform t, float delay = 0)
    {
        t.transform.DOScale(Vector3.zero, durationScale).SetDelay(delay).SetEase(ease);
    }
}
