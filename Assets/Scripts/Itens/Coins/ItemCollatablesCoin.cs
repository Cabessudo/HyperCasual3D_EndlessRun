using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollatablesCoin : ItemCollatablesBase
{
    public ParticleSystem coinSFX;
    private MeshRenderer render;
    public float lerpSpeed = 3;
    public float timeToDestroy = 1;
    private float minDis = 1.5f;
    private bool _collect;

    [Header("Audio")]
    private AudioManager sfx;

    void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        if(CoinsAnimation.Instance != null)
        CoinsAnimation.Instance.RegisterCoins(this);

        render = GetComponentInChildren<MeshRenderer>();
    }

    public override void Collect()
    {
        base.Collect();
        sfx.CoinSFX();
    }
    public override void OnCollect()
    {
        _collect = true;
    }

    void Update()
    {
        if(_collect)
        {
            transform.position = Vector3.Lerp(transform.position, PlayerController.Instance.transform.position, lerpSpeed * Time.deltaTime);

            if(Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < minDis)
            {
                Collected();
            }
        }
    }

    void Collected()
    {
        _collect = false;
        render.enabled = false;
        coinSFX.Play();
        ItemManager.Instance.AddCoins();
        Destroy(gameObject, timeToDestroy);
    }

    /*void Animation()
    {
        /transform.DOMoveY(transform.position.y + 0.5f, 1).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
    }*/
}
