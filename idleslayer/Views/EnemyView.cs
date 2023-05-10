namespace idleslayer;

using System.Diagnostics;
using Terminal.Gui;

class EnemyView : FrameView
{
    Label enemyName;
    Label enemyHp;
    ProgressBar enemyHealthBar;

    public EnemyView(): base("Enemy Info")
    {
        Width = Dim.Fill();
        Height = Dim.Percent(50);
        X = Pos.Center();
        Y = Pos.Center();
        var enemy = App.GameSystem.CurrentEnemy;
        var enemyColorScheme = new ColorScheme();
        enemyColorScheme.Normal = new Terminal.Gui.Attribute(Color.Red, Color.Gray);
        enemyName = new Label($"{enemy?.Name ?? ""}") { Y = 0, X = Pos.Center() };
        enemyHp = new Label($"HP: {enemy?.Health ?? 0}") { Y = 1, X = Pos.Center() };

        enemyHealthBar = new ProgressBar()
        {
            X = Pos.Center(),
            Y = Pos.Bottom(enemyHp),
            Width = Dim.Fill(),
            Height = 1,
            Fraction = enemy?.Health ?? 0 / enemy?.HealthMax ?? 1,
            ProgressBarStyle = ProgressBarStyle.Continuous,
            ColorScheme = enemyColorScheme,
        };

        App.GameSystem.OnGameTick += HandleGameTick;
        Add(enemyName, enemyHp, enemyHealthBar);
    }
    ~EnemyView()
    {
       App.GameSystem.OnGameTick -= HandleGameTick;
    }
    public void HandleGameTick()
    {
        var enemy = App.GameSystem.CurrentEnemy;
        if(enemy == null)
        {
            return;
        }
        enemyName.Text = $"{enemy.Name}";
        enemyHp.Text = $"HP: {enemy.Health}";
        enemyHealthBar.Fraction = enemy.Health / enemy.HealthMax;
    }
}
