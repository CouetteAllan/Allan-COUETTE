using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryButtonsTween : MonoBehaviour
{
    [SerializeField] private RectTransform _leftButton, _rightButton;

    private void Awake()
    {
        GameManager.OnGameStateChange += GameManager_OnGameStateChange;
    }

    private void GameManager_OnGameStateChange(GameState newState)
    {
        if (newState != GameState.Victory)
            return;

        _rightButton.DOMoveY(500.0f, 1.5f).SetEase(Ease.OutElastic).SetDelay(4.0f).OnComplete(() =>
        {
            _leftButton.DOMoveY(500.0f, 1.5f).SetEase(Ease.OutElastic);
        });
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        GameManager.Instance.RestartScene();
    }
}
