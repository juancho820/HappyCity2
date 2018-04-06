using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const int COIN_SCORE_AMOUNT = 5;

	public static GameManager Instance { set; get; }

    private int InvenciPower = 0;
    private int valorEstrella = 0;
    public bool isDead { set; get; }
    private bool isGameStarted = false;
    private bool iniciado = false;
    public static bool Once = false;
    private PlayerMotor motor;
    public Camera camara;
    public Button boton1, boton2, boton3;
    public Sprite sprite1, sprite2, sprite3, none;

    // UI and UI fields
    public Animator gameCanvas, menuAnim, diamondAnim, botonAnim, TiendaAnim;
    public Text scoreText, coinText, modifierText, hiscoreText, coinTextTienda, InvenciText;
    private float score, coinScore, modifierScore;
    private int lastScore;

    //Death menu
    public Animator deathMenuAnim;
    public Text deadScoreText, deadCoinText;

    private void Awake()
    {
        InvenciPower = PlayerPrefs.GetInt("IntInvencibilidad");
        coinScore = PlayerPrefs.GetInt("Score");
        valorEstrella = 100;
        Once = false;
        Instance = this;
        modifierScore = 1;
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();

        modifierText.text = "x" + modifierScore.ToString("0.0");
        coinText.text = coinScore.ToString("0");
        scoreText.text = scoreText.text = score.ToString("0");
        InvenciText.text = InvenciPower.ToString("0");

        hiscoreText.text = PlayerPrefs.GetInt("Hiscore").ToString();
        botonAnim.SetTrigger("Iniciar");
    }
    private void Update()
    {
        if(iniciado == true)
        {
            if (MobileInput.Instance.Tap && !isGameStarted)
            {
                Once = true;
                isGameStarted = true;
                motor.StartRunning();
                FindObjectOfType<GlacierSpawner>().IsScrolling = true;
                FindObjectOfType<CamaraMotor>().IsMoving = true;
                gameCanvas.SetTrigger("Show");
                coinText.text = coinScore.ToString("0");
            }
        }

        if (isGameStarted && !isDead)
        {
            // Bump score up
            score = GameObject.FindGameObjectWithTag("Player").transform.position.z;
            if(lastScore != (int)score)
            {
                lastScore = (int)score;
                scoreText.text = score.ToString("0");
            }
        }
    }

    public void GetCoin()
    {
        //diamondAnim.SetTrigger("Collect");
        coinScore += 1 *X2.x2;
        coinText.text = coinScore.ToString("0");
        //score += COIN_SCORE_AMOUNT;
        //scoreText.text = scoreText.text = score.ToString("0");
    }

    public void Infinito()
    {
        iniciado = true;
        menuAnim.SetTrigger("Hide");
        botonAnim.SetTrigger("Esconder");
    }

    public void Invenci()
    {
        if (InvenciPower > 0)
        {
            Invencibilidad.powerInvenci = true;
            InvenciPower--;
            InvenciText.text = InvenciPower.ToString("0");
            PlayerPrefs.SetInt("IntInvencibilidad", InvenciPower);
        }
    }

    public void SumarInvici()
    {
        if(coinScore >= valorEstrella)
        {
            InvenciPower++;
            coinScore -= valorEstrella;
            PlayerPrefs.SetInt("IntInvencibilidad", InvenciPower);
            PlayerPrefs.SetInt("Score", (int)coinScore);
            coinTextTienda.text = coinScore.ToString("0");
        }
    }

    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");
    }

    public void OnPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void Tienda()
    {
        botonAnim.SetTrigger("Esconder");
        menuAnim.SetTrigger("Hide");
        TiendaAnim.SetTrigger("Show");
        coinTextTienda.text = coinScore.ToString("0");
    }

    public void VolverMenu()
    {
        menuAnim.SetTrigger("Show");
        botonAnim.SetTrigger("Iniciar");
        TiendaAnim.SetTrigger("Hide");
    }

    public void OnDeath()
    {
        isDead = true;
        FindObjectOfType<GlacierSpawner>().IsScrolling = false;
        //deadScoreText.text = score.ToString("0");
        //deadCoinText.text = coinScore.ToString("0");
        deathMenuAnim.SetTrigger("Dead");
        gameCanvas.SetTrigger("Hide");

        camara.GetComponent<SlidingNumber>().AddToNumber(score);
        camara.GetComponent<SlidingNumber>().AddToNumber2(coinScore);

        PlayerPrefs.SetInt("Score", (int)coinScore);

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
}
