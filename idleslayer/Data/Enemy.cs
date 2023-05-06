namespace idleslayer;

public class Enemy
{

    public string Name { get; set; } = "Skeleton";
    public float Health { get; set; } = 10;
    public float HealthMax { get; set; } = 10;
    public int Gold { get; set; } = 1;


    public Enemy()
    {
        Health = 10;
        HealthMax = 10;
        Gold = 1;
    }
    public Enemy(string name, float health, int gold)
    {
        Name = name;
        HealthMax = (float)Math.Ceiling(DataProcessor.ExponentialGrowth(health, 1, 0.15f));
        Health = HealthMax;
        Gold = gold;
    }

    public void Reset()
    {
        Health = HealthMax;
    }

    


}
