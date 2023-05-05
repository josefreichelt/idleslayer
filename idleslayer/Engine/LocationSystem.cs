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
            index = 0;
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



        for (int i = 0; i < Locations.Count - 1; i++)
        {
            Locations[i].index = i;
            if (i == Locations.Count - 1)
            {
                Locations[i].IsLast = true;
            }
        }

    }
}
