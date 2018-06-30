using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteprefs : MonoBehaviour {

	public void borrar()
    {
        PlayerPrefs.DeleteAll();
    }
}
