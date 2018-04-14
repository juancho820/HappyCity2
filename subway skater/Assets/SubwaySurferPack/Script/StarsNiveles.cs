using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsNiveles : MonoBehaviour {

    public Sprite Spr1,Spr2,Spr3;
    private SpriteRenderer SprR;
    public static int NroEstrellas;

    public void Start()
    {
        SprR = GetComponent<SpriteRenderer>();
        //SprR.sprite;
    }

    public void AsignarEstrellas()
    {
        switch (NroEstrellas)
        {
            case 1:
                SprR.sprite = Spr1;
                    break;
            case 2:
                SprR.sprite = Spr2;
                break;
            case 3:
                SprR.sprite = Spr3;
                break;
        }
    }
}
