using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    private CrossThePath _pedestrianParent;
    private BoxCollider _box;
    private Rigidbody _pedestrianRb;
    private string deathTrigger = "Dead";
    private Animator _pedestrianAnim;

    void Start()
    {
        _pedestrianAnim = GetComponent<Animator>();
        _pedestrianRb = GetComponent<Rigidbody>();
        _pedestrianParent = GetComponentInParent<CrossThePath>();
        _box = GetComponent<BoxCollider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(_pedestrianParent.tagToCompare))
        {
            _pedestrianParent.dead = true;
            _pedestrianAnim.SetTrigger(deathTrigger);
            _pedestrianRb.useGravity = false;
            _box.enabled = false; 
        }
    }
}