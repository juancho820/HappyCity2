using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Animator anim;
    public GameObject player;
    public bool cogida = false;
    private float t = 0;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        anim.SetTrigger("Spawn");
    }

    private void OnTriggerEnter (Collider other)
    {
        if(Magneto.powerMagneto == false)
        {
            if (other.tag == "Player")
            {
                cogida = true;
                GameManager.Instance.GetCoin();
                anim.SetTrigger("Collected");
            }
        }
        
        if(other.tag == "Magneto")
        {
            cogida = true;
            GameManager.Instance.GetCoin();
            anim.SetTrigger("Collected");
        }
    }
    private void Update()
    {
        t += Time.deltaTime;
        if (cogida == true)
        {
            GetComponentInParent<Transform>().transform.position = Vector3.Lerp(transform.position, player.transform.position, t);
        }
    }
}
