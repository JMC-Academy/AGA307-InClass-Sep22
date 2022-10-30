using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {Title, Playing, Paused, GameOver}
public enum Difficulty { Easy, Medium, Hard}

public class GameManager : Singleton<GameManager>
{
    public static event Action<Difficulty> OnDifficultyChanged = null; 

    public GameState gameState;
    public Difficulty difficulty;
    public int score;
    int scoreMultiplyer = 1;

    private void Start()
    {
        Setup();
        OnDifficultyChanged?.Invoke(difficulty);
    }

    void Setup()
    {
        switch(difficulty)
        {
            case Difficulty.Easy:
                scoreMultiplyer = 1;
                break;
            case Difficulty.Medium:
                scoreMultiplyer = 2;
                break;
            case Difficulty.Hard:
                scoreMultiplyer = 3;
                break;
        }
    }

    public void AddScore(int _score)
    {
        score += _score * scoreMultiplyer;
    }

    private void OnEnable()
    {
        Enemy.OnEnemyHit += OnEnemyHit;
        Enemy.OnEnemyDie += OnEnemyDie;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyHit -= OnEnemyDie;
        Enemy.OnEnemyDie -= OnEnemyDie;
    }

    void OnEnemyHit(GameObject _enemy)
    {
        AddScore(10);
    }
    void OnEnemyDie(GameObject _enemy)
    {
        AddScore(100);
    }
}