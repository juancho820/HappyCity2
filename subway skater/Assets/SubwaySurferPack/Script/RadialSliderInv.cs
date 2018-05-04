using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialSliderInv : MonoBehaviour {

    public Image filled;
    public Image background;
    public Color Full;
    public Color Low;


    public float maxValue = 10;
    public float value = 0;

	void Update ()
    {
        value = Mathf.Clamp(value, 0, maxValue);
        float amount = value / maxValue;

        filled.fillAmount = amount;

        background.color = Color.Lerp(Full, Low, amount);
	}
}
