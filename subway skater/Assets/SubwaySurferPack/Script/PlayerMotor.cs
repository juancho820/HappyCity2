using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    public static PlayerMotor Instance { set; get; }

    private const float LANE_DISTANCE = 3.5f;
    private const float TURN_SPEED = 0.05f;

    //
    private bool isRunning = false;

    //Animation
    private Animator anim;

    private float posicionActual;
    private float travel;
    private float posicionAnterior;
    private bool salto2;
    private bool caida;
    private int random, random2;

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
    private float originalSpeed = 7.0f;
    [SerializeField]
    private float speed = 7.0f;
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
            slider.maxValue = InvCooldown;
            slider.value += Time.deltaTime;
            if (slider.value >= InvCooldown)
            {
                Invencibilidad.powerInvenci = false;
                slider.value = 0;
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
            GameManager.Instance.UpdateModifier(speed - originalSpeed);
        }

        // Gather the inputs on which lane we should be
        if (MobileInput.Instance.SwipeLeft)
        {
            MoveLane(false);
        }
        if (MobileInput.Instance.SwipeRight)
        {
            MoveLane(true);
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
            if(caida == true)
            {
                anim.SetTrigger("Roll");
                caida = false;
            }
            verticalVelocity = -0.1f;
            if (MobileInput.Instance.SwipeUp)
            {
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
                StartSliding();
                Invoke("StopSliding", 0.6f);
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
                if(random == 1)
                {
                    anim.SetTrigger("Bajando");
                }
                else
                {
                    anim.SetTrigger("Bajando2");
                    caida = true;
                }
            }

            // Fast Falling mechanic
            if (MobileInput.Instance.SwipeDown)
            {
                verticalVelocity -= jumpForce;
                
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
    }

    private void MoveLane(bool goingRight)
    {
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
        anim.SetTrigger("Death");
        isRunning = false;
        GameManager.Instance.OnDeath();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Invencibilidad.powerInvenci == true)
        {
            if (other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Invencibilidad")
            {
                other.gameObject.SetActive(false);
            }
        }
        if(other.gameObject.tag == "bajarCamara")
        {
            CamaraMotor.agachar = true;
        }
        if (other.gameObject.tag == "subirCamara")
        {
            CamaraMotor.subir = true;
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
            }
        }
        if (Invencibilidad.powerInvenci == true)
        {
            if (hit.gameObject.tag == "Obstacle" || hit.gameObject.tag == "Invencibilidad")
            {
                hit.gameObject.SetActive(false);
            }
        }
    }
}
