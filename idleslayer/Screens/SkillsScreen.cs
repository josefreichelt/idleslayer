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


        Debug.WriteLine("Skiils view created");
        Add(goldLabel, damageLabel);
        RenderButtons(damageLabel);
        Add(statusBar);

        KeyPress += SkillsScreen_KeyPress;
    }


    private void SkillsScreen_KeyPress(KeyEventEventArgs e)
    {
        Debug.WriteLine($"Key {e.KeyEvent.Key}");
    }

    void RenderButtons(View lastView)
    {
        var buttonGroup = new FrameView();
        buttonGroup.Y = Pos.Top(lastView) + 1;
        buttonGroup.Title = "Grupka";

        int skillIdx = 1;
        foreach (var skill in Globals.SkillList)
        {
            var buttonTitle = $"_{skillIdx} | {skill.ToString()}";
            var button = new Button(buttonTitle);
            button.IsDefault = false;
            button.Clicked += () =>
            {
                if (BattleEngine.Player.Gold >= skill.Cost)
                {
                    BattleEngine.Player.PurchaseSkill(skill);
                    goldLabel.Text = $"gold: {BattleEngine.Player.Gold}";
                    damageLabel.Text = $"damage: {BattleEngine.Player.Damage}";
                    skill.LevelUp();
                    button.Text = $"_{skillIdx} | {skill.ToString()}";
                }
            };
            button.Y = Pos.Top(lastView) + skillIdx + 1;
            Add(button);

            skillIdx++;
        }

    }

}
