namespace idleslayer;

public class BattleSystem
{
    public event Action<Enemy>? OnEnemyKilled;
    public event Action<Enemy>? OnEnemySpawned;
    private readonly LocationSystem _locationSystem;

    public BattleSystem(LocationSystem locationSystem)
    {
        _locationSystem = locationSystem;
    }


    public void BattleTick(Player player, Enemy enemy)
    {
        enemy.Health -= player.Damage;

        if (enemy.Health <= 0)
        {
            OnEnemyKilled?.Invoke(enemy);
            OnEnemySpawned?.Invoke(_locationSystem.CurrentLocation.GetRandomEnemy());
        }
    }

}
