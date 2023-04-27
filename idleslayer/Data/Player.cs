namespace idleslayer;

class Player
{
    public string Name { get; set; } = "Bob";
    public int Gold { get; set; } = 99;
    public int Damage { get; set; } = 1;
    public int Xp { get; set; } = 0;
    public List<Skill> SkillList = new List<Skill>();
    public EventHandler<Skill>? OnSkillPurchased;
    public EventHandler<Skill>? OnSkillUnlocked;
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

    public string GoldString()
    {
        return $"{Gold} Gold Coins";
    }

    void GenerateSkills()
    {
        SkillList.Add(new Skill("Slash", 1, 1) { isUnlocked = true });
        SkillList.Add(new Skill("Stab", 2, 2));
        SkillList.Add(new Skill("Punch", 5, 10));


        for (int i = 0; i < SkillList.Count; i++)
        {
            SkillList[i].index = i + 1;
        }
    }

    void CheckSkillsToUnlock()
    {
        for (int i = 1; i < SkillList.Count; i++)
        {
            if (SkillList[i - 1].CurrentLevel >= 10)
            {
                if(SkillList[i].isUnlocked) continue;
                SkillList[i].isUnlocked = true;
                OnSkillUnlocked?.Invoke(this, SkillList[i]);
            }
        }
    }
}
