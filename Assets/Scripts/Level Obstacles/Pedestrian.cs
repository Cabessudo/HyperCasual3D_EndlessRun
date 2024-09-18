using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    private CrossThePath _pedestrianParent;
    private BoxCollider _box;
    private Rigidbody _pedestrianRb;
    private Animator _pedestrianAnim;
    private string deathTrigger = "Dead";
    private string playerTag = "Player";

    void Start()
    {
        _pedestrianAnim = GetComponent<Animator>();
        _pedestrianRb = GetComponent<Rigidbody>();
        _pedestrianParent = GetComponentInParent<CrossThePath>();
        _box = GetComponent<BoxCollider>();
    }

    public void Death()
    {
        if(_pedestrianParent != null) _pedestrianParent.dead = true;
        _pedestrianAnim.SetTrigger(deathTrigger);
        _pedestrianRb.useGravity = false;
        _box.enabled = false; 
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(playerTag))
        {
            Death();
        }
    }
}