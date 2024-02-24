using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using UnityEngine.UIElements;


public class PlayerController : MonoBehaviour
{
    private enum PlayerInfo
    {
        None,
        DesertIsland,
        Travel
    };

    private PlayerInfo _playerInfo = PlayerInfo.None;

    private int _desertIslandCount = 3;
    private bool _isTravelWaiting = true;
    
    private GameManager _gameManager;
    private List<GameObject> _map;

    private int _currentIndex = 0;

    private int _money;

    void Start()
    {
        _gameManager = GameManager.instance;
        _map = _gameManager.map;
    }

    void Update()
    {
        if (_gameManager.currentPlayer != gameObject)
            return;

        switch (_playerInfo)
        {
            case PlayerInfo.DesertIsland:
            { 
                --_desertIslandCount;
                Debug.Log("무인도!");

                if (_desertIslandCount <= 0)
                {
                    _playerInfo = PlayerInfo.None;
                    _desertIslandCount = 3;
                }
                SetNextPlayer();

                return;
            }
            case PlayerInfo.Travel:
            {
                if (_isTravelWaiting)
                {
                    SetNextPlayer();
                    _isTravelWaiting = false;
                    return;
                }

                GameManager.instance.travelPanel.SetActive(true);
                
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main
                        .ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out RaycastHit hit)
                        && hit.collider.CompareTag("MapBlock"))
                    {
                        for (int i = 0; i < _map.Count; i++)
                        {
                            if (hit.transform.gameObject == _map[i])
                            {
                                _currentIndex = i;
                                
                                var dirPos = 
                                    _map[i].transform.position
                                    + new Vector3(0, 2f, 0);
                                
                                transform.DOJump(dirPos,
                                    10,
                                    1,
                                    0.5f);

                                break;
                            }
                        }
                        
                        GameManager.instance.travelPanel.SetActive(false);

                        GameManager.instance.state =
                                GameManager.State.Build;
                        Debug.Log(GameManager.instance.state);
                        _playerInfo = PlayerInfo.None;
                    }
                }
                
                return;
            }
        }

        if (GameManager.instance.state == GameManager.State.Ready)
        {
            GameManager.instance.state = GameManager.State.Dice;
            _gameManager.diceButton.SetActive(true);
        }
        
        switch (GameManager.instance.state)
        {
            case GameManager.State.Wait:
                break;
            case GameManager.State.Dice:
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _gameManager.RandomDice();
                    _gameManager.state = GameManager.State.Wait;
                }

                break;
            }
            case GameManager.State.Move:
            {
                _gameManager.diceValueText.SetActive(false);
                
                for (int i = 0; i < _gameManager.dice.Count; i++)
                    _gameManager.dice[i].SetActive(false);
                
                _gameManager.state = GameManager.State.Wait;
                StartCoroutine(SetPosition(_gameManager.diceValue));
                _gameManager.diceValue = 0;
                break;
            }
            case GameManager.State.Build:
            {
                Build();
                break;
            }
            case GameManager.State.NextPlayer:
            {
                SetNextPlayer();
                break;
            }
        }
    }
    
    IEnumerator SetPosition(int diceValue)
    {
        for (int i = 0; i < diceValue; i++)
        {
            var nextIndex = (_currentIndex + i + 1) % _map.Count;
            var dirPos = _map[nextIndex].transform.position
                                    + new Vector3(0, 2f, 0);
            transform.DOJump(dirPos,
                    2,
                    1,
                    0.1f);
            yield return new WaitForSeconds(diceValue > 12
                                                ? 0.05f : 0.2f);
        }

        _currentIndex = (_currentIndex + diceValue) % _map.Count;

        _gameManager.state = GameManager.State.Build;
    }

    void Build()
    {
        _gameManager.currentMapLogicIndex = _currentIndex;
        
        Map currentMap = _gameManager.map[_currentIndex].
                                GetComponent<Map>();

        _gameManager.state = GameManager.State.Wait;
        currentMap.OnLogic();
    }

    void SetNextPlayer()
    {
        if (_playerInfo != PlayerInfo.None
            || !_gameManager.isDiceDouble)
        {
            _gameManager.NextPlayer();
        }
        _gameManager.isDiceDouble = false;
                
        _gameManager.state = GameManager.State.Ready;
    }

    public void ChangePlayerInfo(Map.MapInfo info)
    {
        switch (info)
        {
            case Map.MapInfo.DesertIsland:
            {
                _playerInfo = PlayerInfo.DesertIsland;
                break;
            }
            case Map.MapInfo.Travel:
            {
                _playerInfo = PlayerInfo.Travel;
                break;
            }
        }
    }
}
