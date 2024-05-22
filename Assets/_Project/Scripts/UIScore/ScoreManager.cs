using MoreMountains.Feedbacks;
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
        ScoreManagerDataHandler.OnInitScoreManager += OnInitScoreManager;
    }

    public void OnInitScoreManager(int currentLevelIndex)
    {
        _maxScore += currentLevelIndex * 10;
        PlayerScore = 0;
    }

    private void OnAddScore(int bonusScore)
    {
        PlayerScore = Mathf.Clamp(PlayerScore + bonusScore,0,_maxScore);
        this.UpdateScore(PlayerScore,_maxScore);

        if(PlayerScore >=  _maxScore)
        {
            this.MaxScore();
        }
    }

    private void OnDestroy()
    {
        ScoreManagerDataHandler.OnAddScore -= OnAddScore;
        ScoreManagerDataHandler.OnInitScoreManager -= OnInitScoreManager;

    }
}
