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

    public Text coinTextTienda, InvenciText, MasInv, MasMag, Masx2;
    private float coinScore;

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
        valorEstrella = 100;
        valorUpgradeInv = PlayerPrefs.GetInt("UpgradeInv");
        valorUpgradeMag = PlayerPrefs.GetInt("UpgradeMag");
        valorUpgradex2 = PlayerPrefs.GetInt("Upgradex2");
        Instance = this;

        InvenciText.text = InvenciPower.ToString("0");
        coinTextTienda.text = "Tickets: " + coinScore.ToString("0");
    }
    private void Start()
    {
        InvenciText.text = InvenciPower.ToString("0");
        coinTextTienda.text = "Tickets: " + coinScore.ToString("0");
        MasInv.text = valorUpgradeInv.ToString("0");
        MasMag.text = valorUpgradeMag.ToString("0");
        Masx2.text = valorUpgradex2.ToString("0");

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

    public void SumarUpgradeInv()
    {
        if (coinScore >= valorUpgradeInv)
        {
            if(InvCooldown < 20)
            {
                InvCooldown += 2;
                PlayerPrefs.SetInt("InvCooldown", InvCooldown);
                valorUpgradeInv += 100;
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
                MagCooldown += 2;
                PlayerPrefs.SetInt("MagCooldown", MagCooldown);
                valorUpgradeMag += 100;
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
                x2Cooldown += 2;
                PlayerPrefs.SetInt("x2Cooldown", x2Cooldown);
                valorUpgradex2 += 100;
                PlayerPrefs.SetInt("Upgradex2", valorUpgradex2);
                Masx2.text = valorUpgradex2.ToString("0");
            }
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
