using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pasos : MonoBehaviour {

    public AudioClip PasosNormales, PasosHulk;

    private AudioSource Audio;
    public AudioSource Audio2;

    public static bool iniciadoPasos = true;
    public static bool pararPasos = false;

    private bool iniciado = false;

    // Use this for initialization
    void Start () {

        Audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {        
        if(GameManager.Once == true)
        {
            if (iniciado == false)
            {
                Audio2.clip = PasosHulk;
                Audio2.volume = 0;
                Audio2.Play();
                iniciado = true;
            }
            if (iniciadoPasos == true)
            {
                if(Invencibilidad.powerInvenci == true)
                {
                    Audio2.volume = 1;
                    GameManager.Instance.GetComponent<AudioSource>().volume = 0;
                    Audio.Stop();
                }
                else
                {
                    Audio.clip = PasosNormales;
                    Audio.Play();
                    Audio2.volume = 0;
                    GameManager.Instance.GetComponent<AudioSource>().volume = 1;
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
