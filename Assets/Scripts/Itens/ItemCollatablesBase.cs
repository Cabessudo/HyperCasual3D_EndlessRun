using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollatablesBase : MonoBehaviour
{
    // public GameObject PFB_collected;
    // public SpriteRenderer graphicItem;
    // public ParticleSystem PFB_particleSystem;
    protected GameObject obj;
    public string tagToCompare = "Player";
    protected bool _chanceToCollect = true; 
    
    // [Header("Sounds")]
    // public AudioSource audioSource;

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
        // if(graphicItem != null) graphicItem.enabled = false;
        
        OnCollect();
    }

    public virtual void OnCollect()
    {
        // if(PFB_collected != null) PFB_collected.SetActive(true);
        // if(PFB_particleSystem != null) Instantiate(PFB_particleSystem, transform.position, PFB_particleSystem.transform.rotation);
        // if(audioSource != null) audioSource.Play();
        Destroy(gameObject, .3f);
    }
}
