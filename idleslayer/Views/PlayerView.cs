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
        gold = new Label(player.GoldString()) { Y = 1 };
        var damage = new Label(player.DamageString()) { Y = 2 };
        var locationView = new LocationView();
        BattleEngine.OnEnemyChanged += HandleEnemyChanged;
        Add(name, gold, damage, locationView);
    }

    private void HandleEnemyChanged(object? sender, Enemy e)
    {
        var player = BattleEngine.Player;
        var enemy = BattleEngine.CurrentEnemy;
        gold.Text = player.GoldString();
    }



    ~PlayerView()
    {
        BattleEngine.OnEnemyChanged -= HandleEnemyChanged;
    }


}
