using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avioneta : MonoBehaviour {

    private Animator avionetaAnim;
    public float random;
    public int random2;
    private bool avionetaOn;

	// Use this for initialization
	void Start () {
        avionetaAnim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if(avionetaOn == false)
        {
            StartCoroutine(EscogerAnim());
            avionetaOn = true;
        }
        
    }

    private IEnumerator EscogerAnim()
    {
        random2 = Random.Range(1, 4);
        random = Random.Range(30, 40);
        Debug.Log(random2);

        switch (random2)
        {
            case 1:
                avionetaAnim.SetTrigger("Derecha");
                break;
            case 2:
                avionetaAnim.SetTrigger("Izquierda");
                break;
            case 3:
                avionetaAnim.SetTrigger("Frente");
                break;
        }
        yield return new WaitForSeconds(random);
        avionetaOn = false;
    }
}
