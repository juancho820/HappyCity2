using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialSlider : MonoBehaviour {

    public Image filled;

    public float maxValue = 10;
    public float value = 0;

	void Update ()
    {
        value = Mathf.Clamp(value, 0, maxValue);
        float amount = value / maxValue;

        filled.fillAmount = amount;		
	}
}
