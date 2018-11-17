using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bodega : MonoBehaviour {

    public static Bodega Instance { set; get; }

    private int numeroDeCodigo;

    public GameObject codigo;

    private void Awake()
    {
        Instance = this;

        if (!PlayerPrefs.HasKey("numeroDeCodigo"))
        {
            PlayerPrefs.SetInt("numeroDeCodigo", 0);
            numeroDeCodigo = PlayerPrefs.GetInt("numeroDeCodigo");
        }
        else
        {
            numeroDeCodigo = PlayerPrefs.GetInt("numeroDeCodigo");
        }
    }

    public void crearCodigo(string codigoGenerado)
    {
        numeroDeCodigo++;
        GameObject go = Instantiate(codigo, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        go.name = "Premio" + numeroDeCodigo.ToString();
        go.transform.SetParent(this.transform, false);
        codigo.GetComponentInChildren<Text>().text = codigoGenerado;
        PlayerPrefs.SetString("Codigo" + numeroDeCodigo.ToString("0"), codigoGenerado);
        PlayerPrefs.SetInt("numeroDeCodigo", numeroDeCodigo);
        resize.Instance.Resz();
    }

    public void cargarCodigos()
    {
        if (numeroDeCodigo > 0)
        {
            destruirCodigos();
            for (int i = 1; i <= numeroDeCodigo; i++)
            {
                GameObject go = Instantiate(codigo, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                go.transform.SetParent(this.transform, false);
                go.name = "Premio" + i.ToString();
                codigo.GetComponentInChildren<Text>().text = PlayerPrefs.GetString("Codigo" + i.ToString("0"));
            }
        }
    }

    public void destruirCodigos()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
