using UnityEngine;
using System;
using UnityEngine.UI;

public class ChestPlata : MonoBehaviour
{
    public float msToWait = 5000.0f;

    public Text chestTimer;
    private Button chestButton;
    private Sprite sprite;
    private ulong lastChestOpen;
    private int cofre;

    private void Start()
    {
        cofre = PlayerPrefs.GetInt("CofrePlata");
        if(cofre == 1)
        {
            GetComponent<Image>().sprite = GameManager.Instance.sprite1;
        }
        else if (cofre == 2)
        {
            GetComponent<Image>().sprite = GameManager.Instance.sprite2;
        }
        else if (cofre == 3)
        {
            GetComponent<Image>().sprite = GameManager.Instance.sprite3;
        }
        chestButton = GetComponent<Button>();
        lastChestOpen = ulong.Parse(PlayerPrefs.GetString("LastChestOpen1"));
        chestTimer = GetComponentInChildren<Text>();

        if (!IsChestReady())
        {
            chestButton.interactable = false;
        }
    }

    private void Update()
    {
        if (!chestButton.IsInteractable())
        {
            if (IsChestReady())
            {
                chestButton.interactable = true;
                return;
            }

            //Set the timer
            ulong diff = ((ulong)DateTime.Now.Ticks - lastChestOpen);
            ulong m = diff / TimeSpan.TicksPerMillisecond;
            float secondsLeft = (float)(msToWait - m) / 1000.0f;

            string r = "";
            // Hours
            r += ((int)secondsLeft / 3600).ToString() + "h ";
            secondsLeft -= ((int)secondsLeft / 3600) * 3600;
            //Minutes
            r += ((int)secondsLeft / 60).ToString("00") + "m ";
            //Seconds
            r += (secondsLeft % 60).ToString("00") + "s";
            chestTimer.text = r;
        }
    }

    public void ChestClick()
    {
        //lastChestOpen = (ulong)DateTime.Now.Ticks;
        //PlayerPrefs.SetString("LastChestOpen",lastChestOpen.ToString());
        //chestButton.interactable = false;

        GetComponent<Image>().sprite = GameManager.Instance.none;
        PlayerPrefs.SetInt("CofrePlata", 0);
        //GameManager.Instance.boton1.gameObject.SetActive(false);

        //Give reward
    }
    public void ActivateChest()
    {
        lastChestOpen = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("LastChestOpen1", lastChestOpen.ToString());
        chestButton.interactable = false;
    }

    private bool IsChestReady()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - lastChestOpen);
        ulong m = diff / TimeSpan.TicksPerMillisecond;
        float secondsLeft = (float)(msToWait - m) / 1000.0f;

        if (secondsLeft < 0)
        {
            chestTimer.text = "Ready!";
            return true;
        }
        else
        {
            return false;
        }
    }

    
}
