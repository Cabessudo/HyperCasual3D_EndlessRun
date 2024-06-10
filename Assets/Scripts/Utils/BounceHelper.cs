using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BounceHelper : MonoBehaviour
{
    [Header("Bounce Anim")]
    public Ease ease = Ease.InOutBounce;
    public float bounceScale = 1.1f;
    public float bounceDuration = .1f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        Bounce();
    }

    public void Bounce()
    {
        transform.DOScale(bounceScale, bounceDuration).SetEase(ease).SetLoops(2, LoopType.Yoyo);
    }
}
