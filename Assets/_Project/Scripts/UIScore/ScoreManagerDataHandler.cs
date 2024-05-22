using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreManagerDataHandler
{
    public static event Action<int,int> OnUpdateScore;
    public static event Action<int> OnAddScore;
    public static event Action OnStopAddingScore;
    public static void UpdateScore(this ScoreManager manager, int score, int scoreMax) => OnUpdateScore?.Invoke(score,scoreMax);
    public static void AddScore(int bonusScore) => OnAddScore?.Invoke(bonusScore);
    public static void StopAddingScore() => OnStopAddingScore?.Invoke();
}
