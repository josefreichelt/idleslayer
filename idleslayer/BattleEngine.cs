namespace idleslayer;

class BattleEngine {

    public static Player Player { get; set; } = new Player();
    public static Enemy Enemy { get; set; } = new Enemy("Skeleton", 10, 1, 1);

    int skeletonCounter = 1;
    public void ProcessBattle(){
        Enemy.Health -= Player.Damage;

        if (Enemy.Health <= 0)
        {
            Player.Gold += Enemy.Gold;
            Player.Xp += Enemy.Xp;
            skeletonCounter++;
            Enemy = new Enemy($"Skeleton {skeletonCounter}", 10, 1, 1);
        }
    }
}
