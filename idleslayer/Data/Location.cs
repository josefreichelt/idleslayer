namespace idleslayer;
public class Location
{
    public int index { get; set; } = 0;
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
        var enemy = Enemies[index];
        enemy.Reset();
        return enemy;
    }
}
