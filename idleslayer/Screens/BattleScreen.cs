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
        new StatusItem(Key.CtrlMask | Key.Q,"~CTRL-Q~ - Quit",()=>{
            Debug.WriteLine("Pressed CTRL-Q");
            Game.MenuState = MenuState.Exit;
            Game.ChangeView();
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
        }),
new StatusItem(Key.n, "~n~ - Next Location", () =>
                {
                    BattleEngine.ChangeLocation(true);
                })

        });
            battleView.Y = Pos.Bottom(playerFrame);
            statusBar.Y = Pos.Bottom(battleView);
            Game.OnGamePaused += OnGamePaused;
            Debug.WriteLine("Battle screen created");
            BattleEngine.OnLocationChanged += HandleLocationChanged;
            Add(playerFrame, battleView, statusBar);
        }

        private void HandleLocationChanged(object? sender, Location e)
        {
            var previousButtonIndex = statusBar.Items.ToList().FindIndex(v => (v.Title.ToString() ?? "").Contains("Previous"));
            var nextButtonIndex = statusBar.Items.ToList().FindIndex(v => (v.Title.ToString() ?? "").Contains("Next"));
            if (e.index == 0 && previousButtonIndex != -1)
            {
                statusBar.RemoveItem(previousButtonIndex);
            }
            else if (previousButtonIndex == -1)
            {
                statusBar.AddItemAt(statusBar.Items.Count(), new StatusItem(Key.b, "~b~ - Previous Location", () =>
                {
                    BattleEngine.ChangeLocation(false);
                }));
            }
            if (e.IsLast && nextButtonIndex != -1)
            {
                statusBar.RemoveItem(nextButtonIndex);
            }
            else if (nextButtonIndex == -1)
            {
                statusBar.AddItemAt(statusBar.Items.Count(), new StatusItem(Key.n, "~n~ - Next Location", () =>
                 {
                     BattleEngine.ChangeLocation(true);
                 }));
            }
        }

        private void OnGamePaused(bool e)
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
