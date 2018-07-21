using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCoinDead : MonoBehaviour {
	void Update () {
        if(GameManager.Instance == null)
        {
            if(GameManagerTutorial.Instance.isDead == true)
            {
                transform.Rotate(0, 0, 0 + Time.deltaTime * 20);
            }
            if (GameObject.Find("ResplandorTitulo"))
            {
                transform.Rotate(0, 0, 0 + Time.deltaTime * 10);
            }

        }
        else
        {
            if (GameManager.Instance.isDead == true)
            {
                transform.Rotate(0, 0, 0 + Time.deltaTime * 20);
            }
            if (GameObject.Find("ResplandorTitulo"))
            {
                transform.Rotate(0, 0, 0 + Time.deltaTime * 10);
            }
        }
	
	}
}
