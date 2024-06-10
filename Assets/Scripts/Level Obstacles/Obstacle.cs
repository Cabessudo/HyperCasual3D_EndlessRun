using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public MeshRenderer obstacleRender;
    public ParticleSystem destroyedVFX;

    public void DestroyObstacle()
    {
        obstacleRender.enabled = false;
        destroyedVFX.Play();
    }
}
