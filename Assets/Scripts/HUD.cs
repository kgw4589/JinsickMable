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
        GameManager gameManager = GameManager.instance;
        int mapIndex = gameManager.currentMapLogicIndex;

        Map map = gameManager.map[mapIndex].GetComponent<Map>();
        bool[] isToggleOn = new bool[toggle.Length];

        for (int i = 0; i < toggle.Length; i++)
        {
            isToggleOn[i] = toggle[i].isOn;
        }

        map.Build(isToggleOn);
        
        gameManager.buildPanel.SetActive(false);
        gameManager.state = GameManager.State.NextPlayer;
    }
}