using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour {

    private GameObject player;
    private Vector3 position;
    private bool llego = false;
    private bool una = false;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        position = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.isDead == false)
        {
            if (this.transform.position.z - player.transform.position.z > -20)
            {
                if (this.transform.position.z - player.transform.position.z < 40)
                {
                    llego = true;
                }
            }
            else
            {
                llego = false;
                transform.position = position;
            }
            if (llego == true)
            {
                if (una == false)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 40);
                    una = true;
                }
                transform.Translate((-Vector3.forward * PlayerMotor.Instance.speed) * Time.deltaTime);
            }
        }		
	}
}
