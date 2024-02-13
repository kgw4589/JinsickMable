using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    private GameManager _gameManager;
    private List<GameObject> _map;

    private int _currentIndex = 0;

    void Start()
    {
        _gameManager = GameManager.Instance;
        _map = _gameManager.map;
    }

    void Update()
    {
        if (_gameManager.currentPlayer != gameObject)
            return;
        
        switch (_gameManager.state)
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
                _gameManager.diceButton.SetActive(true);
                
                if (!_gameManager.isDiceDouble)
                    _gameManager.NextPlayer();
                _gameManager.isDiceDouble = false;
                
                _gameManager.state = GameManager.State.Dice;
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
            transform.position += new Vector3(0,1.5f,0);
            yield return new WaitForSeconds(0.2f);
        }

        _currentIndex = (_currentIndex + diceValue) % _map.Count;

        _gameManager.state = GameManager.State.Build;
    }

    void Build()
    {
        Debug.Log(_gameManager.state);
        
        _gameManager.state = GameManager.State.NextPlayer;
    }
}
