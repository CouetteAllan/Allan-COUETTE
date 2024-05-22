using DG.Tweening;
using MoreMountains.Tools;
using UnityEngine;

public class ScoreVisuals : MonoBehaviour
{
    [SerializeField] private MMProgressBar _progressBar;
    [SerializeField] private CanvasGroup _canvasGroup;

    private void Awake()
    {
        ScoreManagerDataHandler.OnUpdateScore += OnUpdateScore;
        ScoreManagerDataHandler.OnStopAddingScore += OnStopAddingScore;
    }

    private void OnStopAddingScore()
    {
        StopUpdateScoreAndHideBar();
    }

    private void OnUpdateScore(int score,int maxScore)
    {
        _progressBar.TextValueMultiplier = maxScore;
        _canvasGroup.DOFade(1.0f, .5f);
        _progressBar.UpdateBar(score,0,maxScore);
    }

    private void StopUpdateScoreAndHideBar()
    {
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0.0f, .7f).SetDelay(2.5f);
    }


    private void OnDestroy()
    {
        ScoreManagerDataHandler.OnUpdateScore -= OnUpdateScore;
        ScoreManagerDataHandler.OnStopAddingScore -= OnStopAddingScore;

    }
}
