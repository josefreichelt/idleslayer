// namespace idleslayer;
// using System.Diagnostics;
// using Terminal.Gui;

// internal class BattleScreen : CenteredWindow
// {
//     StatusBar statusBar;
//     public BattleScreen() : base("[ Battle Screen ]")
//     {
//         Height = Dim.Fill(2);
//         Width = Dim.Fill(2);
//         Modal = true;
//         ColorScheme = Globals.baseColorScheme;
//         var playerView = new PlayerView();
//         var playerFrame = new View()
//         {
//             X = Pos.Center(),
//             Y = 0,
//             Height = Dim.Percent(50),
//             Width = Dim.Fill(),
//         };
//         playerFrame.Add(playerView);
//         var battleView = new BattleView();
//         statusBar = new StatusBar(new StatusItem[] {
//                 new StatusItem(Key.CtrlMask | Key.Q,"~CTRL-Q~ - Quit",()=>{
//                     Debug.WriteLine("Pressed CTRL-Q");
//                     App.MenuState = MenuState.Exit;
//                     App.ChangeView();
//                 }),
//                 new StatusItem(Key.s ,"~s~ - Skills Shop",()=>{
//                     Debug.WriteLine("Pressed S");
//                     App.PauseGame();
//                     App.MenuState = MenuState.Shop;
//                     App.ChangeView();
//                 })
//                 ,
//                 new StatusItem(Key.p , App.IsBattling ? "~p~  - Pause" :  "~p~ - Unpause",()=>{
//                     if(App.IsBattling){
//                         App.PauseGame();
//                     } else {
//                         App.ResumeGame();
//                     }
//                 }),
//                 new StatusItem(Key.n, "~n~ - Next Location", () =>
//                         {
//                             App.BattleEngine.ChangeLocation(true);
//                         })
//             });
//         battleView.Y = Pos.Bottom(playerFrame);
//         statusBar.Y = Pos.Bottom(battleView);
//         App.OnGamePaused += OnGamePaused;
//         App.OnGameResumed += OnGameResumed;
//         Debug.WriteLine("Battle screen created");
//         App.BattleEngine.OnLocationChanged += HandleLocationChanged;
//         Add(playerFrame, battleView, statusBar);
//     }

//     private void HandleLocationChanged(object? sender, Location e)
//     {
//         var previousButtonIndex = statusBar.Items.ToList().FindIndex(v => (v.Title.ToString() ?? "").Contains("Previous"));
//         var nextButtonIndex = statusBar.Items.ToList().FindIndex(v => (v.Title.ToString() ?? "").Contains("Next"));
//         if (e.index == 0 && previousButtonIndex != -1)
//         {
//             statusBar.RemoveItem(previousButtonIndex);
//         }
//         else if (previousButtonIndex == -1)
//         {
//             statusBar.AddItemAt(statusBar.Items.Count(), new StatusItem(Key.b, "~b~ - Previous Location", () =>
//             {
//                 App.BattleEngine.ChangeLocation(false);
//             }));
//         }
//         if (e.IsLast && nextButtonIndex != -1)
//         {
//             statusBar.RemoveItem(nextButtonIndex);
//         }
//         else if (nextButtonIndex == -1)
//         {
//             statusBar.AddItemAt(statusBar.Items.Count(), new StatusItem(Key.n, "~n~ - Next Location", () =>
//              {
//                  App.BattleEngine.ChangeLocation(true);
//              }));
//         }
//     }

//     private void OnGamePaused()
//     {
//         statusBar.Items.First(
//             v =>
//             {
//                 var title = v.Title.ToString();
//                 if (title == null)
//                     return false;
//                 return title.Contains("Pause") || title.Contains("Unpause");
//             }).Title = "~p~ - Unpause";
//     }

//     private void OnGameResumed()
//     {
//         statusBar.Items.First(
//             v =>
//             {
//                 var title = v.Title.ToString();
//                 if (title == null)
//                     return false;
//                 return title.Contains("Pause") || title.Contains("Unpause");
//             }).Title = "~p~ - Pause";
//     }
// }
