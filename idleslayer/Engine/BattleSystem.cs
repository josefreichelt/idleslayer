namespace idleslayer;

public class BattleSystem
{
    public event Action<Enemy>? OnEnemyKilled;
    public event Action<Enemy>? OnEnemySpawned;
    private readonly LocationSystem locationSystem;

    public BattleSystem(LocationSystem locationSystem)
    {
        this.locationSystem = locationSystem;
    }


    public void BattleTick(Player player, Enemy enemy)
    {
        enemy.Health -= player.Damage;

        if (enemy.Health <= 0)
        {
            if (locationSystem.CurrentLocation.IsLast)
            {
                App.GameSystem.PauseGame();
                App.SceneManager.MenuState = MenuState.Victory;
            }
            else
            {
                OnEnemyKilled?.Invoke(enemy);
                OnEnemySpawned?.Invoke(locationSystem.CurrentLocation.GetRandomEnemy());
            }
        }
    }

}
