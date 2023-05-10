namespace idleslayer;
using System.Diagnostics;
using Terminal.Gui;
public class SceneManager
{
    MenuState _menuState = MenuState.MainMenu;
    public MenuState MenuState
    {
        get { return _menuState; }
        set
        {
            _menuState = value;
            OnMenuStateChanged?.Invoke(_menuState);
            RenderScene();
        }
    }
    public Action<MenuState>? OnMenuStateChanged;
    public MainScreen MainScreen = new MainScreen();

    public SceneManager(){
        App.GameSystem.OnGameReset += OnGameReset;
    }

    private void OnGameReset()
    {
        MenuState = MenuState.MainMenu;
    }

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
                MainScreen.ChangeView(new BattleScreen());
                break;
            case MenuState.Shop:
                MainScreen.ChangeView(new SkillsScreen());
                break;
            case MenuState.Victory:
                MainScreen.ChangeView(new VictoryScreen());
                break;
            case MenuState.Exit:
                Debug.WriteLine("Exiting game");
                Application.RequestStop();
                break;
        }
        Application.Refresh();
    }
}
