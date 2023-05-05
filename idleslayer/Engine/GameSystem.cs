namespace idleslayer;

public class GameSystem
{
    public event Action? OnGameTick;
    public event Action? OnGamePaused;
    public event Action? OnGameResumed;
    public bool isPaused { get; private set; } = false;
    public readonly BattleSystem BattleSystem;
    public readonly LocationSystem LocationSystem;
    public readonly Player Player = new Player();
    public Enemy CurrentEnemy { get; private set; } = new Enemy();

    public GameSystem()
    {
        LocationSystem = new LocationSystem();
        BattleSystem = new BattleSystem(LocationSystem);
        BattleSystem.OnEnemySpawned += OnEnemySpawned;
    }

    private void OnEnemySpawned(Enemy enemy)
    {
        CurrentEnemy = enemy;
    }

    public void GameLoop()
    {
        if (!isPaused)
        {
            BattleSystem.BattleTick(Player, CurrentEnemy);
        }
        OnGameTick?.Invoke();
    }


    public void PauseGame()
    {
        isPaused = true;
        OnGamePaused?.Invoke();
    }

    public void ResumeGame()
    {
        isPaused = false;
        OnGameResumed?.Invoke();
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
}
