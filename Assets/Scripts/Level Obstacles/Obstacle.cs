using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public BoxCollider boxCollider;
    public MeshRenderer obstacleRender;
    public ParticleSystem destroyedVFX;
    public bool once = false;

    public void DestroyObstacle()
    {
        if(!once)
        {
            once = true;
            boxCollider.enabled = false;            
            obstacleRender.enabled = false;
            destroyedVFX.Play();
        }
    }
}
