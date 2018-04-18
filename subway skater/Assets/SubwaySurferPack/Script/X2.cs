using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X2 : MonoBehaviour {

    public static int x2 = 1;
    public ParticleSystem PS;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            x2 = 2;
            PS.Play();
            gameObject.SetActive(false);
        }
    }
}
