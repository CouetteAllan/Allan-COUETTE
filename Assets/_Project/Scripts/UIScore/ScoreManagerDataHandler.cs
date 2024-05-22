using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreManagerDataHandler
{
    public static event Action<int,int> OnUpdateScore;
    public static event Action<int> OnAddScore;
    public static event Action OnStopAddingScore;
    public static event Action OnMaxScore;
    public static event Action<int> OnInitScoreManager;
    public static void UpdateScore(this ScoreManager manager, int score, int scoreMax) => OnUpdateScore?.Invoke(score,scoreMax);
    public static void AddScore(int bonusScore) => OnAddScore?.Invoke(bonusScore);
    public static void StopAddingScore() => OnStopAddingScore?.Invoke();
    public static void MaxScore(this ScoreManager manager) => OnMaxScore?.Invoke();

    public static void InitScoreManager(int scoreMax) => OnInitScoreManager?.Invoke(scoreMax);
}
