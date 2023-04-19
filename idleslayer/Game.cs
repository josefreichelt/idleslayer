namespace idleslayer;

using idleslayer.Views;
using Terminal.Gui;

class Game
{
    public static EventHandler? OnGameTick;
    public static BattleEngine BattleEngine = new BattleEngine();
    public static bool IsBattling = true;
    public static MenuState MenuState = MenuState.Battle;

    static GameView GameView = new GameView();


    public Game()
    {

        Application.MainLoop.AddTimeout(TimeSpan.FromSeconds(1), (loop) =>
        {
            GameLoop();
            return true;
        });
        
        Application.Run(GameView);
        
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
                GameView.SwapView(new SkillsView());
                break;
            case MenuState.Exit:
                break;
        }
    }


}
