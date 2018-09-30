using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invencibilidad : MonoBehaviour {

    public static bool powerInvenci = false;
    public static bool powerInvenciExtra = false;
    public ParticleSystem PS;

    private void OnTriggerEnter(Collider other)
    {
        var Player = other.GetComponent<PlayerMotor>();
        if (Player != null)
        {
            Player.ActivateStarParticles();
            if (powerInvenci == true)
            {
                PlayerMotor.Instance.slider.value = 0;
                PlayerMotor.Instance.speed -= 10;
            }
            Pasos.iniciadoPasos = true;
            other.GetComponent<Animator>().SetTrigger("BigRunning");
            PlayerMotor.Instance.speed += 10;
            powerInvenci = true;
            gameObject.SetActive(false);
            GetComponentInParent<AudioSource>().clip = PlayerMotor.Instance.InvenciAudio;
            GetComponentInParent<AudioSource>().Play();
        }
    }
}
