using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invencibilidad : MonoBehaviour {

    public static bool powerInvenci = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            powerInvenci = true;
            gameObject.SetActive(false);
            Debug.Log(powerInvenci);
        }
    }
}
