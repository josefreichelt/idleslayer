namespace idleslayer;

using Terminal.Gui;

public class ShopControls : StatusBar
{

    StatusItem backButton = new StatusItem(Key.b, "~b~ Back", () =>
    {
        App.GameSystem.ResumeGame();
        App.SceneManager.MenuState = MenuState.Battle;
    });


    public ShopControls()
    {
        AddItemAt(0, backButton);
    }


}
