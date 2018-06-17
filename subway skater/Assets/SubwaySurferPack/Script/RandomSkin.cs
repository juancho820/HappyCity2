using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkin : MonoBehaviour {

    public Texture[] textures;
    private Renderer rend;
    private int random;

    void Start()
    {
        if (textures.Equals(2))
        {
            random = Random.Range(0, 2);
        }
        if (textures.Equals(3))
        {
            random = Random.Range(0, 3);
        }
        if (textures.Equals(4))
        {
            random = Random.Range(0, 4);
        }
        rend = GetComponent<Renderer>();
        rend.material.mainTexture = textures[random];
    }
}
