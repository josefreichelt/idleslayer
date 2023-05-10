namespace idleslayer;

public class GameSystem
{
    public event Action? OnGameTick;
    public event Action? OnGamePaused;
    public event Action? OnGameResumed;
    public event Action? OnGameReset;
    public bool isPaused { get; private set; } = false;
    public BattleSystem BattleSystem;
    public LocationSystem LocationSystem;
    public Player Player = new Player();
    public Enemy? CurrentEnemy { get; private set; } = null;

    public GameSystem()
    {
        Player = new Player();
        LocationSystem = new LocationSystem();
        BattleSystem = new BattleSystem(LocationSystem);
        BattleSystem.OnEnemySpawned += OnEnemySpawned;
        LocationSystem.OnLocationChanged += OnLocationChanged;
    }

    private void OnLocationChanged(Location location)
    {
        CurrentEnemy = location.GetRandomEnemy();
    }

    private void OnEnemySpawned(Enemy enemy)
    {
        CurrentEnemy = enemy;
    }

    public void GameLoop()
    {
        if (!isPaused && CurrentEnemy != null)
        {
            BattleSystem.BattleTick(Player, CurrentEnemy);
        }
        if (!isPaused && CurrentEnemy == null)
        {
            CurrentEnemy = LocationSystem.CurrentLocation.GetRandomEnemy();
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


    public void ResetGame()
    {
        OnGameReset?.Invoke();
        BattleSystem.OnEnemySpawned -= OnEnemySpawned;
        LocationSystem.OnLocationChanged -= OnLocationChanged;
        Player = new Player();
        LocationSystem = new LocationSystem();
        LocationSystem.OnLocationChanged += OnLocationChanged;
        BattleSystem = new BattleSystem(LocationSystem);
        BattleSystem.OnEnemySpawned += OnEnemySpawned;
        ResumeGame();
    }
}
