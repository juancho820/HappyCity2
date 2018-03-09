using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X2 : MonoBehaviour {

    public static int x2 = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            x2 = 2;
        }
    }
}
