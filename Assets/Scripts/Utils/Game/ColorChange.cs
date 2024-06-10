using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChange : MonoBehaviour
{
    public MeshRenderer mesh;
    public Color startColor = Color.white;
    private Color correctColor;

    [Header("Animation")]
    public Ease ease = Ease.InOutBounce;
    private float duration = 2;
    private float delay = .7f; 

    void OnValidate()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        correctColor = mesh.materials[0].GetColor("_Color");
        LerpColor();
    }

    public void LerpColor()
    {
        mesh.materials[0].SetColor("_Color", startColor);
        mesh.materials[0].DOColor(correctColor, duration).SetDelay(delay);
    }
}
