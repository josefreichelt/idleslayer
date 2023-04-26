namespace idleslayer;

using System.Diagnostics;
using idleslayer.Views;
using Terminal.Gui;

class Game
{
    public static event EventHandler? OnGameTick;
    public static event EventHandler<bool>? OnGamePaused;
    public static BattleEngine BattleEngine = new BattleEngine();
    public static bool IsBattling = true;
    public static MenuState MenuState = MenuState.Battle;

    static GameScreen GameView = new GameScreen();


    public Game()
    {
        Debug.WriteLine("Game created");
        BattleEngine.Setup();
        Application.MainLoop.AddTimeout(TimeSpan.FromSeconds(0.1), (loop) =>
        {
            GameLoop();
            return true;
        });
        Debug.WriteLine("Running GameView");
        Application.Run(GameView);

    }

    public static void PauseGame(bool isPaused)
    {
        IsBattling = isPaused;
        OnGamePaused?.Invoke(typeof(Game), isPaused);
    }


    public void GameLoop()
    {
        if (IsBattling)
        {
            BattleEngine.ProcessBattle();
        }
        OnGameTick?.Invoke(typeof(Program), EventArgs.Empty);
    }
    public static void ChangeView()
    {
        switch (MenuState)
        {
            case MenuState.MainMenu:
                break;
            case MenuState.Battle:
                GameView.SwapView(new BattleScreen());
                break;
            case MenuState.Shop:
                GameView.SwapView(new SkillsScreen());
                break;
            case MenuState.Exit:
                break;
        }
    }


}
