using DG.Tweening;
using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private MMF_Player _feedbackLose;
    [SerializeField] private Transform _buttonsTransform;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _backgroundImage;

    private void Awake()
    {
        GameManager.OnGameStateChange += OnGameStateChange;
    }

    private void OnGameStateChange(GameState newState)
    {
        if(newState == GameState.GameOver)
        {
            _feedbackLose.PlayFeedbacks();
            _backgroundImage.DOFade(.8f, .5f);
            _canvasGroup.DOFade(1.0f, 1.5f).SetDelay(.5f);
            _buttonsTransform.DOMoveY(-600.0f, 1.5f).SetEase(Ease.OutBounce).SetDelay(.5f);
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void ReloadScene()
    {
        GameManager.Instance.RestartScene();
    }
}
