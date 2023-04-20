namespace idleslayer;

public class Skill
{
    public string Title { get; set; } = "";
    public int Cost { get; set; } = 0;
    public int Damage { get; set; } = 0;

    public Skill()
    {
    }

    public Skill(string title, int cost, int damage)
    {
        Title = title;
        Cost = cost;
        Damage = damage;
    }
}
