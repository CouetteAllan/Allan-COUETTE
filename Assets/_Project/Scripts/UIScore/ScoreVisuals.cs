using DG.Tweening;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Collections;
using UnityEngine;

public class ScoreVisuals : MonoBehaviour
{
    [SerializeField] private MMProgressBar _progressBar;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private MMF_Player _feedbacks;

    private Coroutine _countDownCoroutine;
    private float _countdownTimer = 0;
    private void Awake()
    {
        ScoreManagerDataHandler.OnUpdateScore += OnUpdateScore;
        ScoreManagerDataHandler.OnStopAddingScore += OnStopAddingScore;
        ScoreManagerDataHandler.OnMaxScore += OnMaxScore;
    }

    private void OnMaxScore()
    {
        _feedbacks.PlayFeedbacks();
    }

    private void OnStopAddingScore()
    {

    }

    private void OnUpdateScore(int score,int maxScore)
    {
        _progressBar.TextValueMultiplier = maxScore;
        _canvasGroup.DOFade(1.0f, .5f);
        _progressBar.UpdateBar(score,0,maxScore);
        _countdownTimer = 2.5f;
        if (_countDownCoroutine != null)
            StopCoroutine(_countDownCoroutine);
        _countDownCoroutine = StartCoroutine(CountdownCoroutine());

    }
    IEnumerator CountdownCoroutine()
    {
        while(_countdownTimer > 0)
        {
            _countdownTimer -= Time.deltaTime;
            yield return null;
        }
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0.0f, .8f);
    }


    private void OnDestroy()
    {
        ScoreManagerDataHandler.OnUpdateScore -= OnUpdateScore;
        ScoreManagerDataHandler.OnStopAddingScore -= OnStopAddingScore;
        ScoreManagerDataHandler.OnMaxScore -= OnMaxScore;

    }

    public void ProgressBarFeedBackDone()
    {
        GameManager.Instance.ChangeState(GameState.Victory);
    }
}
