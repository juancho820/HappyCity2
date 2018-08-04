using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsSpawner : MonoBehaviour {

    private float chanceToSpawn = 0.4f;

    private void Awake()
    {
        if (Random.Range(0.0f, 1.0f) > chanceToSpawn)
        {
            OnDisable();
        }
        else
        {
            gameObject.SetActive(true);
        }     
    }
    private void OnDisable()
    {
            gameObject.SetActive(false);
    }
}
