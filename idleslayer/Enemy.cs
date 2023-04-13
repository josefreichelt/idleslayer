namespace idleslayer;

class Enemy
{
    public string Name { get; set; } = "Skeleton";
    public float Health { get; set; } = 10;
    public float HealthMax { get; set; } = 10;
    public int Gold { get; set; } = 1;
    public int Xp { get; set; } = 1;


    public Enemy()
    {
        Health = 10;
        HealthMax = 10;
        Gold = 1;
        Xp = 1;
    }
    public Enemy(string name, float health, int gold, int xp)
    {
        Name = name;
        Health = health;
        HealthMax = health;
        Gold = gold;
        Xp = xp;
    }
}
