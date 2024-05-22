using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int _maxScore;
    public int PlayerScore { get; private set; } = 0;

    private void Awake()
    {
        ScoreManagerDataHandler.OnAddScore += OnAddScore;

    }

    private void OnAddScore(int bonusScore)
    {
        PlayerScore+= bonusScore;
        this.UpdateScore(PlayerScore,_maxScore);
    }

    private void OnDestroy()
    {
        ScoreManagerDataHandler.OnAddScore -= OnAddScore;
    }
}
