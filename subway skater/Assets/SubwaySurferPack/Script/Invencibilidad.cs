using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invencibilidad : MonoBehaviour {

    public static bool powerInvenci = false;
    public int InvenPower = 0;
    public ParticleSystem PS;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            powerInvenci = true;
            PS.Play();
            gameObject.SetActive(false);
        }
    }
}
