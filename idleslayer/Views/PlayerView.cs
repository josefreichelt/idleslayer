namespace idleslayer;
using Terminal.Gui;
class PlayerView : FrameView
{
    int goldCount = 0;
    public PlayerView()
    {
        var player = BattleEngine.Player;
        var enemy = BattleEngine.Enemy;
        AutoSize = true;
        X = Pos.Center();
        Y = 1;
        Width = Dim.Fill(4);
        Height = Dim.Fill(4);
      

        var name = new Label($"Name: {player.Name}") { Y = 0 };
        var gold = new Label($"gold: {player.Gold}") { Y = 1 };
        var damage = new Label($"damage: {player.Damage}") { Y = 2 };
        Game.OnGameTick += (_sender, _e) =>
        {
            var player = BattleEngine.Player;
            var enemy = BattleEngine.Enemy;
            gold.Text = $"gold: {player.Gold}";
        };

        Add(name, gold, damage);
    }



}
