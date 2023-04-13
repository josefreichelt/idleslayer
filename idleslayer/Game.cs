namespace idleslayer;
using Terminal.Gui;

class Game
{
    public static EventHandler? OnGameTick;
    public static BattleEngine BattleEngine = new BattleEngine();

    public Game()
    {
        var battleWindow = new Window("[ IdleSlayer ]");
        battleWindow.TextAlignment = TextAlignment.Right;
        var customColorScheme = new ColorScheme();
        customColorScheme.Normal = new Terminal.Gui.Attribute(Color.White, Color.Black);
        battleWindow.ColorScheme = customColorScheme;
        var playerFrame = new View()
        {
            X = Pos.Center(),
            Y = 0,
            Height = Dim.Percent(50),
            Width = Dim.Fill(),
        };
        playerFrame.Add(new PlayerView());
        var battleView = new BattleView();
        var statusBar = new StatusBar(new StatusItem[] {
        new StatusItem(Key.Q | Key.CtrlMask,"~CTRL-Q~ Quit",()=>{
            Application.RequestStop();
        })
    });
        battleView.Y = Pos.Bottom(playerFrame);
        OnGameTick += (_sender, _e) =>
        {
            battleView.HandleGameTick(BattleEngine.Enemy);
        };
        Application.MainLoop.AddTimeout(TimeSpan.FromSeconds(1), (loop) =>
          {
              GameLoop();
              return true;
          });
        battleWindow.Add(playerFrame, battleView, statusBar);
        Application.Top.Add(battleWindow);
    }

    public void Start()
    {

    }

    public void GameLoop()
    {
        BattleEngine.ProcessBattle();
        OnGameTick?.Invoke(typeof(Program), EventArgs.Empty);
    }
}
