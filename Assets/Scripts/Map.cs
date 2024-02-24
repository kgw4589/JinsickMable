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

    public void OnLogic()
    {
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
                Travel();
                break;
            }
        }
    }

    void Normal()
    {
        var buildPanel = GameManager.instance.buildPanel;
        buildPanel.SetActive(true);
        HUD hud = buildPanel.GetComponent<HUD>();

        for (int i = 0; i < constructionCost.Length; i++)
        {
            hud.toggleTxt[i].text =
                $"제 {i+1}건물\n{constructionCost[i]}원";
        }
    }

    void ChangeCurrentPlayerInfo(MapInfo info)
    {
        GameManager.instance.currentPlayer
            .GetComponent<PlayerController>()
            .ChangePlayerInfo(info);
    }

    public void Build(bool[] selectedBuildToggle)
    {
        _myOwner = GameManager.instance.currentPlayer;
        
        for (int i = 0; i < selectedBuildToggle.Length; i++)
        {
            if (selectedBuildToggle[i])
            {
                Debug.Log(transform.GetChild(i).name);
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    void Travel()
    {
        
    }

    void SetStateNextPlayer()
    {
        GameManager.instance.state = GameManager.State.NextPlayer;
    }
}
