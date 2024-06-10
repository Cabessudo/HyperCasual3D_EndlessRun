using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CoinManager : MonoBehaviour
{
    public SpawnHelper spawnAnim;
    private List<Coin> spawnedCoins = new List<Coin>();
    public Coin coin;
    public GameObject[] coinsSpawn;
    private int coinPos;
    public int coinsToSpawn = 5;

    void OnEnable()
    {
        coinPos = Random.Range(0, coinsSpawn.Length);
        for(int i = 0; i < coinsToSpawn; i++)
        {
            SpawnNextCoin();   
        }      
    }
    
    void SpawnNextCoin()
    {
        var spawnedCoin = Instantiate(coin, coinsSpawn[coinPos].transform);
        spawnedCoin.mesh.enabled = false;

        if(spawnedCoins.Count > 0)
        {
            var lastCoin = spawnedCoins[spawnedCoins.Count - 1];
            spawnedCoin.transform.position = lastCoin.nextPos.position;
        }
        
        spawnedCoins.Add(spawnedCoin);
    }
}
