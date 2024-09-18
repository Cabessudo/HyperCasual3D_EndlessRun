using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("References")]
    public BoxCollider carHitBox;
    public GameObject lights;
    public ParticleSystem explosionVFX;
    public AudioSource honkSFX;

    [Header("Parameters")]
    public float speed = 5;

    //Player
    public float offset;
    public LayerMask player;
    public bool checkPlayer;
    public float playerDis = 1;

    //Honk and Lights
    public float timeFlash = .35f;
    private bool honk;
    public bool blink;

    //Obstacles
    private string obstacleTag = "Obstacle";
    private string pedestrianTag = "Pedestrian";
    


    void Update()
    {
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

    void Movement(float currSpeed = 0)
    {
        transform.Translate(Vector3.forward * currSpeed * Time.deltaTime);
    }

    void Check()
    {
        checkPlayer = Physics.Raycast(carHitBox.transform.position, carHitBox.transform.forward + Vector3.up * offset, playerDis, player);
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

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(obstacleTag))
        {
            Obstacle obs = collision.transform.GetComponent<Obstacle>();
            obs?.DestroyObstacle();
        }

        if(collision.gameObject.CompareTag(pedestrianTag))
        {
            Pedestrian ped = collision.transform.GetComponent<Pedestrian>();
            ped.Death();
        }
    }
}
