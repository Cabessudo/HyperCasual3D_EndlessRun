using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollatablesBase : MonoBehaviour
{
    protected GameObject obj;
    public string tagToCompare = "Player";
    protected bool _chanceToCollect = true; 

    void Awake()
    {
        obj = gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(tagToCompare))
        {
            PlayerController.Instance.PlayerBounce();
            _chanceToCollect = false;
            Collect();
        }
    }

    public virtual void Collect()
    {
        Debug.Log("Collected");
        
        OnCollect();
    }

    public virtual void OnCollect()
    {
        Destroy(gameObject, .3f);
    }
}
