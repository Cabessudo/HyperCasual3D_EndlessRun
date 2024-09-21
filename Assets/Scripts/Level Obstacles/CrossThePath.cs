using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossThePath : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;
    public SpawnHelper spawn;
    public GameObject[] obj;
    public Pedestrian pedestrian;
    protected int _randomPedestrian;

    [Header("Player Check")]
    private string playerTag = "Player";
    protected bool _onRadious;

    [Header("Animation")]
    public List<AnimationType> animType;
    public Animator _currAnim;
    public int _randomAnim;

    [Header("Parameters")]
    private bool one;
    private bool triggerGravityOnce = false;
    protected float timeToMove = .5f;

    [Header("Audio")]
    private AudioManager sfx;
    private bool outRoad; 

    private bool isMoving = false;
    private float _currSpeed;

    void Awake()
    {
        _randomPedestrian = Random.Range(0, obj.Length);
        _randomAnim = Random.Range(0, animType.Count);

        if(!outRoad) sfx = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pedestrian != null)
        {
            Move();
            
            if(pedestrian.dead)
            {
                _currSpeed = 0;
                StopAllCoroutines();
            }


            if(!Settings.Instance.onSettings && one && !pedestrian.dead && !isMoving)
            {
                StartCoroutine(Movement());
                StopPedestrian(false);
            }
            else if(Settings.Instance.onSettings && one)
            {
                StopAllCoroutines();
                StopPedestrian(true);
            }

        }

        if(_onRadious && !one)
        {
            SpawnObj();
            StartCoroutine(Movement());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(playerTag))
        {
            _onRadious = true;
        }
    }

    void SpawnObj()
    {
        one = true;
        spawn?.Spawn(transform);
        var ped = Instantiate(obj[_randomPedestrian], transform);
        pedestrian = ped.GetComponent<Pedestrian>();
        sfx?.PedestrianSFX();
    }

    void Move()
    {
        if(one)
            transform.Translate(Vector3.forward * _currSpeed * Time.deltaTime);
    }

    IEnumerator Movement()
    {
        yield return new WaitForSeconds(timeToMove);
        GetAnim();
        _currSpeed = animType[_randomAnim].speed;
        _currAnim.SetTrigger(animType[_randomAnim].trigger);
        if(!triggerGravityOnce)
        {
            rb.useGravity = true;
            triggerGravityOnce = true;
        }
    }

    public void GetAnim()
    {
        _currAnim = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody>();
    }

    public void StopPedestrian(bool pause)
    {
        if(_currAnim != null)
        {
            if(pause)
            {
                _currAnim.speed = 0;
                isMoving = false;
                _currSpeed = 0;
            }
            else
            {
                _currSpeed = animType[_randomAnim].speed;
                _currAnim.speed = 1;
                isMoving = true;
            }
        }
    }

    [System.Serializable]
    public class AnimationType
    {
        public string trigger;
        public float speed;
    }
}
