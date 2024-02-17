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

    public int[] constructionCost = new int[4];

    private GameObject _myOwner;

    public void OnLogic()
    {
        switch (mapInfo)
        {
            case MapInfo.Start:
            {
                SetStateNextPlayer();
                break;
            }
            case MapInfo.DesertIsland:
            {
                ChangeCurrentPlayerInfo(1);
                SetStateNextPlayer();
                break;
            }
            case MapInfo.Normal:
            {
                GameManager.instance.buildPanel.SetActive(true);
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
                ChangeCurrentPlayerInfo(2);
                Travel();
                break;
            }
        }
    }

    void ChangeCurrentPlayerInfo(int info)
    {
        GameManager.instance.currentPlayer
            .GetComponent<PlayerController>()
            .ChangePlayerInfo(info);
    }

    void Travel()
    {
        
    }

    void SetStateNextPlayer()
    {
        GameManager.instance.state = GameManager.State.NextPlayer;
    }
}
