using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomObj : MonoBehaviour
{
    public List<GameObject> obj;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        RandomSpawn();
    }

    void RandomSpawn()
    {
        index = Random.Range(0, obj.Count);
        Instantiate(obj[index], transform);    
    }
}
