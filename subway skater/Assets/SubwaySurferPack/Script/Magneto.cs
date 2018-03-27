using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magneto : MonoBehaviour {

    public static bool powerMagneto = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            powerMagneto = true;
            gameObject.SetActive(false);
        }
    }
}
