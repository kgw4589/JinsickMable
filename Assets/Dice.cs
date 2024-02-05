using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    private GameManager _gameManager;
    
    public Transform dirPos;
    private float _jumpPower = 5.0f;
    private int _jumpCount = 4;
    private float _duration = 1.5f;

    public List<Vector3> dirRotation;
    private int _randomValue;

    void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    void OnEnable()
    {
        _randomValue = Random.Range(0, 6);
        Debug.Log("랜덤값" + _randomValue);
        _gameManager.diceValue += (_randomValue + 1);
        
        transform.DOJump(dirPos.position,
            _jumpPower,
            _jumpCount,
            _duration);
        
        transform.DORotate(dirRotation[_randomValue], _duration);
        
        StartCoroutine(ChangeState());
    }

    IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(_duration*1.5f);
        _gameManager.diceValueText.SetActive(true);
        
        yield return new WaitForSeconds(_duration);
        
        Debug.Log(_gameManager.diceValue);
        _gameManager.state = GameManager.State.Move;
    }
}