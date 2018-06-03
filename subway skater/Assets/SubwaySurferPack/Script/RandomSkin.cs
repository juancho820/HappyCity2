﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkin : MonoBehaviour {

    public Texture[] textures;
    private Renderer rend;
    private int random;

    void Start()
    {
        random = Random.Range(0, 3);
        rend = GetComponent<Renderer>();
        rend.material.mainTexture = textures[random];
    }
}
