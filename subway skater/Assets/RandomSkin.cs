using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkin : MonoBehaviour {

    public Texture[] textures;
    public Renderer rend;
    public int random;

    void Start()
    {
        random = Random.Range(0, 3);
        rend = GetComponent<Renderer>();
        rend.material.mainTexture = textures[random];
        Debug.Log(random);
    }
}
