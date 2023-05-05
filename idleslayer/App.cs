namespace idleslayer;

using System.Diagnostics;
using Terminal.Gui;

class App
{
    public static GameSystem GameSystem = new GameSystem();
    public static SceneManager SceneManager = new SceneManager();
    public App()
    {
        Debug.WriteLine("Game created");
        Application.QuitKey = Key.Null;
        Application.MainLoop.AddTimeout(TimeSpan.FromSeconds(0.1), GameLoop);
        SceneManager.Init();
        Debug.WriteLine("Running GameView");

    }

    public bool GameLoop(MainLoop loop)
    {
        GameSystem.GameLoop();
        return true;
    }




}
