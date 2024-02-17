using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] players;
    public GameObject currentPlayer;
    private int _playerIndex;
    
    public enum State
    {
        Ready,
        Dice,
        Move,
        Wait,
        Build,
        NextPlayer
    };

    public State state;
    
    public List<GameObject> map, dice;

    private float _diceDelay = 0.2f;
    public int diceValue = 0;
    
    public List<int> doubleCheckValue;
    public bool isDiceDouble = false;

    public GameObject diceButton;
    public GameObject diceValueText;

    public GameObject buildPanel;
    public GameObject travelPanel;
    
    void Awake()
    {
        state = State.Dice;
        _playerIndex = 0;
        currentPlayer = players[_playerIndex];

        instance = this;
    }

    public void RandomDice()
    {
        StartCoroutine(RandomDiceAndDelay());
    }

    IEnumerator RandomDiceAndDelay()
    {
        for (int i = 0; i < dice.Count; i++)
        {
            dice[i].transform.position = transform.position;
            yield return new WaitForSeconds(_diceDelay);
            dice[i].SetActive(true);
        }
    }

    public void NextPlayer()
    {
        _playerIndex = (++_playerIndex % players.Length);
        currentPlayer = players[_playerIndex];
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}