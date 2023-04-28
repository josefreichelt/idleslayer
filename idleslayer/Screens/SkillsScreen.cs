namespace idleslayer;

using System.Diagnostics;
using Terminal.Gui;

class SkillsScreen : CenteredWindow
{
    Label goldLabel;
    Label damageLabel;
    FrameView buttonGroup;

    Player player;

    public SkillsScreen() : base("[ Skills Shop ]")
    {
        Height = Dim.Fill(2);
        Width = Dim.Fill(2);
        player = BattleEngine.Player;
        ColorScheme = Globals.baseColorScheme;
        Modal = true;

        goldLabel = new Label(player.GoldString()) { Y = 0 };
        damageLabel = new Label(player.DamageString()) { Y = 1 };
        var statusBar = new StatusBar(new StatusItem[] {
        new StatusItem(Key.b, "~b~ Back",()=>{
                Game.MenuState = MenuState.Battle;
                Game.IsBattling = true;
                Game.ChangeView();
            })
        });
        Add(goldLabel, damageLabel, statusBar);
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
        foreach (var skill in player.SkillList)
        {
            if (!skill.isUnlocked) continue;
            var buttonTitle = skill.ToString();
            var button = new Button(buttonTitle);
            button.IsDefault = false;
            button.Clicked += () =>
            {
                HandleSkillButtonClick(button, skill);
            };
            button.Y = skill.index;
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
