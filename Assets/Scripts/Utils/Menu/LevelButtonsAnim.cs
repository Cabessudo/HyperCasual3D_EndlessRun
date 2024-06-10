using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LevelButtonsAnim : MonoBehaviour
{
    [Header("Increase Button Unlock")]
    private Ease ease = Ease.OutBack;
    public Sprite trueIncreaseButton;
    public Sprite unlock;
    [Header("Play Anim")]
    private Ease easeLinear = Ease.Linear;
    public Transform bus;
    public float busDuration = 1; 
    private bool isMoving;
    private float alphaColor = 0;
    private float white = 255;
    private float delay = .1f;

    //Float
    private float y = 10;
    private float x = -50;

    //Bool
    public bool once;

    public void LockedButton(Transform t)
    {
        if(!isMoving)
        {
            isMoving = true;
            t.DORotate(new Vector3(0, 0, -10), .1f).SetLoops(2, LoopType.Yoyo).SetEase(easeLinear).OnComplete(
                delegate
                {
                    t.DORotate(new Vector3(0, 0, -10), .1f).SetLoops(2, LoopType.Yoyo).SetEase(easeLinear);
                    isMoving = false;
                    t.rotation = Quaternion.Euler(Vector3.zero);
                });
        }
    }

    public void BounceButton(Transform transform, float scale, float duration)
    {
        if(!isMoving)
        {
            isMoving = true;
            transform.DOScale(scale, duration).SetEase(easeLinear).SetLoops(2, LoopType.Yoyo).OnComplete(
                delegate
                {
                    transform.localScale = Vector3.one;
                    isMoving = false;
                });
        }
    }

    //Bounce the object and let the color white and decrease the alpha color
    public void UnlockedAnim(Image mainImage, Image color, float duration, float delay = 0)
    {
        mainImage.transform.DOScale(1.1f, 0.1f).SetDelay(delay).SetEase(easeLinear).OnComplete(
            delegate
            {
                color.DOColor(new Color(white,white,white), duration).SetEase(easeLinear).OnComplete(
                    delegate
                    {
                        color.DOColor(new Color(white,white,white, alphaColor), duration).SetEase(easeLinear).OnComplete(
                            delegate
                            {
                                mainImage.transform.DOScale(1, 0.1f).SetEase(easeLinear).OnComplete(
                                    delegate
                                    {
                                        once = false;    
                                        color.enabled = false;
                                    });       
                            });
                    });
            });
    }

    public void UnLockIncreasePWUPLvl(Image button, Image color, float delay = 0)
    {
        button.transform.DOScale(1.3f, .5f).SetDelay(delay).SetEase(easeLinear);
        color.DOColor(new Color(white, white, white), .5f).SetDelay(delay).SetEase(easeLinear).OnComplete(
            delegate
            {
                color.DOColor(new Color(white, white, white, alphaColor), .5f).SetEase(easeLinear);
                button.sprite = trueIncreaseButton;
                button.transform.DOScale(Vector3.one, .5f);
            }
        );
    }

    public void Unlock(Image lockImage, float y, float delay = 0)
    {
        lockImage.transform.DOScale(1.3f, .1f).SetDelay(delay).SetEase(easeLinear).OnComplete(
            delegate
            {
                lockImage.sprite = unlock;
                lockImage.transform.DORotate(new Vector3(0, 0, -10), .1f).SetEase(easeLinear).OnComplete(
                    delegate
                    {
                        lockImage.transform.DOLocalMoveY(y, .2f).SetEase(easeLinear).SetRelative().OnComplete(
                            delegate
                            {
                                lockImage.transform.DOLocalMoveY(-y, .2f).SetEase(easeLinear).SetRelative().OnComplete(
                                delegate
                                {lockImage.gameObject.SetActive(false);});
                            });
                    });
            });
    }

    public void Shake(Transform t, float delay = 0)
    {
        t.DORotate(new Vector3(0, 0, -10), .1f).SetDelay(delay).SetLoops(2, LoopType.Yoyo).SetEase(easeLinear).OnComplete(
            delegate
            {
                t.DORotate(new Vector3(0, 0, -10), .1f).SetLoops(2, LoopType.Yoyo).SetEase(easeLinear).OnComplete(
                    delegate
                    {
                        t.rotation = Quaternion.Euler(new Vector3(0, 0, 300));
                    });

            });
    }

    public void SpawnBouncing(Transform t, float delay)
    {
        t.DOScale(Vector3.one, 1).SetEase(ease).SetDelay(delay);
    }

    public void Hide(Image i, float duration)
    {
        DOTween.Kill(i);
        i.transform.DOKill();
        i.transform.DOScale(Vector3.zero, duration).SetEase(easeLinear);
    }

    public void MoveBusForwardAnim()
    {
        bus.DOMoveX(x, busDuration).SetEase(easeLinear);
    }

    public IEnumerator FloatAnim(Image[] image)
    {
        for(int i = 0; i < image.Length; i++)
        {
            image[i].transform.DOLocalMoveY(y, 1).SetEase(easeLinear).SetLoops(-1, LoopType.Yoyo).SetRelative();
            yield return new WaitForSeconds(delay);
        }
    }

    public IEnumerator ShowAndHideCharPWUPs(GameObject[] obj, float x, float duration = 1)
    {
        for(int i = 0; i < obj.Length; i++)
        {
            obj[i].transform.DOLocalMoveX(x, duration).SetEase(easeLinear);
            yield return new WaitForSeconds(delay);
        }
    }
}
