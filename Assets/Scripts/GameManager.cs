using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState {Title, Playing, Paused, GameOver}
public enum Difficulty {Easy, Medium, Hard}

public class GameManager : Singleton<GameManager>
{
    public GameState gameState;
    public Difficulty difficulty;
    public int score;
    int scoreMultiplyer = 1;

    private void Start()
    {
        Setup();
    }

    /// <summary>
    /// Sets up the variables for our game based on the difficulty
    /// </summary>
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

    /// <summary>
    /// Adds to our score the updates the UI
    /// </summary>
    /// <param name="_score">The amount to add to the score</param>
    public void AddScore(int _score)
    {
        score += _score * scoreMultiplyer;
        _UI.UpdateScore(score);
    }

    /// <summary>
    /// Changes the difficulty then runs the setup function
    /// </summary>
    /// <param name="_difficulty">The difficulty to change to</param>
    public void ChangeDifficulty(int _difficulty)
    {
        difficulty = (Difficulty)_difficulty;
        Setup();
    }

    /// <summary>
    /// Subscribes to our events
    /// </summary>
    private void OnEnable()
    {
        GameEvents.OnGameStateChanged += OnGameStateChanged;
        GameEvents.OnEnemyHit += OnEnemyHit;
        GameEvents.OnEnemyDie += OnEnemyDie;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// Unsubscribes from our events
    /// </summary>
    private void OnDisable()
    {
        GameEvents.OnGameStateChanged -= OnGameStateChanged;
        GameEvents.OnEnemyHit -= OnEnemyDie;
        GameEvents.OnEnemyDie -= OnEnemyDie;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Runs when the On Game State Changed event fires
    /// </summary>
    /// <param name="_gameState">The new Game State</param>
    public void OnGameStateChanged(GameState _gameState)
    {
        gameState = _gameState;
    }

    /// <summary>
    /// Runs when our On Enemy Hit event fires
    /// </summary>
    /// <param name="_enemy">The enemy that was hit</param>
    void OnEnemyHit(GameObject _enemy)
    {
        AddScore(10);
    }

    /// <summary>
    /// Runs when our On Enemy Die event fires
    /// </summary>
    /// <param name="_enemy">The enemy that dies</param>
    void OnEnemyDie(GameObject _enemy)
    {
        AddScore(100);
    }

    /// <summary>
    /// On Scene Loaded runs when a new scene is loaded
    /// </summary>
    /// <param name="scene">The scene that is loaded</param>
    /// <param name="mode">The scene load mode</param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch(scene.name)
        {
            case "Title":
                GameEvents.ReportGameStateChanged(GameState.Title);
                break;
            default:
                GameEvents.ReportGameStateChanged(GameState.Playing);
                break;
        }
        GameEvents.ReportDifficultyChanged(difficulty);
    }
}