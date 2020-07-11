using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    public event UnityAction EnemyTurnSkipped;

    public bool IsEnemyTurn { get; private set; } = false;

    private void OnEnable()
    {
        _player.PlayerTurnSkipped += StartTurn;
    }

    private void OnDisable()
    {
        _player.PlayerTurnSkipped -= StartTurn;
    }

    private bool _skip = false;

    private void Update()
    {
        if (_skip)
        {
            _skip = false;
            SkipTurn();
        }
    }

    private void StartTurn()
    {
        IsEnemyTurn = true;

        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (var enemy in enemies)
        {
            enemy.MakeTurn();
        }

        _skip = true;
    }

    private void SkipTurn()
    {
        IsEnemyTurn = false;
        EnemyTurnSkipped?.Invoke();
    }
}
