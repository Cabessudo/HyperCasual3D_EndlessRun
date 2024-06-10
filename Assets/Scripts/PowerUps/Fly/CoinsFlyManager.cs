using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class CoinsFlyManager : MonoBehaviour
{
    public SpawnHelper spawn;
    public Coin coin;
    private List<Coin> _spawnedCoin = new List<Coin>();
    public Transform startPos;
    public SOFlyPowerUp SOFly;

    public float timeBetweenCoin = 0.5f;

    void Start()
    {
        SpawnCoins();
    }
    
    void SpawnCoins()
    {
        for(int i = 0; i < SOFly.coinsToAdd; i++)
        {
            SpawnNextCoin();
        }

        StartCoroutine(ScalePerCoin());
    }

    void SpawnNextCoin()
    {
        var spawned = Instantiate(coin, startPos);

        if(_spawnedCoin.Count > 0)
        {
            var lastCoin = _spawnedCoin[_spawnedCoin.Count - 1];
            spawned.transform.position = lastCoin.nextPos.position;
        }

        _spawnedCoin.Add(spawned);
    }

    IEnumerator ScalePerCoin()
    {
        var currScale = coin.transform.localScale;

        foreach(var c in _spawnedCoin)
        {
            c.transform.localScale = Vector3.zero;
        }

        Sort();
        yield return null;

        for(int i = 0; i < _spawnedCoin.Count; i++)
        {
            _spawnedCoin[i].transform.DOScale(currScale, spawn.durationScale).SetEase(spawn.ease);
            yield return new WaitForSeconds(timeBetweenCoin);
        }
    }

    void Sort()
    {
        _spawnedCoin = _spawnedCoin.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
    }
}
