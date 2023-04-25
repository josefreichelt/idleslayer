namespace idleslayer;

using System.Diagnostics;
using Terminal.Gui;
class PlayerView : FrameView
{
    Label gold;
    public PlayerView() : base("Player Info")
    {
        var player = BattleEngine.Player;
        var enemy = BattleEngine.CurrentEnemy;
        AutoSize = true;
        X = Pos.Center();
        Y = 0;
        Width = Dim.Fill();
        Height = Dim.Fill();


        var name = new Label($"Name: {player.Name}") { Y = 0 };
        gold = new Label(player.GoldString()) { Y = 0 };
        gold.X = Pos.AnchorEnd() - gold.Text.Length;
        var damage = new Label($"damage: {player.Damage}") { Y = 1 };
        damage.X = Pos.AnchorEnd() - damage.Text.Length;
        var locationView = new LocationView();
        BattleEngine.OnEnemyChanged += HandleEnemyChanged;
        Add(name, gold, damage, locationView);
    }

    private void HandleEnemyChanged(object? sender, Enemy e)
    {
        var player = BattleEngine.Player;
        var enemy = BattleEngine.CurrentEnemy;
        gold.Text = player.GoldString();
        gold.X = Pos.AnchorEnd() - gold.Text.Length;
    }



    ~PlayerView()
    {
        BattleEngine.OnEnemyChanged -= HandleEnemyChanged;
    }


}
