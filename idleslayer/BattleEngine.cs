namespace idleslayer;

class BattleEngine
{

    public static Player Player { get; set; } = new Player();
    public static Enemy CurrentEnemy { get; set; } = new Enemy();
    public static Location CurrentLocation { get; set; } = new Location();

    public static event EventHandler<Location>? OnLocationChanged;
    public static event EventHandler<Enemy>? OnEnemyChanged;

    public void Setup()
    {
        CurrentLocation = Globals.Locations.First();
        CurrentEnemy = CurrentLocation.GetRandomEnemy();
    }

    public void ProcessBattle()
    {
        CurrentEnemy.Health -= Player.Damage;

        if (CurrentEnemy.Health <= 0)
        {
            Player.Gold += CurrentEnemy.Gold;
            CurrentEnemy = CurrentLocation.GetRandomEnemy();
            OnEnemyChanged?.Invoke(typeof(BattleEngine), CurrentEnemy);
        }
    }

    public static float ExponentialGrowth(float value, float time, float rate = 0.01f)
    {
        return (float)(value * Math.Pow((1 + rate), time));
    }

    public static void ChangeLocation(bool isForward)
    {
        var index = isForward ? CurrentLocation.index + 1 : CurrentLocation.index - 1;
        if (index >= Globals.Locations.Count || index < 0)
        {
            index = 0;
        }
        CurrentLocation = Globals.Locations[index];
        CurrentEnemy = CurrentLocation.GetRandomEnemy();
        OnLocationChanged?.Invoke(typeof(BattleEngine), CurrentLocation);
    }
}
