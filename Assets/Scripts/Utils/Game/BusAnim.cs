using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BusAnim : MonoBehaviour
{
    public Transform Followbus;
    public Ease ease = Ease.Linear;
    public Ease easeTest;
    public float z = 50;
    public bool startBus;
    private bool cutscene = true;
    private float delay = 2;
    private string playerTag = "Player";

    [Header("Audio")]
    private AudioMenu sfx;
    public AudioSource getBusSFX;

    // Start is called before the first frame update
    void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioMenu>();
        
        if(startBus)
        {
            transform.DOLocalMoveZ(z, 9).SetEase(ease).SetDelay(.5f);
            transform.DOScale(Vector3.zero, .1f).SetDelay(5); 
        }
        else
        transform.DOScaleY(2.01f, .1f).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
    }

    void Update()
    {
        if(cutscene)
        {
            Followbus.position = new Vector3(transform.position.x, Followbus.position.y, transform.position.z);
            Invoke(nameof(StopFollow), delay);
        }
    }

    void StopFollow()
    {
        cutscene = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(playerTag))
        {
            z = 50;
            getBusSFX.Play();
            sfx.BusMoveSFX();
            transform.DOScaleY(2.05f, .5f).SetEase(easeTest).SetLoops(2, LoopType.Yoyo).OnComplete(
                delegate
                {
                    transform.DOLocalMoveZ(75, 1).SetEase(ease);
                    
                });
        }
    }    
}
