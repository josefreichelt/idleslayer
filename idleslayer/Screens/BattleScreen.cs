using System.Diagnostics;
using Terminal.Gui;

namespace idleslayer.Views
{
    internal class BattleScreen : CenteredWindow
    {
        StatusBar statusBar;
        public BattleScreen() : base("[ Battle Screen ]")
        {
            Height = Dim.Fill(2);
            Width = Dim.Fill(2);
            Modal = true;
            ColorScheme = Globals.baseColorScheme;
            var playerView = new PlayerView();
            var playerFrame = new View()
            {
                X = Pos.Center(),
                Y = 0,
                Height = Dim.Percent(50),
                Width = Dim.Fill(),
            };
            playerFrame.Add(playerView);
            var battleView = new BattleView();
            statusBar = new StatusBar(new StatusItem[] {
        new StatusItem(Key.q | Key.Q | Key.CtrlMask,"~CTRL-Q~ - Quit",()=>{
            Application.RequestStop();
        }),
         new StatusItem(Key.s ,"~s~ - Skills Shop",()=>{
             Debug.WriteLine("Pressed S");
            Game.PauseGame(true);
            Game.MenuState = MenuState.Shop;
            Game.ChangeView();
        })
        ,
         new StatusItem(Key.p , Game.IsBattling ? "~p~  - Pause" :  "~p~ - Unpause",()=>{
            Game.PauseGame(!Game.IsBattling);


        })
    });
            battleView.Y = Pos.Bottom(playerFrame);
            statusBar.Y = Pos.Bottom(battleView);

            Game.OnGamePaused += OnGamePaused;
            Debug.WriteLine("Battle screen created");
            Add(playerFrame, battleView, statusBar);
        }

        private void OnGamePaused(object? sender, bool e)
        {
            statusBar.Items.First(
                v =>
                {
                    var title = v.Title.ToString();
                    if (title == null)
                        return false;
                    return title.Contains("Pause") || title.Contains("Unpause");
                }).Title = e ? "~p~ - Pause" : "~p~ - Unpause";
        }
    }
}
