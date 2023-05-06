namespace idleslayer;

using System.Diagnostics;
using Terminal.Gui;

class SkillsScreen : CenterFrame
{
    Label goldLabel;
    Label damageLabel;
    FrameView buttonGroup;

    Player player;

    public SkillsScreen() : base("[ Skills Shop ]")
    {
        Height = Dim.Fill(2);
        Width = Dim.Fill(2);
        player = App.GameSystem.Player;
        ColorScheme = Globals.baseColorScheme;

        CanFocus = true;
        goldLabel = new Label(player.GoldString()) { Y = 0 };
        damageLabel = new Label(player.DamageString()) { Y = 1 };

        Add(goldLabel, damageLabel, new ShopControls());
        buttonGroup = new FrameView()
        {
            Width = Dim.Fill(1),
            Height = Dim.Fill(1),
        };

        buttonGroup.Y = 3;
        buttonGroup.Title = "Available Skills";

        Add(buttonGroup);
        RenderSkillButtons();
        player.OnSkillUnlocked += HandleSkillUnlocked;
    }


    void RenderSkillButtons()
    {
        buttonGroup.RemoveAll();
        buttonGroup.CanFocus = true;
        foreach (var skill in player.SkillList)
        {
            if (!skill.IsUnlocked) continue;
            var buttonTitle = skill.ToString();
            var button = new Button(buttonTitle);
            button.CanFocus = true;
            button.IsDefault = false;
            button.Clicked += () =>
            {
                HandleSkillButtonClick(button, skill);
            };
            button.Y = skill.Index;
            buttonGroup.Add(button);
        }
    }

    // Skills got updated, so we need to re-render the buttons
    void HandleSkillUnlocked(object? sender, Skill skill)
    {
        RenderSkillButtons();
    }

    void HandleSkillButtonClick(View button, Skill skill)
    {
        if (player.Gold >= skill.Cost)
        {
            player.PurchaseSkill(skill);
            goldLabel.Text = player.GoldString();
            damageLabel.Text = player.DamageString();
            button.Text = skill.ToString();

        }
    }


    ~SkillsScreen()
    {
        player.OnSkillUnlocked -= HandleSkillUnlocked;
    }

}
