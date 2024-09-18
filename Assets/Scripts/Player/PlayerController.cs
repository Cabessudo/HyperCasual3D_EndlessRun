using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Ebac.Core.Singleton;

public class PlayerController : Singleton<PlayerController>
{
    [Header("References")]
    [SerializeField] private SpawnHelper _spawn;
    [SerializeField] private BounceHelper _bounce;
    public ParticleSystem deathSFX;
    public AnimatorManager anim;
    public GameObject restartUI;
    public GameObject coinCollect; 
    public GameObject ballons;
    public Transform endPos;
    public Transform playerBody;

    [Header("Lerp")]
    public Vector3 pos;
    public Transform targetPos;
    public float lerpSpeed = 1;

    [Header("Move")]
    public float speed = 9; 
    private float _currentSpeed;
    private bool _canRun = false; //If Can Move
    public float timeToRestart = 1;

    [Header("Interations")]
    private string obstacleTag = "Obstacle";
    private string pedestrianTag = "Pedestrian";
    private string endLineTag = "EndLine";
    private string carTag = "Car";
    public bool finish; //Check to pass to the next level

    [Header("PowerUp's")]
    public int currPWUPSCollecteds;
    public bool hasPWUP;
    public bool invencible;
    private float _backToGround;
    public float ballonScale = 1;
    public bool isFlying;

    [Header("Anim")]
    public bool cutscene; //End Anim
    public bool canMove = false; //Start Anim
    public Ease ease = Ease.InOutBounce;

    [Header("Audio")]
    public AudioManager sfx;
    

    void Start()
    {
        RestartSpeed();
        Init();
        StartRun();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPWUPS();

        if(!Settings.Instance.onSettings)
        {
            anim.CheckAnimVelocity(speed);
            EndCutscene();

            if(!_canRun) return;

            Move();

            if(canMove)
            {
                Lerp();
                StopOnBounds();
            }
        }
        else
        anim.CheckAnimVelocity();
    }
    
    #region Collisions
    void OnCollisionEnter(Collision collision)
    {
        if(!invencible)
        {
            if(collision.transform.tag == obstacleTag)
            {
                HitEnemy(collision.transform);             
                EndGame(AnimatorManager.AnimStyle.Dead);
                deathSFX.Play();
                Debug.Log("Game Over!!!");
                RestartScreen.Instance.ChooseEndType(RestartScreen.EndType.Obstacle);
            }

            if(collision.transform.tag == pedestrianTag)
            {
                HitEnemy(collision.transform);             
                EndGame(AnimatorManager.AnimStyle.Dead);
                deathSFX.Play();
                Debug.Log("Game Over!!!");
                RestartScreen.Instance.ChooseEndType(RestartScreen.EndType.Pedestrian);
            }
        } 
        else
        {
            if(collision.transform.tag == obstacleTag)
            {
                sfx.ExplodeSFX(true);
                Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();
                if(obstacle != null)
                obstacle.DestroyObstacle();
            }

            if(collision.transform.tag == pedestrianTag)
            {
                sfx.HitSFX();
            }
        }

        if(collision.gameObject.CompareTag(carTag) && !invencible && _canRun)
        {
            sfx.HitSFX();
            EndGame(AnimatorManager.AnimStyle.Dead_02);
            deathSFX.Play();
            Debug.Log("Game Over!!!");
            RestartScreen.Instance.ChooseEndType(RestartScreen.EndType.Car);
            BoxCollider carBox = collision.gameObject.GetComponent<BoxCollider>();
            carBox.enabled = false;
        }
        else if(collision.gameObject.CompareTag(carTag) && invencible && _canRun)
        {
            sfx.ExplodeSFX();
            Car car = collision.gameObject.GetComponent<Car>();
            if(car != null)
            car.ExplodeCar();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == endLineTag)
        {
            Debug.Log("End Game!!!");
            finish = true;
            _canRun = false;
            RestartScreen.Instance.ChooseEndType(RestartScreen.EndType.Finsh, 3);
        }
    }

    #endregion

    #region  InGame
    void Init()
    {
        _spawn.Spawn(transform);
        _backToGround = transform.position.y;
        playerBody.transform.position = Vector3.zero;
    }

    public void StartRun()
    {
        anim.Play(AnimatorManager.AnimStyle.Run, _currentSpeed);
        _canRun = true;
    }

    void Move()
    {
        transform.Translate(Vector3.forward * _currentSpeed * Time.deltaTime);
    }

    void Lerp()
    {
        pos = targetPos.position;
        pos.y = transform.position.y;
        pos.z = transform.position.z;
        
        float finalX = transform.position.x + (pos.x - transform.position.x) * lerpSpeed * Time.deltaTime;

        var p = transform.position;
        p.x = finalX;
        transform.position = p;
    } 

    void StopOnBounds()
    {
        Vector3 rightX = new Vector3(5, transform.position.y, transform.position.z);
        Vector3 leftX = new Vector3(-5, transform.position.y, transform.position.z);

        if(transform.position.x > 5)
        transform.position = rightX;

        if(transform.position.x < -5)
        transform.position = leftX;
    }

    #endregion



    

    #region PWUPS

    public void CheckPWUPS()
    {
        if(currPWUPSCollecteds > 0)
        hasPWUP = true;
        else
        hasPWUP = false;
    }

    #region PowerUp Speed
    public void PowerUpSpeed(float f)
    {
        sfx.SpeedUpSFX();
        _currentSpeed = f;
    }

    public void RestartSpeed()
    {
        _currentSpeed = speed;
    }
    #endregion

    #region  PowerUp Fly
    public void Fly(float height, float duration, float delayToLand, Ease ease)
    {
        sfx.FlySFX();
        isFlying = true;
        DOTween.Kill(transform);
        Ballons();
        anim.Play(AnimatorManager.AnimStyle.JumpFly, _currentSpeed, true);
        anim.Play(AnimatorManager.AnimStyle.Fly, _currentSpeed, true);
        transform.DOMoveY(height, duration).SetEase(ease).OnComplete(
            delegate
            {
                FlyLand(duration, delayToLand);
            });
    }

    void FlyLand(float duration, float delayToLand)
    {
        transform.DOMoveY(_backToGround, duration / 2).SetDelay(delayToLand).OnComplete(
            delegate
            {
                anim.Play(AnimatorManager.AnimStyle.Fly, _currentSpeed, false);
                Ballons(false, true);
                playerBody.transform.localPosition = Vector3.zero;
                isFlying = false;
            });
    }

    //Make the Ballons from the Fly Powerup appear and disappear 
    void Ballons(bool b = true, bool explode = false)
    {
        foreach(var ballon in ballons.GetComponentsInChildren<MeshRenderer>())
        {
            if(!explode)
            {
                ballons.transform.DOKill();
                ballons.transform.localScale = Vector3.zero;
                ballons.transform.DOScale(Vector3.one, ballonScale).SetEase(ease);
                ballons.SetActive(true);
                ballon.enabled = b;
            }
            else
            {
                ballons.transform.DOScale(1.2f, .5f).SetEase(Ease.InOutBounce).OnComplete(
                    delegate
                    {
                        ExplodeBallons();
                        ballon.enabled = b;
                        sfx.FlySFX(b);
                    });
            }
        }
    }

    void ExplodeBallons()
    {
        foreach(var ballon in ballons.GetComponentsInChildren<ParticleSystem>())
        ballon.Play();
    }
    #endregion 

    #region  Magnet //Increases the size of coin collect that has the trigger to collect the coins
    public void Magnet(bool once, float amount = 1)
    {
        coinCollect.transform.localScale = Vector3.one * amount;
        if(once)
        sfx.MagnetSFX();
    }
    #endregion

    #endregion

    #region Collect Itens

    //When the Player gets something he bounces
    public void PlayerBounce()
    {
        _bounce.Bounce();
    }
    #endregion

    #region Game Over
    //Make the Obstacles move back
    void HitEnemy(Transform t)
    {
        sfx.HitSFX();
        t.DOMoveZ(1, 0.3f).SetRelative();
    }

    void Restart()
    {
        restartUI.SetActive(true);
        ItemManager.Instance.HideCoinsInGameText();
    }

    void EndGame(AnimatorManager.AnimStyle a = AnimatorManager.AnimStyle.Idle)
    {
        anim.Play(a, _currentSpeed);
        _canRun = false;
        Invoke(nameof(Restart), timeToRestart);
    }

    void EndCutscene()
    {
        endPos = GameObject.FindGameObjectWithTag("End Pos").GetComponent<Transform>();

        if(finish && !isFlying)
        {
            var viewDir = endPos.position - transform.position;
            transform.forward = viewDir.normalized;
            transform.Translate(Vector3.forward * _currentSpeed * Time.deltaTime);
            cutscene = true;
            transform.DOScale(Vector3.zero, 1).SetDelay(2);
            Invoke(nameof(Restart), 3);            
        }
        else if(finish && isFlying)
        {
            FlyLand(2.5f, 0);
            transform.Translate(Vector3.forward * 3 * Time.deltaTime);
        }
    }
    #endregion
}