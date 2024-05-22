using CodeMonkey.Utils;
using System;
using UnityEngine;

public enum GameState
{
    MainMenu,
    StartGame,
    InGame,
    Pause,
    Victory,
    GameOver
}

public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnGameStateChange;

    [SerializeField] private int _currentLevelIndex = 0;

    public int CurrentLevelIndex => _currentLevelIndex;

    public GameState CurrentState { get; private set; } = GameState.MainMenu;

    public CloudController Player {  get; private set; }

    private void Start()
    {
        this.ChangeState(GameState.StartGame);
    }

    public void ChangeState(GameState newState)
    {
        if (newState == CurrentState)
            return;

        CurrentState = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.StartGame:
                ScoreManagerDataHandler.InitScoreManager(_currentLevelIndex);
                FunctionTimer.Create(() => ChangeState(GameState.InGame), .5f);
                break;
            case GameState.InGame:
                break;
            case GameState.Pause:
                break;
            case GameState.Victory:
                _currentLevelIndex++;
                break;
            case GameState.GameOver:
                break;
        }

        OnGameStateChange?.Invoke(CurrentState);
    }

    public void SetPlayer(CloudController player)
    {
        Player = player;
    }
}
