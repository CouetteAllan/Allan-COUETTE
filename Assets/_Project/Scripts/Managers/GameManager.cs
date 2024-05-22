using CodeMonkey.Utils;
using System;

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
                FunctionTimer.Create(() => ChangeState(GameState.InGame), .5f);
                break;
            case GameState.InGame:
                break;
            case GameState.Pause:
                break;
            case GameState.Victory:
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
