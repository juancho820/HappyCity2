using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipoDePremio : MonoBehaviour {

    private Text texto;

    string s1;
    string s2;

    // Use this for initialization
    void Start () {
        Invoke("Tipo", 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Tipo()
    {
        texto = this.transform.GetChild(4).GetComponent<Text>();
        s1 = texto.text;
        s2 = s1.Substring(s1.Length - 1);

        switch (int.Parse(s2))
        {
            case 1:
                transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 2:
                transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 3:
                transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 4:
                transform.GetChild(3).gameObject.SetActive(true);
                break;
        }
    }
}
