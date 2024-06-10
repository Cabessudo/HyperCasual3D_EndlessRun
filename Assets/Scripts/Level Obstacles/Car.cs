using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("References")]
    public BoxCollider carHitBox;
    public GameObject lights;
    public ParticleSystem explosionVFX;
    

    [Header("Parameters")]
    public float speed = 5;

    //Player
    public LayerMask player;
    public bool checkPlayer;
    public float playerDis = 1;

    //Obstacle
    public LayerMask obstacle;
    public float obstacleDis = 1;  

    //Honk and Lights
    public float timeFlash = .35f;
    private bool honk;
    public bool blink;

    [Header("Audio")]
    public AudioSource honkSFX;

    void Update()
    {
        CheckObstacle();
        Check();
        
        if(!Settings.Instance.onSettings)
        {
            if(checkPlayer)
            {
                StartCoroutine(Lights());
            }
            else
            {
                StopAllCoroutines();
                lights.SetActive(true);
            }

            if(PlayerController.Instance.canMove)
            {
                Movement(speed);
            }
        }
        else
        {
            Movement();
        }
    }

    public void CheckObstacle()
    {
        RaycastHit hit;

        if(Physics.Raycast(carHitBox.transform.position, carHitBox.transform.forward, out hit, obstacleDis, obstacle))
        {
            Obstacle obs = hit.transform.GetComponent<Obstacle>();
            if(obs != null)
            obs.DestroyObstacle();
        }
        
    }

    void Movement(float currSpeed = 0)
    {
        transform.Translate(Vector3.forward * currSpeed * Time.deltaTime);
    }

    void Check()
    {
        checkPlayer = Physics.Raycast(carHitBox.transform.position, carHitBox.transform.forward, playerDis, player);
    }

    public void ExplodeCar()
    {
        Instantiate(explosionVFX, transform.position, transform.rotation);
        Destroy(gameObject, 0.1f);
    }

    IEnumerator Lights()
    {
        if(blink)
        {
            lights.SetActive(false);
            yield return new WaitForSeconds(timeFlash);
            blink = false;
        }
        else
        {
            honkSFX.Play();
            lights.SetActive(true);
            yield return new WaitForSeconds(timeFlash);
            blink = true;
        }
    }

    void Honk()
    {
        if(honk)
        {
            honkSFX.Play();
            honk = false;
        }
    }
}
