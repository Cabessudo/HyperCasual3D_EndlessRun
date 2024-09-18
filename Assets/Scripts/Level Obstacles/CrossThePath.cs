using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossThePath : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public SpawnHelper spawn;
    public GameObject[] obj;
    protected int _randomObj;

    [Header("Player Check")]
    private string playerTag = "Player";
    protected bool _onRadious;

    [Header("Animation")]
    public List<AnimationType> animType;
    public Animator _currAnim;
    public int _randomAnim;

    [Header("Parameters")]
    private bool one;
    private bool once;
    public bool dead;
    protected float timeToMove = .5f;

    [Header("Audio")]
    private AudioManager sfx;
    private bool outRoad; 

    void Awake()
    {
        _randomObj = Random.Range(0, obj.Length);
        _randomAnim = Random.Range(0, animType.Count);

        if(!outRoad) sfx = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if(_onRadious && !dead)
        {
            SpawnObj();
            StartCoroutine(Movement());
        }

        if(!Settings.Instance.onSettings && one && !dead)
        {
            StartCoroutine(Movement());
            StopPedestrian(false);
        }
        else if(Settings.Instance.onSettings && one || dead)
        {
            StopAllCoroutines();
            StopPedestrian(true);
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
        if(!one)
        { 
            spawn.Spawn(transform);
            Instantiate(obj[_randomObj], transform);
            one = true;
            if(sfx != null)
            sfx.PedestrianSFX();
        }
    }

    IEnumerator Movement()
    {
        yield return new WaitForSeconds(timeToMove);
        GetAnim();
        _currAnim.SetTrigger(animType[_randomAnim].trigger);
        transform.Translate(Vector3.forward * animType[_randomAnim].speed * Time.deltaTime);
        if(!once)
        {
            rb.useGravity = true;
            once = true;
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
                _currAnim.speed = 0;
            else
                _currAnim.speed = 1;
        }
    }

    [System.Serializable]
    public class AnimationType
    {
        public string trigger;
        public float speed;
    }
}
