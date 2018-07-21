using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvencibilidadTutorial : MonoBehaviour {

    public static bool powerInvenci = false;
    public ParticleSystem PS;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(powerInvenci == true)
            {
                PlayerMotorTutorial.Instance.slider.value = 0;
                PlayerMotorTutorial.Instance.speed -= 10;
            }
            Pasos.iniciadoPasos = true;
            other.GetComponent<Animator>().SetTrigger("BigRunning");
            PlayerMotorTutorial.Instance.speed += 10;
            powerInvenci = true;
            PS.Play();
            gameObject.SetActive(false);
            GetComponentInParent<AudioSource>().clip = PlayerMotorTutorial.Instance.InvenciAudio;
            GetComponentInParent<AudioSource>().Play();
        }
    }
}
