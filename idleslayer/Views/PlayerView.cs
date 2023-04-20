namespace idleslayer;

using System.Diagnostics;
using Terminal.Gui;
class PlayerView : FrameView
{
    int goldCount = 0;
    Label gold;
    public PlayerView() : base("Player Info")
    {
        var player = BattleEngine.Player;
        var enemy = BattleEngine.Enemy;
        AutoSize = true;
        X = Pos.Center();
        Y = 0;
        Width = Dim.Fill();
        Height = Dim.Fill();


        var name = new Label($"Name: {player.Name}") { Y = 0 };
        gold = new Label($"gold: {player.Gold}") { Y = 1 };
        var damage = new Label($"damage: {player.Damage}") { Y = 2 };
        Game.OnGameTick += HandleGameTick;
        Debug.WriteLine("Created PLAYER VIEW");
        Add(name, gold, damage);
    }

    void HandleGameTick(object? sender, EventArgs e)
    {
        var player = BattleEngine.Player;
        var enemy = BattleEngine.Enemy;
        gold.Text = $"gold: {player.Gold}";
    }

    ~PlayerView()
    {
        Game.OnGameTick -= HandleGameTick;
        Debug.WriteLine("Disposed PLAYER VIEW");
    }


}
