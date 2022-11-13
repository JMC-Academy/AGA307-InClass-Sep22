using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static event Action<Difficulty> OnDifficultyChanged = null;
    public static event Action<GameState> OnGameStateChanged = null;

    public static event Action<GameObject> OnEnemyHit = null;
    public static event Action<GameObject> OnEnemyDie = null;

    public static void ReportDifficultyChanged(Difficulty _difficulty)
    {
        OnDifficultyChanged?.Invoke(_difficulty);
    }

    public static void ReportGameStateChanged(GameState _gameState)
    {
        OnGameStateChanged?.Invoke(_gameState);
    }

    public static void ReportEnemyHit(GameObject _enemy)
    {
        OnEnemyHit?.Invoke(_enemy);
    }

    public static void ReportEnemyDie(GameObject _enemy)
    {
        OnEnemyDie?.Invoke(_enemy);
    }
}
