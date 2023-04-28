namespace idleslayer;

using System.Diagnostics;
using idleslayer.Views;
using Terminal.Gui;

class Game
{
    public static event Action? OnGameTick;
    public static event Action<bool>? OnGamePaused;
    public static BattleEngine BattleEngine = new BattleEngine();
    public static bool IsBattling = true;
    public static MenuState MenuState = MenuState.Shop;

    static GameScreen GameScreen = new GameScreen();


    public Game()
    {
        Application.QuitKey = Key.Null;
        Debug.WriteLine("Game created");
        BattleEngine.Setup();
        Application.MainLoop.AddTimeout(TimeSpan.FromSeconds(0.1), (loop) =>
        {
            GameLoop();
            return true;
        });
        Debug.WriteLine("Running GameView");
        Application.Run(GameScreen);

    }

    public static void PauseGame(bool isPaused)
    {
        IsBattling = isPaused;
        OnGamePaused?.Invoke(isPaused);
    }


    public void GameLoop()
    {
        if (IsBattling)
        {
            BattleEngine.ProcessBattle();
        }
        if(MenuState == MenuState.Exit){
            Application.RequestStop();
        }
        OnGameTick?.Invoke();
    }

    public static void ChangeView()
    {
        switch (MenuState)
        {
            case MenuState.MainMenu:
                break;
            case MenuState.Battle:
                GameScreen.SwapView(new BattleScreen());
                break;
            case MenuState.Shop:
                GameScreen.SwapView(new SkillsScreen());
                break;
            case MenuState.Exit:
                Debug.WriteLine("Exiting game");
                Application.RequestStop();
                break;
        }
    }


}
