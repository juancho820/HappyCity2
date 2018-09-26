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
    private void FixedUpdate()
    {
        if (cogida == true)
        {
            anim.enabled = false;
            if(Invencibilidad.powerInvenci == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.9f);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.9f);
            }         

            if ((transform.position - player.transform.position).magnitude < 0.1)
            {
                GetComponentInParent<AudioSource>().pitch = 1 + GameManager.Instance.pitch;
                GetComponentInParent<AudioSource>().clip = PlayerMotor.Instance.TicketAudio;
                GetComponentInParent<AudioSource>().Play();
                gameObject.SetActive(false);
            }
        }
    }
}
