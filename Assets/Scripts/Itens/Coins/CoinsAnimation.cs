using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Ebac.Core.Singleton;
using System.Linq;

public class CoinsAnimation : Singleton<CoinsAnimation>
{
    public List<ItemCollatablesCoin> itens;

    [Header("Animation")]
    public Ease ease = Ease.OutBounce;
    public float durationScale = .2f;
    public float timeBetweenItem = .3f;
    public bool playOnce;

    // Start is called before the first frame update
    void Start()
    {
        itens = new List<ItemCollatablesCoin>();    
    }

    void Update()
    {
        if(!playOnce)
        {
            Invoke("StartAnimScale", 1);
            playOnce = true;
        }
    }

    public void RegisterCoins(ItemCollatablesCoin i)
    {
        if(!itens.Contains(i))
        {
            itens.Add(i);
        }
    }

    void StartAnimScale()
    {
        StartCoroutine(ScalePiecesByTime());
    }

    IEnumerator ScalePiecesByTime()
    {
        Vector3 currScale = new Vector3(1, 0.05f, 1);

        foreach(var i in itens)
        {
            i.transform.localScale = Vector3.zero;
            foreach(var coin in i.GetComponents<Coin>())
            {
                coin.mesh.enabled = true;
            }
        }

        Sort();
        yield return null;

        
        for(int i = 0; i < itens.Count; i++)
        {
            itens[i].transform.DOScale(currScale, durationScale).SetEase(ease);
            yield return new WaitForSeconds(timeBetweenItem);
        }
    }

    void Sort()
    {
        itens = itens.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
    }
}
