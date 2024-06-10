using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuBackGround : MonoBehaviour
{
    public Transform backGround;
    private Vector3 startPos;
    public int moveMenu = 10;
    private float time;
    public float timeToBack = 10;

    void Awake()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if(time < timeToBack)
        {
            transform.Translate(Vector3.up * moveMenu * Time.deltaTime);
            time += Time.deltaTime;
        }

        if(time > timeToBack)
        {
            transform.position = startPos;
            time = 0;
        }
    }

    // // Start is called before the first frame update
    // void Start()
    // {
    //     var move = (Vector3.right * moveMenu /2) + (Vector3.up * moveMenu);
    //     backGround.DOLocalMove(move, 10).OnComplete(
    //     delegate
    //     {
    //         transform.position = startPos;
    //     });   
    // }
}
