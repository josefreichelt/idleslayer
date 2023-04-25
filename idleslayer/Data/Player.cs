namespace idleslayer;

class Player
{
    public string Name { get; set; } = "Bob";
    public int Gold { get; set; } = 99;
    public int Damage { get; set; } = 1;
    public int Xp { get; set; } = 0;

    public void PurchaseSkill(Skill skill)
    {
        if (Gold >= skill.Cost)
        {
            Gold -= skill.Cost;
            Damage += skill.Damage;
            skill.LevelUp();
        }
    }

    public string GoldString()
    {
        return $"{Gold} Gold Coins";
    }
}
