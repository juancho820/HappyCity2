using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
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
        if (Magneto.powerMagneto == false)
        {
            if (other.tag == "Player")
            {
                cogida = true;
                GameManager.Instance.GetCoin();
            }
        }
        else
        {
            if (other.tag == "Magneto")
            {
                cogida = true;
                GameManager.Instance.GetCoin();
            }
        }
    }
    private void Update()
    {
        if (cogida == true)
        {
            anim.enabled = false;

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.75f);

            if ((transform.position - player.transform.position).magnitude < 0.1)
            {
                GetComponentInParent<AudioSource>().clip = PlayerMotor.Instance.TicketAudio;
                GetComponentInParent<AudioSource>().Play();
                gameObject.SetActive(false);
            }
        }
    }
}
