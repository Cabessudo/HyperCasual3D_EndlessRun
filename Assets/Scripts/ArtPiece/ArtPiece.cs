using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtPiece : MonoBehaviour
{
    public GameObject currentArt;

    public void ChangeArtType(GameObject g)
    {
        if(currentArt != null) Destroy(gameObject);

        currentArt = Instantiate(g, transform);
        currentArt.transform.localPosition = Vector3.zero;
    }
}
