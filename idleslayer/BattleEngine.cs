namespace idleslayer;

class BattleEngine
{

    public static Player Player { get; set; } = new Player();
    public static Enemy Enemy { get; set; } = new Enemy("Skeleton", 1000, 1);

    int skeletonCounter = 1;
    public void ProcessBattle()
    {
        Enemy.Health -= Player.Damage;

        if (Enemy.Health <= 0)
        {
            Player.Gold += Enemy.Gold;
            skeletonCounter++;
            Enemy.Name = $"Skeleton {skeletonCounter}";
            Enemy.Reset();
        }
    }

    public static float ExponentialGrowth(float value, float time, float rate = 0.01f)
    {
        return (float)(value * Math.Pow((1 + rate), time));
    }
}
