using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Material material;
    Vector2 offset;
    [SerializeField] [Range(0,1)] float velocidade;
    void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset.Set(offset.x + Time.deltaTime * velocidade, 0);
        material.mainTextureOffset = offset;

        if(offset.x >= 1)
        {
            offset.Set(0, 0);
            material.mainTextureOffset = offset;
        }
    }
}
