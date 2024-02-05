using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum StateInfo
    {
        DiceValueText,
        DiceButton
    };

    public StateInfo state;

    public Text myText;

    void OnEnable()
    {
        switch (state)
        {
            case StateInfo.DiceValueText:
            {
                myText.text = String.Format("l {0} l",
                    GameManager.Instance.diceValue);
                break;
            }
        }
    }
    
    public void RandomDice()
    {
        GameManager.Instance.RandomDice();
        gameObject.SetActive(false);
    }
}