using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTutorial : MonoBehaviour
{
    private Animator anim;
    private GameObject player;
    public bool cogida = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            cogida = true;
            GameManagerTutorial.Instance.GetCoin();
        }
    }
    private void Update()
    {
        if (cogida == true)
        {
            anim.enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.6f);    

            if ((transform.position - player.transform.position).magnitude < 0.1)
            {
                GetComponentInParent<AudioSource>().pitch = 1 + GameManagerTutorial.Instance.pitch;
                GetComponentInParent<AudioSource>().clip = PlayerMotorTutorial.Instance.TicketAudio;
                GetComponentInParent<AudioSource>().Play();
                gameObject.SetActive(false);
            }
        }
    }
}
