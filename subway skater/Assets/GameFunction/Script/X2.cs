using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X2 : MonoBehaviour {

    public static int x2 = 1;
    public ParticleSystem PS;

    private void OnTriggerEnter(Collider other)
    {
        var Player = other.GetComponent<PlayerMotor>();
        if (Player != null)
        {
            Player.ActivateX2Particles();
            if (x2 == 2)
            {
                PlayerMotor.Instance.slider2.value = 0;
            }
            x2 = 2;
            gameObject.SetActive(false);
            GetComponentInParent<AudioSource>().clip = PlayerMotor.Instance.X2Audio;
            GetComponentInParent<AudioSource>().Play();
        }
    }
}
