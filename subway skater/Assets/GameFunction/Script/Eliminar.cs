using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eliminar : MonoBehaviour {

    public void removerCodigo()
    {
        if (this.gameObject.activeInHierarchy == true)
        {
            this.gameObject.SetActive(false);
            GetComponentInParent<paraEliminar>().eliminar = false;
        }
        else
        {
            this.gameObject.SetActive(true);
            GetComponentInParent<paraEliminar>().eliminar = true;
        }
    }
}
