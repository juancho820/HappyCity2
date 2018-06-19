using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const int COIN_SCORE_AMOUNT = 5;

	public static GameManager Instance { set; get; }

    public int InvenciPower = 0;

    public bool isDead { set; get; }
    private bool isGameStarted = false;
    private bool iniciado = false;
    public static bool Once = false;
    private PlayerMotor motor;
    public Camera camara;
    public Button boton1, boton2, boton3;
    public Sprite sprite1, sprite2, sprite3, none;
    public GameObject botonPlay, botonTienda, botonInvenci;
    public float pitch;
    public float pitchTimer;

    public AudioClip BotonMain, BotonTienda, Loop, Main;

    // UI and UI fields
    public Animator gameCanvas, menuAnim, CoinUIAnim, botonAnim, jugarAnim, nivelesAnim, TapAnim;
    public Text scoreText, coinText, modifierText, hiscoreText, InvenciText;
    private float score, coinScore, modifierScore;
    private int lastScore;

    //Death menu
    public Animator deathMenuAnim;
    public Text deadScoreText, deadCoinText;

    private void Awake()
    {
        GetComponent<AudioSource>().clip = Main;
        GetComponent<AudioSource>().Play();
        if (PlayerPrefs.GetInt("Replay") == 1)
        {
            Jugar();
        }
        PlayerPrefs.SetInt("Replay", 0);
        if (!PlayerPrefs.HasKey("MagCooldown"))
        {
            PlayerPrefs.SetInt("MagCooldown", 10);
        }
        if (!PlayerPrefs.HasKey("InvCooldown"))
        {
            PlayerPrefs.SetInt("InvCooldown", 10);
        }
        if (!PlayerPrefs.HasKey("x2Cooldown"))
        {
            PlayerPrefs.SetInt("x2Cooldown", 10);
        }

        coinScore = PlayerPrefs.GetInt("Score");
        Once = false;
        Instance = this;
        modifierScore = 1;

        Invencibilidad.powerInvenci = false;
        Magneto.powerMagneto = false;
        X2.x2 = 1;

        modifierText.text = "x" + modifierScore.ToString("0.0");
        coinText.text = coinScore.ToString("0");
        scoreText.text = scoreText.text = score.ToString("0");


        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
    
        hiscoreText.text = PlayerPrefs.GetInt("Hiscore").ToString();
        botonAnim.SetTrigger("Iniciar");
    }
    private void Update()
    {
        if(pitch > 0)
        {
            pitchTimer -= Time.deltaTime;
            if(pitchTimer <= 0)
            {
                pitch = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            coinScore += 100;
        }

        if (iniciado == true)
        {
            if (MobileInput.Instance.Tap && !isGameStarted)
            {
                GetComponent<AudioSource>().clip = Loop;
                GetComponent<AudioSource>().Play();
                Once = true;
                isGameStarted = true;
                TapAnim.gameObject.SetActive(false);
                motor.StartRunning();
                FindObjectOfType<GlacierSpawner>().IsScrolling = true;
                FindObjectOfType<CamaraMotor>().IsMoving = true;
                gameCanvas.SetTrigger("Show");
                if (coinScore < 1000)
                {
                    coinText.text = coinScore.ToString("0");
                }
                if (coinScore > 1000)
                {
                    coinText.text = (coinScore / 1000).ToString("0.0 K");
                }
                if (coinScore > 1000000)
                {
                    coinText.text = (coinScore / 1000000).ToString("0.0 M");
                }
                InvenciPower = PlayerPrefs.GetInt("IntInvencibilidad");
                InvenciText.text = InvenciPower.ToString("0");
            }
        }

        if (isGameStarted && !isDead)
        {
            // Bump score up
            if(GameObject.FindGameObjectWithTag("Player").transform.position.z > 0)
            {
                score = GameObject.FindGameObjectWithTag("Player").transform.position.z;
            }
            if(lastScore != (int)score)
            {
                lastScore = (int)score;
                scoreText.text = score.ToString("0");
                if (score < 1000)
                {
                    scoreText.text = score.ToString("0");
                }
                if (score > 1000)
                {
                    scoreText.text = (score / 1000).ToString("0.0 K");
                }
                if (score > 1000000)
                {
                    scoreText.text = (score / 1000000).ToString("0.0 M");
                }
            }
        }
    }

    public void GetCoin()
    {
        pitch += 0.1f;
        pitchTimer = 0.7f;
        //CoinUIAnim.SetTrigger("Collect");
        coinScore += (1 * modifierScore) * X2.x2;

        if(coinScore < 1000)
        {
            coinText.text = coinScore.ToString("0");
        }
        if (coinScore > 1000)
        {
            coinText.text = (coinScore/1000).ToString("0.0 K");
        }
        if (coinScore > 1000000)
        {
            coinText.text = (coinScore / 1000000).ToString("0.0 M");
        }
    }

    public void Jugar()
    {
        botonPlay.GetComponent<AudioSource>().clip = BotonMain;
        botonPlay.GetComponent<AudioSource>().Play();
        iniciado = true;
        menuAnim.SetTrigger("Hide");
        TapAnim.gameObject.SetActive(true);
        botonAnim.SetTrigger("Esconder");
    }

    public void Invenci()
    {
        if (InvenciPower > 0)
        {
            InvenciPower--;
            InvenciText.text = InvenciPower.ToString("0");
            PlayerPrefs.SetInt("IntInvencibilidad", InvenciPower);

            if (Invencibilidad.powerInvenci == true)
            {
                PlayerMotor.Instance.slider.value = 0;
                PlayerMotor.Instance.speed -= 10;
            }
            PlayerMotor.Instance.GetComponent<Animator>().SetTrigger("BigRunning");
            PlayerMotor.Instance.speed += 10;
            Invencibilidad.powerInvenci = true;
            botonInvenci.GetComponent<AudioSource>().clip = PlayerMotor.Instance.InvenciAudio;
            botonInvenci.GetComponent<AudioSource>().Play();
            Pasos.iniciadoPasos = true;
        }
    }

    public void UpdateModifier(float modifierAmount)
    {
        modifierScore += modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");
    }

    public void HomePause()
    {
        Pausar();
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void Home()
    {
        PlayerPrefs.SetInt("Replay", 0);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
    public void Replay()
    {
        PlayerPrefs.SetInt("Replay", 1);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void Pausar()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Tienda()
    {
        botonTienda.GetComponent<AudioSource>().clip = BotonTienda;
        botonTienda.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Tienda");
    }

    public void OnDeath()
    {
        GetComponent<AudioSource>().Stop();
        Pasos.pararPasos = true;
        isDead = true;
        FindObjectOfType<GlacierSpawner>().IsScrolling = false;
        //deadScoreText.text = score.ToString("0");
        //deadCoinText.text = coinScore.ToString("0");
        deathMenuAnim.SetTrigger("Dead");
        gameCanvas.SetTrigger("Hide");

        StartCoroutine(SlidingNumbers());

        PlayerPrefs.SetInt("Score", (int)coinScore);
        
        /*
        if (score > 30 && score < 50)
        {
            if (boton1.image.sprite == none)
            {
                boton1.image.sprite = sprite1;
                PlayerPrefs.SetInt("CofreMadera", 1);
                //boton1.gameObject.SetActive(true);
                boton1.GetComponent<Chest>().ActivateChest();

            }
            else if (boton2.image.sprite == none)
            {
                boton2.image.sprite = sprite1;
                PlayerPrefs.SetInt("CofrePlata", 1);
                //boton2.gameObject.SetActive(true);
                boton2.GetComponent<ChestPlata>().ActivateChest();

            }
            else if (boton3.image.sprite == none)
            {
                boton3.image.sprite = sprite1;
                PlayerPrefs.SetInt("CofreOro", 1);
                //boton3.gameObject.SetActive(true);
                boton3.GetComponent<ChestOro>().ActivateChest();
            }
        }
        else if (score > 50 && score < 100)
        {
            if (boton1.image.sprite == none)
            {
                boton1.image.sprite = sprite2;
                PlayerPrefs.SetInt("CofreMadera", 2);
                //boton1.gameObject.SetActive(true);
                boton1.GetComponent<Chest>().ActivateChest();

            }
            else if (boton2.image.sprite == none)
            {
                boton2.image.sprite = sprite2;
                PlayerPrefs.SetInt("CofrePlata", 2);
                //boton2.gameObject.SetActive(true);
                boton2.GetComponent<ChestPlata>().ActivateChest();

            }
            else if (boton3.image.sprite == none)
            {
                boton3.image.sprite = sprite2;
                PlayerPrefs.SetInt("CofreOro", 2);
                //boton3.gameObject.SetActive(true);
                boton3.GetComponent<ChestOro>().ActivateChest();

            }
        }
        else if (score > 100)
        {
            if (boton1.image.sprite == none)
            {
                boton1.image.sprite = sprite3;
                //boton1.gameObject.SetActive(true);
                boton1.GetComponent<Chest>().ActivateChest();
                PlayerPrefs.SetInt("CofreMadera", 3);
            }
            else if (boton2.image.sprite == none)
            {
                boton2.image.sprite = sprite3;
                //boton2.gameObject.SetActive(true);
                boton2.GetComponent<ChestPlata>().ActivateChest();
                PlayerPrefs.SetInt("CofrePlata", 3);
            }
            else if (boton3.image.sprite == none)
            {
                boton3.image.sprite = sprite3;
                //boton3.gameObject.SetActive(true);
                boton3.GetComponent<ChestOro>().ActivateChest();
                PlayerPrefs.SetInt("CofreOro", 3);
            }
        }
        */
        //Check if this is a highscore
        if (score > PlayerPrefs.GetInt("Hiscore"))
        {
            float s = score;

            if (s % 1 == 0)
            {
                s += 1;
            }
            PlayerPrefs.SetInt("Hiscore", (int)s);
        }
    }

    private IEnumerator SlidingNumbers()
    {
        yield return new WaitForSeconds(1.5f);
        camara.GetComponent<SlidingNumber>().AddToNumber(score);
        camara.GetComponent<SlidingNumber>().AddToNumber2(coinScore);
    }
}
