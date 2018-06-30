using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkybox : MonoBehaviour {
	void Update () {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time*0.1f);
    }
}
