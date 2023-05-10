namespace idleslayer;

public class Player
{
    public string Name { get; set; } = "Bob";
    int gold = 2;
    public int TotalGold { get; private set; } = 0;
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            TotalGold += value;
        }
    }

    public int Damage { get; set; } = 1;
    public int Xp { get; set; } = 0;
    public List<Skill> SkillList = new List<Skill>();
    public event EventHandler<Skill>? OnSkillPurchased;
    public event EventHandler<Skill>? OnSkillUnlocked;
    public Player()
    {
        GenerateSkills();
    }

    public void PurchaseSkill(Skill skill)
    {
        if (Gold >= skill.Cost)
        {
            Gold -= skill.Cost;
            Damage += skill.Damage;
            skill.LevelUp();
            CheckSkillsToUnlock();
            OnSkillPurchased?.Invoke(this, skill);
        }
    }

    public string NameString()
    {
        return $"Name: {Name}";
    }

    public string GoldString()
    {
        return $"Gold Coins: {Gold}";
    }

    public string DamageString()
    {
        return $"Damage: {Damage}";
    }

    void GenerateSkills()
    {
        SkillList.Add(new Skill("Punch", 1, 1) { IsUnlocked = true });
        SkillList.Add(new Skill("Stab", 2, 2));
        SkillList.Add(new Skill("Slash", 5, 3));
        SkillList.Add(new Skill("Throw Axe", 10, 5));
        SkillList.Add(new Skill("Rend", 15, 10));
        SkillList.Add(new Skill("Double Swing", 30, 20));
        SkillList.Add(new Skill("Groundstomp", 50, 30));
        SkillList.Add(new Skill("Whirlwind", 100, 40));
        SkillList.Add(new Skill("Mighty Strike", 200, 50));
        SkillList.Add(new Skill("Cleave", 300, 60));


        for (int i = 0; i < SkillList.Count; i++)
        {
            SkillList[i].Index = i + 1;
            SkillList[i].Cost = (int)Math.Ceiling(DataProcessor.ExponentialGrowth(SkillList[i].Cost, i, 0.5f));
            SkillList[i].Damage = (int)Math.Ceiling(DataProcessor.ExponentialGrowth(SkillList[i].Damage, i, 0.2f));
        }
    }

    void CheckSkillsToUnlock()
    {
        for (int i = 1; i < SkillList.Count; i++)
        {
            if (SkillList[i - 1].CurrentLevel >= 10)
            {
                if (SkillList[i].IsUnlocked) continue;
                SkillList[i].IsUnlocked = true;
                OnSkillUnlocked?.Invoke(this, SkillList[i]);
            }
        }
    }
}
