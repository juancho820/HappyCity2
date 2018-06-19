using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TiendaManager : MonoBehaviour
{
	public static TiendaManager Instance { set; get; }

    public static int InvenciPower = 0;
    private int valorEstrella = 0;
    private int valorUpgradeInv = 100;
    private int valorUpgradeMag = 100;
    private int valorUpgradex2 = 100;

    public int InvCooldown = 10;
    public int MagCooldown = 10;
    public int x2Cooldown = 10;

    public GameObject Inv, Mag, X2, Invenci, GT;

    public AudioClip BotonComprar;

    public Text coinTextTienda, InvenciText, MasInv, MasMag, Masx2, GoldenTickts;
    private float coinScore;
    private float GoldenT;


    private void Awake()
    {
        if (!PlayerPrefs.HasKey("UpgradeInv"))
        {
            PlayerPrefs.SetInt("UpgradeInv", 100);
        }
        if (!PlayerPrefs.HasKey("UpgradeMag"))
        {
            PlayerPrefs.SetInt("UpgradeMag", 100);
        }
        if (!PlayerPrefs.HasKey("Upgradex2"))
        {
            PlayerPrefs.SetInt("Upgradex2", 100);
        }

        InvCooldown = PlayerPrefs.GetInt("InvCooldown");
        MagCooldown = PlayerPrefs.GetInt("MagCooldown");
        x2Cooldown = PlayerPrefs.GetInt("x2Cooldown");

        InvenciPower = PlayerPrefs.GetInt("IntInvencibilidad");
        coinScore = PlayerPrefs.GetInt("Score");
        GoldenT = PlayerPrefs.GetInt("Golden");
        valorEstrella = 100;
        valorUpgradeInv = PlayerPrefs.GetInt("UpgradeInv");
        valorUpgradeMag = PlayerPrefs.GetInt("UpgradeMag");
        valorUpgradex2 = PlayerPrefs.GetInt("Upgradex2");
        Instance = this;

        InvenciText.text = InvenciPower.ToString("0");
        coinTextTienda.text = "Tickets: " + coinScore.ToString("0");
        GoldenTickts.text = GoldenT.ToString("0");
    }
    private void Start()
    {
        InvenciText.text = InvenciPower.ToString("0");
        coinTextTienda.text = "Tickets: " + coinScore.ToString("0");
        GoldenTickts.text = GoldenT.ToString("0");
        MasInv.text = valorUpgradeInv.ToString("0");
        MasMag.text = valorUpgradeMag.ToString("0");
        Masx2.text = valorUpgradex2.ToString("0");

        if (coinScore < 1000)
        {
            coinTextTienda.text = "Tickets: " + coinScore.ToString("0");
        }
        if (coinScore > 1000)
        {
            coinTextTienda.text = "Tickets: " + (coinScore/1000).ToString("0.0 K");
        }
        if (coinScore > 1000000)
        {
            coinTextTienda.text = "Tickets: " + (coinScore/1000000).ToString("0.0 M");
        }

        if (GoldenT < 1000)
        {
            coinTextTienda.text = "Tickets: " + coinScore.ToString("0");
        }
        if (GoldenT > 1000)
        {
            coinTextTienda.text = "Tickets: " + (coinScore / 1000).ToString("0.0 K");
        }
        if (GoldenT > 1000000)
        {
            coinTextTienda.text = "Tickets: " + (coinScore / 1000000).ToString("0.0 M");
        }

    }

    public void SumarInvici()
    {
        if(coinScore >= valorEstrella)
        {
            Invenci.GetComponent<AudioSource>().clip = BotonComprar;
            Invenci.GetComponent<AudioSource>().Play();
            InvenciPower++;
            coinScore -= valorEstrella;
            PlayerPrefs.SetInt("IntInvencibilidad", InvenciPower);
            PlayerPrefs.SetInt("Score", (int)coinScore);
            coinTextTienda.text = "Tickets: " + coinScore.ToString("0");
            InvenciText.text = InvenciPower.ToString("0");
        }
    }

    public void SumarUpgradeInv()
    {
        if (coinScore >= valorUpgradeInv)
        {
            if(InvCooldown < 20)
            {
                Inv.GetComponent<AudioSource>().clip = BotonComprar;
                Inv.GetComponent<AudioSource>().Play();
                InvCooldown += 2;
                coinScore -= valorUpgradeInv;
                PlayerPrefs.SetInt("InvCooldown", InvCooldown);
                valorUpgradeInv += 100;
                PlayerPrefs.SetInt("Score", (int)coinScore);
                coinTextTienda.text = "Tickets: " + coinScore.ToString("0");
                PlayerPrefs.SetInt("UpgradeInv", valorUpgradeInv);
                MasInv.text = valorUpgradeInv.ToString("0");
            }
        }      
    }
    public void SumarUpgradeMag()
    {
        if (coinScore >= valorUpgradeMag)
        {
            if (MagCooldown < 20)
            {
                Mag.GetComponent<AudioSource>().clip = BotonComprar;
                Mag.GetComponent<AudioSource>().Play();
                MagCooldown += 2;
                coinScore -= valorUpgradeMag;
                PlayerPrefs.SetInt("MagCooldown", MagCooldown);
                valorUpgradeMag += 100;
                PlayerPrefs.SetInt("Score", (int)coinScore);
                coinTextTienda.text = "Tickets: " + coinScore.ToString("0");
                PlayerPrefs.SetInt("UpgradeMag", valorUpgradeMag);
                MasMag.text = valorUpgradeMag.ToString("0");
            }
        }
    }
    public void SumarUpgradex2()
    {
        if (coinScore >= valorUpgradex2)
        {
            if (x2Cooldown < 20)
            {
                X2.GetComponent<AudioSource>().clip = BotonComprar;
                X2.GetComponent<AudioSource>().Play();
                x2Cooldown += 2;
                coinScore -= valorUpgradex2;
                PlayerPrefs.SetInt("x2Cooldown", x2Cooldown);
                valorUpgradex2 += 100;
                PlayerPrefs.SetInt("Score", (int)coinScore);
                coinTextTienda.text = "Tickets: " + coinScore.ToString("0");
                PlayerPrefs.SetInt("Upgradex2", valorUpgradex2);
                Masx2.text = valorUpgradex2.ToString("0");
            }
        }
    }

    public void ComprarGolden()
    {
        if(coinScore >= 100)
        {
            GT.GetComponent<AudioSource>().clip = BotonComprar;
            GT.GetComponent<AudioSource>().Play();
            GoldenT++;
            coinScore -= 100;
            PlayerPrefs.SetInt("Score", (int)coinScore);
            coinTextTienda.text = "Tickets: " + coinScore.ToString("0");
            PlayerPrefs.SetInt("Golden", (int)GoldenT);
            GoldenTickts.text = GoldenT.ToString("0");
        }
    }

    public void OnPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void VolverTienda()
    {
        SceneManager.LoadScene("GameScene");
    }
}
