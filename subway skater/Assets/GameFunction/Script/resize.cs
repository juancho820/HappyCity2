using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resize : MonoBehaviour {

    public static resize Instance { set; get; }

    private RectTransform rt;

    // Use this for initialization
    void Start () {
        rt = GetComponent<RectTransform>();
        Instance = this;
        rt.sizeDelta = new Vector2 (0, PlayerPrefs.GetInt("canvasSize"));

		
	}

    public void Resz()
    {
        rt.sizeDelta += new Vector2(0, 200);
        PlayerPrefs.SetInt("canvasSize", (int)rt.sizeDelta.y);
    }
}
