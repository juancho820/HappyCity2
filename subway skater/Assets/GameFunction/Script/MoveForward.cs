using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour {

    private GameObject player;
    private Vector3 position;
    private bool llego = false;
    private bool una = false;
    public bool invenci = true;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}

    private void OnEnable()
    {
        position = this.transform.position;
    }

    void Update () {
        if (GameManager.Instance.isDead == false)
        {
            if (this.transform.position.z - player.transform.position.z > -40)
            {
                if (this.transform.position.z - player.transform.position.z < 40)
                {
                    llego = true;
                }
            }
            else
            {
                llego = false;
                SlidingNumbers();
            }
            if (llego == true)
            {
                if (una == false)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 40);
                    una = true;
                }
                if(invenci == true)
                {
                    transform.Translate((-Vector3.forward * PlayerMotor.Instance.speed) * Time.deltaTime);
                }
            }
        }		
	}
    private IEnumerator SlidingNumbers()
    {
        yield return new WaitForSeconds(1f);
        transform.position = position;
    }
}
