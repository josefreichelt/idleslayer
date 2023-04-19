namespace idleslayer;

using idleslayer.Views;
using System.Diagnostics;
using Terminal.Gui;
class GameView : CenteredWindow
{
    public GameView() : base("[ Idle Slayer ]")
    {
        Modal = true;
        ColorScheme = Globals.baseColorScheme;
        this.Removed += GameView_Removed;
        Ready += GameView_Ready;
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
