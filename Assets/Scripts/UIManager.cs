using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public TMP_Text scoreText;
    public TMP_Text enemyCountText;
    public TMP_Text difficultyText;
    public TMP_Text timerText;

    void Start()
    {
        UpdateScore(0);
    }

    public void UpdateScore(int _score)
    {
        scoreText.text = "Score: " + _score;
    }

    public void UpdateEnemyCount(int _count)
    {
        enemyCountText.text = "Enemy Count: " + _count;
    }

    public void UpdateDifficulty(Difficulty _difficulty)
    {
        difficultyText.text = "Difficulty: " + _difficulty.ToString();
    }

    public void UpdateTimer(float _time)
    {
        timerText.text = "Time: " + _time.ToString("F2");
    }

    private void OnEnable()
    {
        GameEvents.OnDifficultyChanged += UpdateDifficulty;
    }

    private void OnDisable()
    {
        GameEvents.OnDifficultyChanged -= UpdateDifficulty;
    }
}
