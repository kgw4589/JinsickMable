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
        Build,
        HaveLittleMoney
    };

    public StateInfo state;

    public Text myText;

    public Toggle[] toggle;
    [SerializeField] private Text[] toggleText;

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
            case StateInfo.Build:
            {
                SetToggleCost();
                break;
            }
            case StateInfo.HaveLittleMoney:
            {
                StartCoroutine(Destroy(1f));
                break;
            }
        }
    }
    
    public void RandomDice()
    {
        GameManager.instance.RandomDice();
        gameObject.SetActive(false);
    }

    void SetToggleCost()
    {
        var cost = GameManager.instance.buildingCost;
        for (int i = 0; i < cost.Count; i++)
        {
            toggleText[i].text = $"제 {i + 1}건물\n{cost[i]}원";
        }
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
            if (toggle[i].isOn)
            {
                isToggleOn[i] = true;
                GameManager.instance.totalBuildingCost += map.constructionCost[i];
            }
            else
            {
                isToggleOn[i] = false;
            }
        }

        gameManager.buildPanel.SetActive(false);
        
        map.Build(isToggleOn);
    }

    IEnumerator Destroy(float sec)
    {
        yield return new WaitForSeconds(sec);
        gameObject.SetActive(false);
    }

    public void CloseButton()
    {
        GameManager.instance.buildPanel.SetActive(false);
        GameManager.instance.buildingCost.Clear();
        GameManager.instance.state = GameManager.State.NextPlayer;
    }
}