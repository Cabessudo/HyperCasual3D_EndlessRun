using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHelper : MonoBehaviour
{
    public List<Transform> pos;
    public float duration = 3;
    public int index;

    void Start()
    {
        transform.position = pos[0].transform.position;
        NextIndex();

        StartCoroutine(Movement());
    }

    void NextIndex()
    {
        index++;
        if(index >= pos.Count) index = 0;
    }

    IEnumerator Movement()
    {
        float time = 0;

        while(true)
        {
            var currPos = transform.position;

            while(time < duration)
            {
                transform.position = Vector3.Lerp(currPos, pos[index].transform.position, (time/duration));

                time += Time.deltaTime;
                yield return null;
            }

            NextIndex();
            time = 0;

            yield return null;
        }
    }
}
