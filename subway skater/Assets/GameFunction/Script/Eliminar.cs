﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eliminar : MonoBehaviour
{
    public void removerCodigo()
    {
        if (this.gameObject.activeInHierarchy == true)
        {
            this.gameObject.SetActive(false);
            PlayerPrefs.SetInt("Eliminar" + transform.parent.parent.GetComponentInChildren<Text>().text, 0);
        }
        else
        {
            this.gameObject.SetActive(true);
            PlayerPrefs.SetInt("Eliminar" + transform.parent.parent.GetComponentInChildren<Text>().text, 1);
        }
    }
}
