namespace idleslayer;
using Terminal.Gui;

public class BattleScreen : CenterFrame
{
    public BattleScreen() : base("[Battle Screen]")
    {
        Height = Dim.Fill();
        Width = Dim.Fill();
        ColorScheme = Globals.baseColorScheme;
        var playerView = new PlayerView();
        var enemyView = new EnemyView();
        enemyView.Y = Pos.Bottom(playerView);
        Add(playerView, enemyView);
    }
}
