namespace idleslayer;
public class Location
{
    public string Title { get; set; } = "";
    public List<Enemy> Enemies { get; set; } = new List<Enemy>();

    public Location() { }
    public Location(string title, List<Enemy> enemies)
    {
        Title = title;
        Enemies = enemies;
    }

    public Enemy GetRandomEnemy()
    {
        var random = new Random();
        int index = random.Next(Enemies.Count);
        return Enemies[index];
    }
}
