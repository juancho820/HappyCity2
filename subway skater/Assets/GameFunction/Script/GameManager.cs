using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { set; get; }

    public GameObject creditos;

    private int InvenciPower = 0;
    private int GoldenT= 0;

    public AudioSource audiSourc;
    public GameObject player;

    public GameObject pausa;

    public bool isDead { set; get; }
    private bool isGameStarted = false;
    private bool iniciado = false;
    public static bool Once = false;
    public PlayerMotor motor;
    public Camera camara;
    public GameObject botonPlay, botonTienda, botonInvenci , CanvasWinSomething;

    [Header("Gifts Parameters")]
    public GameObject[] giftArray;
    public string[] giftTextArray;
    [Range(0,1)]
    public float probabWinSomeThing;
    [Space(30)]

    public float pitch;
    private float pitchTimer;

    public AudioClip BotonMain, BotonTienda, Loop, Main;

    // UI and UI fields
    public Animator gameCanvas, menuAnim, CoinUIAnim, botonAnim, TapAnim;
    public Text scoreText, coinText, modifierText, InvenciText, WinSomeThingText;
    private float score;
    private float coinScore, modifierScore;
    private int lastScore;

    //Death menu
    public Animator deathMenuAnim;
    public Text deadScoreText, deadCoinText;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        GoldenT = PlayerPrefs.GetInt("Golden");
        CanvasWinSomething.SetActive(false);
        audiSourc.clip = Main;
        audiSourc.Play();
        if (PlayerPrefs.GetInt("Replay") == 1)
        {
            Jugar();
            creditos.SetActive(false);
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
                audiSourc.clip = Loop;
                audiSourc.Play();
                Once = true;
                isGameStarted = true;
                TapAnim.gameObject.SetActive(false);
                pausa.SetActive(true);
                motor.StartRunning();
                FindObjectOfType<GlacierSpawner>().IsScrolling = true;
                FindObjectOfType<CamaraMotor>().IsMoving = true;
                gameCanvas.SetTrigger("Show");
                coinText.text = coinScore.ToString("0");
                //if (coinScore < 1000)
                //{
                //    coinText.text = coinScore.ToString("0");
                //}
                //if (coinScore > 1000)
                //{
                //    coinText.text = (coinScore / 1000).ToString("0.0 K");
                //}
                //if (coinScore > 1000000)
                //{
                //    coinText.text = (coinScore / 1000000).ToString("0.0 M");
                //}

                InvenciPower = PlayerPrefs.GetInt("IntInvencibilidad");
                InvenciText.text = InvenciPower.ToString("0");
            }
        }

        if (isGameStarted && !isDead)
        {
            // Bump score up
            if(player.transform.position.z > 0)
            {
                score = player.transform.position.z;
            }
            if(lastScore != (int)score)
            {
                lastScore = (int)score;
                scoreText.text = score.ToString("0");
                //if (score < 1000)
                //{
                //    scoreText.text = score.ToString("0");
                //}
                //if (score > 1000)
                //{
                //    scoreText.text = (score / 1000).ToString("0.0 K");
                //}
                //if (score > 1000000)
                //{
                //    scoreText.text = (score / 1000000).ToString("0.0 M");
                //}
            }
        }
    }

    public void GetCoin()
    {
        pitch += 0.1f;
        pitchTimer = 0.7f;
        coinScore += (1 * modifierScore) * X2.x2;
        coinText.text = coinScore.ToString("0");

        //if(coinScore < 1000)
        //{
        //    coinText.text = coinScore.ToString("0");
        //}
        //if (coinScore > 1000)
        //{
        //    coinText.text = (coinScore/1000).ToString("0.0 K");
        //}
        //if (coinScore > 1000000)
        //{
        //    coinText.text = (coinScore / 1000000).ToString("0.0 M");
        //}
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

            PlayerMotor.Instance.ActivateStarParticles();
            if (Invencibilidad.powerInvenci == true)
            {
                PlayerMotor.Instance.slider.value = 0;
                PlayerMotor.Instance.speed -= 10;
                Invencibilidad.powerInvenci = false;
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
        SceneManager.LoadScene("GameScene");    
    }

    public void Home()
    {
        PlayerPrefs.SetInt("Replay", 0);
        SceneManager.LoadScene("GameScene");
    }
    public void Replay()
    {
        float i = Random.Range(0f, 1f);

        if (i <= probabWinSomeThing)
        {
            WinSomeThing();
           
        }
        else
        {
            PlayerPrefs.SetInt("Replay", 1);
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }
    }
   public void WinSomeThing()
    {
        CanvasWinSomething.SetActive(true);
        int i = Random.Range(0, 3);
        giftArray[i].SetActive(true);
        WinSomeThingText.text = giftTextArray[i];
        switch(i+1)
        {
            case 1:
                PlayerPrefs.SetInt("Score", (int)coinScore + 200);
                break;
            case 2:
                PlayerPrefs.SetInt("Golden", (int)GoldenT + 1);
                break;
            case 3:
                PlayerPrefs.SetInt("IntInvencibilidad", InvenciPower + 1);
                break;
            

        }
    }
    public void ReplayAfterWin()
    {
        PlayerPrefs.SetInt("Replay", 1);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }


    public void Pausar()
    {
        if (Time.timeScale == 1)
        {
            Pasos.pararPasos = true;
            Time.timeScale = 0;
        }
        else
        {
            Pasos.iniciadoPasos = true;
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
        audiSourc.Stop();
        Pasos.pararPasos = true;
        isDead = true;
        FindObjectOfType<GlacierSpawner>().IsScrolling = false;
        deathMenuAnim.SetTrigger("Dead");
        gameCanvas.SetTrigger("Hide");

        StartCoroutine(SlidingNumbers());

        PlayerPrefs.SetInt("Score", (int)coinScore);
        
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
