using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerTutorial : MonoBehaviour
{
    private const int COIN_SCORE_AMOUNT = 5;

	public static GameManagerTutorial Instance { set; get; }

    public int InvenciPower = 0;

    public bool isDead { set; get; }
    private bool isGameStarted = false;
    private bool iniciado = false;
    public static bool Once = false;
    private PlayerMotorTutorial motor;
    public GameObject tutorialCards;
    public Camera camara;
    public float pitch;
    public float pitchTimer;

    public AudioClip Loop, Main;

    // UI and UI fields
    public Animator gameCanvas, menuAnim, CoinUIAnim, TapAnim;
    public Text scoreText, coinText, modifierText;
    private float score, coinScore, modifierScore;
    private int lastScore;

    //Death menu
    public Animator deathMenuAnim;
    public Text deadScoreText, deadCoinText;

    private void Awake()
    {
        if(PlayerPrefs.GetInt("TutorialCompleto") == 1)
        {
            SceneManager.LoadScene("GameScene");
        }
        GetComponent<AudioSource>().clip = Main;
        GetComponent<AudioSource>().Play();
        if (PlayerPrefs.GetInt("Replay") == 1)
        {
            tutorialCards.SetActive(false);
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
        Once = false;
        Instance = this;
        modifierScore = 1;

        Invencibilidad.powerInvenci = false;
        Magneto.powerMagneto = false;
        X2.x2 = 1;

        modifierText.text = "x" + modifierScore.ToString("0.0");
        coinText.text = coinScore.ToString("0");
        scoreText.text = scoreText.text = score.ToString("0");


        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotorTutorial>();
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
        iniciado = true;
        menuAnim.SetTrigger("Hide");
        TapAnim.gameObject.SetActive(true);
    }
    public void Invenci()
    {
        if (InvenciPower > 0)
        {
            InvenciPower--;
            PlayerPrefs.SetInt("IntInvencibilidad", InvenciPower);

            if (Invencibilidad.powerInvenci == true)
            {
                PlayerMotorTutorial.Instance.slider.value = 0;
                PlayerMotorTutorial.Instance.speed -= 10;
                Invencibilidad.powerInvenci = false;
            }
            PlayerMotorTutorial.Instance.GetComponent<Animator>().SetTrigger("BigRunning");
            PlayerMotorTutorial.Instance.speed += 10;
            Invencibilidad.powerInvenci = true;
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
    }

    public void Home()
    {
        PlayerPrefs.SetInt("Replay", 0);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
    }
    public void Replay()
    {
        PlayerPrefs.SetInt("Replay", 1);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
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

    public void OnDeath()
    {
        GetComponent<AudioSource>().Stop();
        Pasos.pararPasos = true;
        isDead = true;
        FindObjectOfType<GlacierSpawner>().IsScrolling = false;
        deathMenuAnim.SetTrigger("Dead");
        gameCanvas.SetTrigger("Hide");

        StartCoroutine(SlidingNumbers());
    }

    private IEnumerator SlidingNumbers()
    {
        yield return new WaitForSeconds(1.5f);
        camara.GetComponent<SlidingNumber>().AddToNumber(score);
        camara.GetComponent<SlidingNumber>().AddToNumber2(coinScore);
    }
}
