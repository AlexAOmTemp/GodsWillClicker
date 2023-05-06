using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _enemy;
    private int _currentStage = 1;
    private float _roundTime = 0;
    void Start()
    {
        StartRound();
    }
    void StartRound()
    {
        Debug.Log($"RoundTime {_roundTime}, Stage {_currentStage}");
        _roundTime=0;
        _player.GetComponent<PlayerBaseStats>().PlayerInit();
        _enemy.GetComponent<DemonsGenerator>().GenerateDemon(_currentStage);
        _player.GetComponent<CombatSystem>().IsDead += onAngelDead;
        _enemy.GetComponent<CombatSystem>().IsDead += onDeamonDead;
    }
    void onAngelDead()
    {
        _currentStage = 1;
        StartRound();
    }
    void onDeamonDead()
    {
        _currentStage++;
        StartRound();
    }
    void Update()
    {
        _roundTime += Time.deltaTime;
    }
}