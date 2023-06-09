namespace idleslayer;

public class Skill
{
    public int Index { get; set; } = 0;
    public string Title { get; set; } = "";
    public int Cost { get; set; } = 0;
    public int BaseCost { get; set; } = 0;
    public int Damage { get; set; } = 0;
    public int CurrentLevel { get; set; } = 0;
    public bool IsUnlocked { get; set; } = false;

    public Skill()
    {
    }

    public Skill(string title, int cost, int damage)
    {
        Title = title;
        BaseCost = cost;
        Cost = (int)Math.Ceiling(DataProcessor.ExponentialGrowth(BaseCost, 1, 0.15f));
        Damage = damage;
    }

    public void LevelUp()
    {
        CurrentLevel++;
        Cost = (int)Math.Ceiling(DataProcessor.ExponentialGrowth(BaseCost, CurrentLevel, 0.15f));
    }

    public override string ToString()
    {
        return $"_{Index} | LVL:{CurrentLevel} - {Title} - {Cost} gold - {Damage} damage";
    }
}
