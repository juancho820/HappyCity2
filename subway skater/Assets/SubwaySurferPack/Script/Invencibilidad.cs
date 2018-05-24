﻿using System.Collections;
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
            if(powerInvenci == true)
            {
                PlayerMotor.Instance.slider.value = 0;
                PlayerMotor.Instance.speed -= 10;
            }
            other.GetComponent<Animator>().SetTrigger("BigRunning");
            PlayerMotor.Instance.speed += 10;
            powerInvenci = true;
            PS.Play();
            gameObject.SetActive(false);
        }
    }
}
