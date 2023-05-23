namespace idleslayer;

public class LocationSystem
{
    public Location CurrentLocation { get; private set; } = new Location();
    public List<Location> Locations { get; } = new List<Location>();
    public event Action<Location>? OnLocationChanged;

    public LocationSystem()
    {
        GenerateLocations();
        CurrentLocation = Locations[0];
    }

    public void ChangeLocation(bool isForward)
    {
        var index = isForward ? CurrentLocation.index + 1 : CurrentLocation.index - 1;
        if (index >= Locations.Count || index < 0)
        {
            return;
        }
        CurrentLocation = Locations[index];
        OnLocationChanged?.Invoke(CurrentLocation);
    }

    void GenerateLocations()
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
                new Enemy("Chicken", 40, 2),
                new Enemy("Killer Rabbit", 50, 3),
            }
        });
        Locations.Add(new Location()
        {
            Title = "Forest",
            Enemies = new List<Enemy>() {
                new Enemy("Wild Boar", 200, 7),
                new Enemy("Angry Deer", 100, 5),
                new Enemy("Bandit", 250, 10),
            }
        });
        Locations.Add(new Location()
        {
            Title = "Dark Forest",
            Enemies = new List<Enemy>() {
                new Enemy("Giant Spider", 500, 20),
                new Enemy("Wyvern", 750, 40),
            }
        });
        Locations.Add(new Location()
        {
            Title = "Graveyard",
            Enemies = new List<Enemy>() {
                new Enemy("Skeleton", 1000, 50),
                new Enemy("Zombie", 1500, 70),
                new Enemy("Ghost", 1100, 100),
                new Enemy("Grave Robber", 1200, 200),
            }
        });
        Locations.Add(new Location()
        {
            Title = "Abandoned Manor",
            Enemies = new List<Enemy>() {
                new Enemy("Royal Guard", 2000, 250),
                new Enemy("Guard Captain", 2500, 350),
            }
        });
        Locations.Add(new Location()
        {
            Title = "Throne Room",
            Enemies = new List<Enemy>() {
                new Enemy("Mad King", 10000, 3000),
            }
        });

        for (int i = 0; i <= Locations.Count - 1; i++)
        {
            Locations[i].index = i;
            if (i == Locations.Count - 1)
            {
                Locations[i].IsLast = true;
            }
            for (int e = 0; e < Locations[i].Enemies.Count; e++)
            {
                var enemy = Locations[i].Enemies[e];
                enemy.HealthMax = (float)Math.Ceiling(DataProcessor.ExponentialGrowth(enemy.HealthMax, i + e + 1, 0.5f));
                enemy.Health = enemy.HealthMax;
            }
        }

    }
}
