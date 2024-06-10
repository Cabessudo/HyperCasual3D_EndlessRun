using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomPos : MonoBehaviour
{
    public GameObject obj;
    public Transform[] pos;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        index = Random.Range(0, pos.Length);
        Instantiate(obj, pos[index].transform);
    }

    
}
