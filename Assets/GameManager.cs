using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum State
    {
        Dice,
        Move,
        Wait,
        Build
    };

    public State state;
    
    public List<GameObject> map, dice;

    private float _diceDelay = 0.2f;
    public int diceValue = 0;

    public GameObject diceButton;
    public GameObject diceValueText;

    void Awake()
    {
        state = State.Dice;
        Instance = this;
    }

    public void RandomDice()
    {
        StartCoroutine(RandomDiceDelay());
    }

    IEnumerator RandomDiceDelay()
    {
        for (int i = 0; i < dice.Count; i++)
        {
            dice[i].transform.position = transform.position;
            yield return new WaitForSeconds(_diceDelay);
            dice[i].SetActive(true);
        }
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}