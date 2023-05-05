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
        this.Removed += GameView_Removed;
        Ready += GameView_Ready;
        Loaded += GameView_Loaded;
        Activate += GameView_Activate;
        LayoutComplete += MainScreen_LayoutComplete;
        Added += MainScreen_Added;
        DrawContentComplete += MainScreen_DrawContentComplete;
        App.GameSystem.OnGamePaused += OnGamePaused;
        App.GameSystem.OnGameResumed += OnGameResumed;
    }

    private void GameView_Loaded()
    {
        Debug.WriteLine("Loaded");
        // RemoveAll();
        // Add(new MainMenu());

        App.SceneManager.RenderScene();
    }

    private void MainScreen_Added(View obj)
    {
        Debug.WriteLine("Added");
        SetNeedsDisplay();
    }

    private void MainScreen_LayoutComplete(LayoutEventArgs obj)
    {
        Debug.WriteLine("Layout GameView");
    }

    private void MainScreen_DrawContentComplete(Rect obj)
    {
        Debug.WriteLine("Draw Complete GameView");
    }

    private void GameView_Activate(Toplevel obj)
    {
        Debug.WriteLine("Activating GameView");
        //App.SceneManager.RenderScene();
    }

    private void OnGamePaused()
    {
        Title = "[ Idle Slayer - Paused ]";
    }

    private void OnGameResumed()
    {
        Title = "[ Idle Slayer ]";
    }

    private void GameView_Ready()
    {
        Debug.WriteLine("Ready GameView");
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

    public void ChangeView(View view)
    {
        RemoveAll();
        Add(view);
        LayoutSubviews();

    }


}
