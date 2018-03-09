using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const int COIN_SCORE_AMOUNT = 5;

	public static GameManager Instance { set; get; }

    public bool isDead { set; get; }
    private bool isGameStarted = false;
    private bool iniciado = false;
    public static bool Once = false;
    private PlayerMotor motor;

    // UI and UI fields
    public Animator gameCanvas, menuAnim, diamondAnim, botonAnim;
    public Text scoreText, coinText, modifierText, hiscoreText;
    private float score, coinScore, modifierScore;
    private int lastScore;

    //Death menu
    public Animator deathMenuAnim;
    public Text deadScoreText, deadCoinText;

    private void Awake()
    {
        Once = false;
        Instance = this;
        modifierScore = 1;
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();

        modifierText.text = "x" + modifierScore.ToString("0.0");
        coinText.text = coinScore.ToString("0");
        scoreText.text = scoreText.text = score.ToString("0");

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
        diamondAnim.SetTrigger("Collect");
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

    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");
    }

    public void OnPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void OnDeath()
    {
        isDead = true;
        FindObjectOfType<GlacierSpawner>().IsScrolling = false;
        deadScoreText.text = score.ToString("0");
        deadCoinText.text = coinScore.ToString("0");
        deathMenuAnim.SetTrigger("Dead");
        gameCanvas.SetTrigger("Hide");

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
