namespace idleslayer;
using System.Diagnostics;
using Terminal.Gui;
public class SceneManager
{
    public MenuState MenuState { get; set; } = MenuState.MainMenu;
    public MainScreen MainScreen = new MainScreen();

    public void Init()
    {
        Application.Run(MainScreen);
    }
    public void RenderScene()
    {
        switch (MenuState)
        {
            case MenuState.MainMenu:
                MainScreen.ChangeView(new MainMenu());
                break;
            case MenuState.Battle:

                break;
            case MenuState.Shop:

                break;
            case MenuState.Exit:
                Debug.WriteLine("Exiting game");
                Application.RequestStop();
                break;
        }
        Application.Refresh();
    }
}
