using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pasos : MonoBehaviour {

    public AudioClip PasosNormales, PasosHulk;

    private AudioSource Audio;

    public static bool iniciadoPasos = true;
    public static bool pararPasos = false;

    // Use this for initialization
    void Start () {

        Audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if(GameManager.Once == true)
        {
            if(iniciadoPasos == true)
            {
                if(Invencibilidad.powerInvenci == true)
                {
                    Audio.clip = PasosHulk;
                    Audio.Play();
                }
                else
                {
                    Audio.clip = PasosNormales;
                    Audio.Play();
                }
                iniciadoPasos = false;
            }
            if(pararPasos == true)
            {
                Audio.Stop();
                pararPasos = false;
            }
        }
		
	}
}
