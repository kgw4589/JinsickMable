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
        _gameManager = GameManager.instance;
    }
    
    void OnEnable()
    {
        _randomValue = Random.Range(0, 6);
        ++_randomValue;
        
        Debug.Log("랜덤값" + _randomValue);
        _gameManager.diceValue += (_randomValue);

        _gameManager.doubleCheckValue.Add(_randomValue);

        if (_gameManager.doubleCheckValue.Count == 2)
        {
            if (_gameManager.doubleCheckValue[0]
                ==_gameManager.doubleCheckValue[1])
            {
                _gameManager.isDiceDouble = true;
            }
            
            _gameManager.doubleCheckValue.Clear();
        }
        
        transform.DOJump(dirPos.position,
            _jumpPower,
            _jumpCount,
            _duration);
        
        transform.DORotate(dirRotation[_randomValue - 1], _duration);
        
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