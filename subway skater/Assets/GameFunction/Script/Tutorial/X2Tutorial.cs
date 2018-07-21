using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X2Tutorial : MonoBehaviour {

    public static int x2 = 1;
    public ParticleSystem PS;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (x2 == 2)
            {
                PlayerMotorTutorial.Instance.slider2.value = 0;
            }
            x2 = 2;
            PS.Play();
            gameObject.SetActive(false);
            GetComponentInParent<AudioSource>().clip = PlayerMotorTutorial.Instance.X2Audio;
            GetComponentInParent<AudioSource>().Play();
        }
    }
}
