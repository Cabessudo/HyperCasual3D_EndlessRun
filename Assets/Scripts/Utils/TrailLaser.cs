using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailLaser : MonoBehaviour
{
    public List<Transform> positions;
    public LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line.positionCount = positions.Count;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < positions.Count; i++)
        {
            line.SetPosition(i, positions[i].position);
        }
    }
}
