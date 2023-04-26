namespace idleslayer;

using System;
using System.Diagnostics;
using Terminal.Gui;
class SkillsScreen : CenteredWindow
{
    Label goldLabel;
    Label damageLabel;


    public SkillsScreen() : base("[ Skills Shop ]")
    {
        Height = Dim.Fill(2);
        Width = Dim.Fill(2);
        var player = BattleEngine.Player;
        ColorScheme = Globals.baseColorScheme;
        Modal = true;
        goldLabel = new Label($"gold: {player.Gold}") { Y = 1 };
        damageLabel = new Label($"damage: {player.Damage}") { Y = 2 };
        var statusBar = new StatusBar(new StatusItem[] {
        new StatusItem(Key.b, "~b~ Back",()=>{
            Debug.WriteLine("Pressed B");
            Game.MenuState = MenuState.Battle;
            Game.IsBattling = true;
            Game.ChangeView();
        }),


    });


        Debug.WriteLine("Skils view created");
        Add(goldLabel, damageLabel);
        RenderButtons(damageLabel);
        Add(statusBar);
        var buttonGroup = new FrameView()
        {
            Width = Dim.Fill(1),
            Height = Dim.Sized(10),
        };

        buttonGroup.Y = Pos.Top(Subviews.Last()) + 5;
        buttonGroup.Title = "Grupka";

        Add(buttonGroup);
        KeyPress += SkillsScreen_KeyPress;
    }


    private void SkillsScreen_KeyPress(KeyEventEventArgs e)
    {
        Debug.WriteLine($"Key {e.KeyEvent.Key}");
    }

    void RenderButtons(View lastView)
    {
        foreach (var skill in BattleEngine.Player.SkillList)
        {
            if (!skill.isUnlocked) continue;
            var buttonTitle = skill.ToString();
            var button = new Button(buttonTitle);
            button.IsDefault = false;
            button.Clicked += () =>
            {
                HandleSkillButtonClick(button, skill);
            };
            button.Y = Pos.Top(lastView) + skill.index + 1;
            Add(button);
        }
    }



    void HandleSkillButtonClick(View button, Skill skill)
    {
        if (BattleEngine.Player.Gold >= skill.Cost)
        {
            BattleEngine.Player.PurchaseSkill(skill);
            goldLabel.Text = $"gold: {BattleEngine.Player.Gold}";
            damageLabel.Text = $"damage: {BattleEngine.Player.Damage}";
            button.Text = skill.ToString();

        }
    }

}
