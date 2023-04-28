namespace idleslayer;

class BattleEngine
{

    public static Player Player { get; set; } = new Player();
    public static Enemy CurrentEnemy { get; set; } = new Enemy();
    public static Location CurrentLocation { get; set; } = new Location();
    public static List<Location> Locations = new List<Location>();

    public static event EventHandler<Location>? OnLocationChanged;
    public static event EventHandler<Enemy>? OnEnemyChanged;

    public void Setup()
    {
        GenerateLocations();
        CurrentLocation = Locations.First();
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
        if (index >= Locations.Count || index < 0)
        {
            index = 0;
        }
        CurrentLocation = Locations[index];
        CurrentEnemy = CurrentLocation.GetRandomEnemy();
        OnLocationChanged?.Invoke(typeof(BattleEngine), CurrentLocation);
    }

     static void GenerateLocations()
    {
        Locations.Add(new Location()
        {
            Title = "Training Camp",
            Enemies = new List<Enemy>() {
                new Enemy("Training Dummy", 10, 1),
                new Enemy("Hay Stack", 20, 1),
            }
        });
        Locations.Add(new Location()
        {
            Title = "Camp Outskirts",
            Enemies = new List<Enemy>() {
                new Enemy("Chicken", 20, 2),
                new Enemy("Killer Rabbit", 50, 3),
            }
        });
        Locations.Add(new Location()
        {
            Title = "Forrest",
            Enemies = new List<Enemy>() {
                new Enemy("Wild Boar", 200, 7),
                new Enemy("Angry Deer", 100, 5),
            }
        });
        foreach (var location in Locations)
        {
            location.index = Locations.IndexOf(location);
            if (location.index == Locations.Count - 1)
            {
                location.IsLast = true;
            }
        }
    }
}
