using System.Collections;
using UnityEngine;

public class Avioneta : MonoBehaviour {

    public Animator avionetaAnim;
    private float random;
    private int random2;
    private bool avionetaOn;
	
	void Update ()
    {
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
