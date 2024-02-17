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
        DiceButton,
        Build
    };

    public StateInfo state;

    public Text myText;

    public Toggle[] toggle;

    void OnEnable()
    {
        switch (state)
        {
            case StateInfo.DiceValueText:
            {
                myText.text = String.Format("l {0} l",
                    GameManager.instance.diceValue);
                break;
            }
        }
    }
    
    public void RandomDice()
    {
        GameManager.instance.RandomDice();
        gameObject.SetActive(false);
    }

    public void BuildButton()
    {
        Debug.Log("건설");
        GameManager.instance.buildPanel.SetActive(false);
        GameManager.instance.state = GameManager.State.NextPlayer;
    }
}