using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magneto : MonoBehaviour {

    public static bool powerMagneto = false;
    private ParticleSystem PS;

    public void Start()
    {
        PS = GetComponentInParent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            powerMagneto = true;
            PS.Play();
            gameObject.SetActive(false);
        }
    }
}
