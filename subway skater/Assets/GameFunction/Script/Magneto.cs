using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magneto : MonoBehaviour {

    public static bool powerMagneto = false;
    public ParticleSystem PS;

    private void OnTriggerEnter(Collider other)
    {
        var Player = other.GetComponent<PlayerMotor>();
        if (Player != null)
        {
            Player.ActivateMagnetoParticles();
            if (powerMagneto == true)
            {
                PlayerMotor.Instance.slider3.value = 0;
            }
            powerMagneto = true;
            gameObject.SetActive(false);
            GetComponentInParent<AudioSource>().clip = PlayerMotor.Instance.MagnetAudio;
            GetComponentInParent<AudioSource>().Play();
        }
    }
}
