using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFader : MonoBehaviour
{
    [SerializeField] private float fadeSpeed, fadeAmount;
    float originalOpacity;
    Material mat;
    MeshRenderer mR;
    public bool DoFade = false;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
        mR = GetComponent<MeshRenderer>();
        originalOpacity = mat.color.a;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (DoFade)
            Disappear();
        else
            Appear();
    }

    void Disappear()
    {
        mR.enabled = false;
    }

    void Appear()
    {
        mR.enabled = true;
    }

    void FadeNow()
    {
        Color currentColor = mat.color;
        Color smoothColor = new Color(currentColor.r,currentColor.g, currentColor.b,Mathf.Lerp(currentColor.a,fadeAmount,fadeSpeed));
        mat.color = smoothColor;
    }

    void ResetFade()
    {
        Color currentColor = mat.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, originalOpacity, fadeSpeed));
        mat.color = smoothColor;
    }
}
