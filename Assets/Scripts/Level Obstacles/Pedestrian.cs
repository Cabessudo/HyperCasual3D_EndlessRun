using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    private BoxCollider _box;
    private Rigidbody _pedestrianRb;
    private Animator _pedestrianAnim;
     public bool dead;
    private string deathTrigger = "Dead";
    private string playerTag = "Player";

    void Start()
    {
        _pedestrianAnim = GetComponent<Animator>();
        _pedestrianRb = GetComponent<Rigidbody>();
        _box = GetComponent<BoxCollider>();
    }

    [NaughtyAttributes.Button]
    public void Death()
    {
        dead = true;
        _pedestrianRb.useGravity = false;
        _box.enabled = false; 
        _pedestrianAnim.SetTrigger(deathTrigger);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(playerTag))
        {
            Death();
        }
    }
}