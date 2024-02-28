using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public enum MapInfo
    {
        Start,
        DesertIsland,
        Olympics,
        Travel,
        Gambling,
        Normal
    };

    public MapInfo mapInfo;

    public int[] constructionCost;

    private GameObject _myOwner;

    private PlayerController _curPlayerController;

    public void OnLogic()
    {
        SetCurrentPlayerScript();
        
        switch (mapInfo)
        {
            case MapInfo.Normal:
            {
                Normal();
                break;
            }
            case MapInfo.Start:
            {
                SetStateNextPlayer();
                break;
            }
            case MapInfo.DesertIsland:
            {
                ChangeCurrentPlayerInfo(MapInfo.DesertIsland);
                SetStateNextPlayer();
                break;
            }
            case MapInfo.Gambling:
            {
                SetStateNextPlayer();
                break;
            }
            case MapInfo.Olympics:
            {
                SetStateNextPlayer();
                break;
            }
            case MapInfo.Travel:
            {
                ChangeCurrentPlayerInfo(MapInfo.Travel);
                break;
            }
        }
    }

    void Normal()
    {
        for (int i = 0; i < constructionCost.Length; i++)
        {
            GameManager.instance.buildingCost.Add(constructionCost[i]);
        }
        
        GameManager.instance.buildPanel.SetActive(true);
    }

    void SetCurrentPlayerScript()
    {
        _curPlayerController =  GameManager.instance.
            currentPlayer.GetComponent<PlayerController>();
    }
    
    void ChangeCurrentPlayerInfo(MapInfo info)
    {
        _curPlayerController.ChangePlayerInfo(info);
    }

    public void Build(bool[] selectedBuildToggle)
    {
        GameManager.instance.buildingCost.Clear();

        if (_curPlayerController.money > GameManager.instance.totalBuildingCost)
        {
            _myOwner = GameManager.instance.currentPlayer;
            GameManager.instance.totalBuildingCost = 0;
            
            for (int i = 0; i < selectedBuildToggle.Length; i++)
            {
                if (selectedBuildToggle[i])
                {
                    Debug.Log(transform.GetChild(i).name);
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }

            GameManager.instance.state = GameManager.State.NextPlayer;
        }
        else
        {
            OnLogic();
            GameManager.instance.haveLittleMoney.SetActive(true);
        }
    }

    void SetStateNextPlayer()
    {
        GameManager.instance.state = GameManager.State.NextPlayer;
    }
}
