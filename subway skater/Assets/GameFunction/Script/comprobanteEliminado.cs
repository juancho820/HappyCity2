using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class comprobanteEliminado : MonoBehaviour {

	// Use this for initialization
	void Start () {

        if (PlayerPrefs.GetInt("Eliminar" + transform.parent.name) == 1)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }

    }
}
