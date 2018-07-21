using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetoTutorial : MonoBehaviour {

    public static bool powerMagneto = false;
    public ParticleSystem PS;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (powerMagneto == true)
            {
                PlayerMotorTutorial.Instance.slider3.value = 0;
            }
            powerMagneto = true;
            PS.Play();
            gameObject.SetActive(false);
            GetComponentInParent<AudioSource>().clip = PlayerMotorTutorial.Instance.MagnetAudio;
            GetComponentInParent<AudioSource>().Play();
        }
    }
}
