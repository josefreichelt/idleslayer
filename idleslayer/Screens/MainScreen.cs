namespace idleslayer;

using System;
using System.Diagnostics;
using Terminal.Gui;
public class MainScreen : CenteredWindow
{
    public MainScreen() : base("[ Idle Slayer ]")
    {
        //Modal = true;
        ColorScheme = Globals.baseColorScheme;
        Loaded += GameView_Loaded;
        App.GameSystem.OnGamePaused += OnGamePaused;
        App.GameSystem.OnGameResumed += OnGameResumed;
    }

    private void GameView_Loaded()
    {
        Debug.WriteLine("Loaded");
        App.SceneManager.RenderScene();
    }

    private void OnGamePaused()
    {
        Title = "[ Idle Slayer - Paused ]";
    }

    private void OnGameResumed()
    {
        Title = "[ Idle Slayer ]";
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

    public void ChangeView(View view)
    {
        RemoveAll();
        Add(view);
        LayoutSubviews();

    }


}
