using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : GameBehaviour
{
    float currentTime;
    bool isTiming = false;

    void Update()
    {
        if (isTiming)
            currentTime += Time.deltaTime;

        _UI.UpdateTimer(currentTime);
    }

    /// <summary>
    /// Starts the timer from zero and increments in real time seconds
    /// </summary>
    public void StartTimer()
    {
        isTiming = true;
        currentTime = 0f;
    }

    /// <summary>
    /// Will resume the timer from the previously paused time
    /// </summary>
    public void ResumeTimer()
    {
        isTiming = true;
    }

    /// <summary>
    /// Will pause the timer, with the intention to resume
    /// </summary>
    public void PauseTimer()
    {
        isTiming = false;
    }

    /// <summary>
    /// Stops the timer from timing
    /// </summary>
    public void StopTimer()
    {
        isTiming = false;
    }

    /// <summary>
    /// Gets the current time of the timer
    /// </summary>
    /// <returns>The current time of the timer</returns>
    public float GetTime()
    {
        return currentTime;
    }

    /// <summary>
    /// Are we timing or not?
    /// </summary>
    /// <returns>True if we are timing</returns>
    public bool IsTiming()
    {
        return isTiming;
    }

    void OnGameStateChanged(GameState _gameState)
    {
        switch(_gameState)
        {
            case GameState.Playing:
                isTiming = true;
                break;
            default:
                isTiming = false;
                break;
        }
    }
    private void OnEnable()
    {
        GameEvents.OnGameStateChanged += OnGameStateChanged;
    }
    private void OnDisable()
    {
        GameEvents.OnGameStateChanged -= OnGameStateChanged;
    }
}