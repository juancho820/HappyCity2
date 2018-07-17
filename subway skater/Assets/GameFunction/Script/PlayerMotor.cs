using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    public static PlayerMotor Instance { set; get; }

    private const float LANE_DISTANCE = 3.5f;
    private const float TURN_SPEED = 0.05f;

    //
    private bool isRunning = false;

    public AudioClip Whoosh1, Whoosh2, Whoosh3, MagnetAudio, X2Audio, InvenciAudio, Salto1, Salto2, Salto3, SlideAudio, BlockDeadAudio, BlockDeadAudio2, JumpDeadAudio, TicketAudio;

    public ParticleSystem ps;

    //Animation
    private Animator anim;

    private float posicionActual;
    private float travel;
    private float posicionAnterior;
    private bool caida;
    private bool cayo;
    private int random;
    private int LaneIn;

    public AudioSource Audio;

    public RadialSliderInv slider;
    public RadialSliderx2 slider2;
    public RadialSliderMag slider3;
    public int InvCooldown = 10;
    public int MagCooldown = 10;
    public int x2Cooldown = 10;


    // Movement
    private CharacterController controller;
    [SerializeField]
    private float jumpForce = 6.0f;
    [SerializeField]
    private float gravity = 12.0f;
    private float verticalVelocity;    
    private int desireLane = 1; // 0=L, 1=M , 2=R

    // Speed Modifier
    [SerializeField]
    private float originalSpeed = 7.0f;
    
    public float speed = 7.0f;
    private float speedTurn = 20;
    private float speedIncreaseLastTick;
    [SerializeField]
    private float speedIncreaseTime = 2.5f;
    private float speedIncreaseAmount = 0.1f;

    private void Start()
    {
        Instance = this;
        InvCooldown = PlayerPrefs.GetInt("MagCooldown");
        MagCooldown = PlayerPrefs.GetInt("InvCooldown");
        x2Cooldown = PlayerPrefs.GetInt("x2Cooldown");
        speed = originalSpeed;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        if (PlayerPrefs.GetInt("IntInvencibilidad") > 0)
        {
            slider.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (Magneto.powerMagneto == true)
        {
            slider3.gameObject.SetActive(true);
            slider3.maxValue = MagCooldown;
            slider3.value += Time.deltaTime;
            if (slider3.value >= MagCooldown)
            {
                Magneto.powerMagneto = false;
                slider3.value = 0;
                slider3.gameObject.SetActive(false);
            }
        }
        if (Invencibilidad.powerInvenci == true)
        {
            slider.gameObject.SetActive(true);
            slider.maxValue = InvCooldown;
            slider.value += Time.deltaTime;
            if (slider.value >= InvCooldown)
            {
                Pasos.iniciadoPasos = true;
                Invencibilidad.powerInvenci = false;
                anim.SetTrigger("StartRunning");
                slider.value = 0;
                speed -= 10;
                if (PlayerPrefs.GetInt("IntInvencibilidad") == 0)
                {
                    slider.gameObject.SetActive(false);
                }
            }
        }
        if (X2.x2 == 2)
        {
            slider2.gameObject.SetActive(true);
            slider2.maxValue = x2Cooldown;
            slider2.value += Time.deltaTime;
            if (slider2.value >= x2Cooldown)
            {
                X2.x2 = 1;
                slider2.value = 0;
                slider2.gameObject.SetActive(false);
            }
        }
        if (!isRunning)
        {
            return;
        }

        if(Time.time - speedIncreaseLastTick > speedIncreaseTime)
        {
            speedIncreaseLastTick = Time.time;
            speed += speedIncreaseAmount;
            GameManager.Instance.UpdateModifier(speedIncreaseAmount);
        }

        // Gather the inputs on which lane we should be
        if (MobileInput.Instance.SwipeLeft)
        {
            MoveLane(false);
            int random1;
            random1 = Random.Range(0, 3);
            switch (random1)
            {
                case 0:
                    Audio.clip = Whoosh1;
                    break;
                case 1:
                    Audio.clip = Whoosh2;
                    break;
                case 2:
                    Audio.clip = Whoosh3;
                    break;
            }
            Audio.Play();
        }
        if (MobileInput.Instance.SwipeRight)
        {
            MoveLane(true);
            int random2;
            random2 = Random.Range(0, 3);
            switch (random2)
            {
                case 0:
                    Audio.clip = Whoosh1;
                    break;
                case 1:
                    Audio.clip = Whoosh2;
                    break;
                case 2:
                    Audio.clip = Whoosh3;
                    break;
            }
            Audio.Play();
        }

        // Calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if(desireLane == 0)
        {
            targetPosition += Vector3.left * LANE_DISTANCE;
        }
        else if(desireLane == 2)
        {
            targetPosition += Vector3.right * LANE_DISTANCE;
        }

        // lets calculate our move delta
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * speedTurn;

        bool isGrounded = IsGrounded();
        anim.SetBool("Grounded", isGrounded);

        // Calculate Y
        if (isGrounded) // if Grounded
        {
            if(cayo == true)
            {
                Pasos.iniciadoPasos = true;
                cayo = false;
            }
            if (caida == true)
            {
                anim.SetTrigger("Roll");
                caida = false;
            }
            if(Invencibilidad.powerInvenci == true)
            {
                CamaraShake.shakeDuration = 0.1f;
            }
            verticalVelocity = -0.1f;
            if(Invencibilidad.powerInvenci == false)
            {
                if (MobileInput.Instance.SwipeUp)
                {
                    Pasos.pararPasos = true;
                    cayo = true;
                    int random3;
                    random3 = Random.Range(0, 3);
                    switch (random3)
                    {
                        case 0:
                            Audio.clip = Salto1;
                            break;
                        case 1:
                            Audio.clip = Salto2;
                            break;
                        case 2:
                            Audio.clip = Salto3;
                            break;
                    }
                    Audio.Play();
                    //Jump
                    random = Random.Range(1, 3);

                    if (random == 1)
                    {
                        anim.SetTrigger("Jump");
                    }
                    else
                    {
                        anim.SetTrigger("Jump2");
                    }

                    verticalVelocity = jumpForce;
                }
                else if (MobileInput.Instance.SwipeDown)
                {
                    //Slide
                    Pasos.pararPasos = true;
                    Audio.clip = SlideAudio;
                    Audio.Play();
                    StartSliding();
                    Invoke("StopSliding", 0.6f);
                }
            }          
        }
        else
        {
            verticalVelocity -= (gravity * Time.deltaTime);

            posicionActual = transform.position.y;
            travel = posicionActual - posicionAnterior;
            posicionAnterior = transform.position.y;

            if(travel < 0)
            {
                if(Invencibilidad.powerInvenci== false)
                {
                    if (random == 1)
                    {
                        anim.SetTrigger("Bajando");
                    }
                    else
                    {
                        anim.SetTrigger("Bajando2");
                        caida = true;
                    }
                }
                else
                {
                    anim.SetTrigger("BigFall");
                }
                
            }

            // Fast Falling mechanic
            if (MobileInput.Instance.SwipeDown)
            {
                verticalVelocity -= jumpForce;
                int random4;
                random4 = Random.Range(0, 3);
                switch (random4)
                {
                    case 0:
                        Audio.clip = Whoosh1;
                        break;
                    case 1:
                        Audio.clip = Whoosh2;
                        break;
                    case 2:
                        Audio.clip = Whoosh3;
                        break;
                }
                Audio.Play();

            }
        }

        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        //Move Tin
        controller.Move(moveVector * Time.deltaTime);

        // Rotate Tin to where he is going
        Vector3 dir = controller.velocity;
        if (dir != Vector3.zero)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);
        }
    }

    private void StartSliding()
    {
        anim.SetBool("Sliding", true);
        controller.height /= 2;
        controller.center = new Vector3(controller.center.x,controller.center.y/2,controller.center.z);
    }

    private void StopSliding()
    {
        anim.SetBool("Sliding", false);
        controller.height *= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y*2, controller.center.z);
        Pasos.iniciadoPasos = true;
    }

    private void MoveLane(bool goingRight)
    {
        LaneIn = desireLane;
        desireLane += (goingRight) ? 1 : -1;
        desireLane = Mathf.Clamp(desireLane, 0, 2);
    }

    private bool IsGrounded()
    {
        Ray groundRay = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f, controller.bounds.center.z), Vector3.down);
        Debug.DrawRay(groundRay.origin, groundRay.direction,Color.cyan, 1.0f);

        return Physics.Raycast(groundRay, 0.2f + 0.1f);
    }

    public void StartRunning()
    {
        isRunning = true;
        anim.SetTrigger("StartRunning");
    }

    private void Crash()
    {
        int random5 = Random.Range(0, 2);
        Debug.Log(random5);
        if(random5 == 0)
        {
            Audio.clip = BlockDeadAudio;
        }
        else
        {
            Audio.clip = BlockDeadAudio2;
        }
        Audio.Play();
        anim.SetTrigger("Death");
        isRunning = false;
        GameManager.Instance.OnDeath();
    }
    private void Crash2()
    {
        Audio.clip = JumpDeadAudio;
        Audio.Play();
        anim.SetTrigger("Death2");
        isRunning = false;
        GameManager.Instance.OnDeath();
    }
    private void Crash3()
    {
        Audio.clip = JumpDeadAudio;
        Audio.Play();
        anim.SetTrigger("Death3");
        isRunning = false;
        GameManager.Instance.OnDeath();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Invencibilidad.powerInvenci == false)
        {
            switch (other.gameObject.tag)
            {
                case "Obstacle":
                    Crash();
                    break;
                case "Obstacle3":
                    CamaraShake.shakeDuration = 0.4f;
                    desireLane = LaneIn;
                    break;
            }
        }
        if (Invencibilidad.powerInvenci == true)
        {
            if (other.gameObject.tag == "Invencibilidad")
            {
                ps.transform.position = transform.position + Vector3.forward * 5;
                ps.Play();
                other.gameObject.GetComponentInParent<PieceSpawner>().activo = false;
                if (other.GetComponentInParent<MoveForward>() != null)
                {
                    other.GetComponentInParent<MoveForward>().invenci = false;
                }
            }
        }
        if (other.gameObject.tag == "bajarCamara")
        {
            CamaraMotor.agachar = true;
        }
        if (other.gameObject.tag == "subirCamara")
        {
            CamaraMotor.subir = true;
        }
        if (other.gameObject.tag == "bajarCamaraTren")
        {
            CamaraMotor.agacharTren = true;
        }
        if (other.gameObject.tag == "subirCamaraTren")
        {
            CamaraMotor.subirTren = true;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(Invencibilidad.powerInvenci == false)
        {
            switch (hit.gameObject.tag)
            {
                case "Obstacle":
                    Crash();
                    break;
                case "Obstacle2":
                    Crash2();
                    break;
                case "Obstacle3":
                    CamaraShake.shakeDuration = 0.4f;
                    desireLane = LaneIn;
                    break;
                case "Obstacle4":
                    Crash3();
                    break;
            }
        }
        if (Invencibilidad.powerInvenci == true)
        {
            if (hit.gameObject.tag == "Obstacle" || hit.gameObject.tag == "Obstacle2" || hit.gameObject.tag == "Obstacle3" || hit.gameObject.tag == "Obstacle4" || hit.gameObject.tag == "Ramp")
            {
                Physics.IgnoreCollision(hit.collider, controller);
            }
        }
    }
}
