using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magneto : MonoBehaviour {

    public GameObject magneto;
    public static bool powerMagneto = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            magneto.SetActive(true);
            powerMagneto = true;
        }
    }
}
