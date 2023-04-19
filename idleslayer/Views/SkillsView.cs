namespace idleslayer;

using System.Diagnostics;
using Terminal.Gui;
class SkillsView : CenteredWindow
{
    public SkillsView() : base("[ Skills Shop ]")
    {
        Height = Dim.Fill(2);
        Width = Dim.Fill(2);
        var player = BattleEngine.Player;
        ColorScheme = Globals.baseColorScheme;
        Modal = true;
        var gold = new Label($"gold: {player.Gold}") { Y = 1 };
        var damage = new Label($"damage: {player.Damage}") { Y = 2 };
        var statusBar = new StatusBar(new StatusItem[] {
        new StatusItem(Key.b, "~b~ Back",()=>{
            Debug.WriteLine("Pressed B");
            Game.MenuState = MenuState.Battle;
            Game.IsBattling = true;
            Game.ChangeView();
        }),
        
    });
        Debug.WriteLine("Skiils view created");
        Add(gold, damage,statusBar);
    }



}
