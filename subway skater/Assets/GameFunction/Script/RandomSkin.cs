using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkin : MonoBehaviour {

    public Texture[] textures;
    private Renderer rend;
    private SkinnedMeshRenderer rend2;
    private int random;

    void Start()
    {
        if (textures.Length == 2)
        {
            Debug.Log("1");
            random = Random.Range(0, 2);
        }
        if (textures.Length == 3)
        {
            Debug.Log("2");
            random = Random.Range(0, 3);
        }
        if (textures.Length == 4)
        {
            Debug.Log("3");
            random = Random.Range(0, 4);
        }
        if (textures.Length == 5)
        {
            random = Random.Range(0, 5);
        }
        rend = GetComponent<Renderer>();
        if(rend == null)
        {
            rend2 = GetComponent<SkinnedMeshRenderer>();
            rend2.material.mainTexture = textures[random];
        }
        else
        {
            rend.material.mainTexture = textures[random];
        }
        Debug.Log("start");
    }
}
