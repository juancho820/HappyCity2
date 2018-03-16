using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SlidingNumber : MonoBehaviour
{
    public Text Score, Coins;
    public float animationTime = 1.5f;

    private float desiredNumber, InitialNumber, CurrentNumber;
    private float desiredNumber2, InitialNumber2, CurrentNumber2;

    public void SetNumber(float value)
    {
        InitialNumber = CurrentNumber;
        desiredNumber = value;
    }

    public void AddToNumber(float value)
    {
        InitialNumber = CurrentNumber;
        desiredNumber += value;
    }

    public void AddToNumber2(float value2)
    {
        InitialNumber2 = CurrentNumber2;
        desiredNumber2 += value2;
    }

    public void Update()
    {
        if(CurrentNumber != desiredNumber)
        {
            if (InitialNumber < desiredNumber)
            {
                CurrentNumber += (animationTime * Time.deltaTime) * (desiredNumber - InitialNumber);
                if (CurrentNumber >= desiredNumber)
                {
                    CurrentNumber = desiredNumber;
                }
            }
            else
            {
                CurrentNumber += (animationTime * Time.deltaTime) * (InitialNumber - desiredNumber);
                if (CurrentNumber <= desiredNumber)
                {
                    CurrentNumber = desiredNumber;
                }
            }

            if (InitialNumber2 < desiredNumber2)
            {
                CurrentNumber2 += (animationTime * Time.deltaTime) * (desiredNumber2 - InitialNumber2);
                if (CurrentNumber2 >= desiredNumber2)
                {
                    CurrentNumber2 = desiredNumber2;
                }
            }
            else
            {
                CurrentNumber2 += (animationTime * Time.deltaTime) * (InitialNumber2 - desiredNumber2);
                if (CurrentNumber2 <= desiredNumber2)
                {
                    CurrentNumber2 = desiredNumber2;
                }
            }

            Score.text = CurrentNumber.ToString("0");
            Coins.text = CurrentNumber2.ToString("0");
        }
    }
}
