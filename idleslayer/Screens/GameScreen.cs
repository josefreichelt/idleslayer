namespace idleslayer;

using idleslayer.Views;
using System;
using System.Diagnostics;
using Terminal.Gui;
class GameScreen : CenteredWindow
{
    public GameScreen() : base("[ Idle Slayer ]")
    {
        Modal = true;
        ColorScheme = Globals.baseColorScheme;
        this.Removed += GameView_Removed;
        Ready += GameView_Ready;
        Game.OnGamePaused += OnGamePaused;
    }

    private void OnGamePaused(object? sender, bool isPaused)
    {
        Title = isPaused ? "[ IdleSlayer ]" : "[ IdleSlayer - Paused ]";
    }

    private void GameView_Ready()
    {
        Debug.WriteLine("Activating GameView");
        Application.Run(new BattleScreen());
    }


    private void GameView_Removed(View obj)
    {
        Debug.WriteLine("View removed from GameView");
    }

    public void SwapView(Toplevel view)
    {
        if (!IsCurrentTop)
        {
            Debug.WriteLine("Stopping top view");
            Application.RequestStop();
        }
        Application.Run(view);
    }


}
