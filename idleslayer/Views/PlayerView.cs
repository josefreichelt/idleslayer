namespace idleslayer;

using System;
using Terminal.Gui;
class PlayerView : FrameView
{
    Player _player;
    Label gold;
    public PlayerView() : base("Player Info")
    {
        _player = App.GameSystem.Player;
        AutoSize = true;
        X = Pos.Center();
        Y = 0;
        Width = Dim.Fill();
        Height = Dim.Percent(50);


        var name = new Label(_player.NameString()) { Y = 0 };
        gold = new Label(_player.GoldString()) { Y = 1 };
        var damage = new Label(_player.DamageString()) { Y = 2 };
        var locationView = new LocationView();
        Add(name, gold, damage, locationView);
        App.GameSystem.BattleSystem.OnEnemyKilled += HandleEnemyKilled;
    }

    private void HandleEnemyKilled(Enemy enemy)
    {
        _player.Gold += enemy.Gold;
        gold.Text = _player.GoldString();
    }
}
