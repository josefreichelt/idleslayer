using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace idleslayer.Views
{
    internal class BattleScreen : CenteredWindow
    {
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
            var statusBar = new StatusBar(new StatusItem[] {
        new StatusItem(Key.q | Key.Q | Key.CtrlMask,"~CTRL-Q~ Quit",()=>{
            Application.RequestStop();
        }),
         new StatusItem(Key.s ,"~s~ Skills Shop",()=>{
             Debug.WriteLine("Pressed S");
            Game.IsBattling = false;
            Game.MenuState = MenuState.Shop;
            Game.ChangeView();
        })
        ,
         new StatusItem(Key.p , Game.IsBattling ? "~p~ Pause" :  "~p~ Unpause",()=>{
            Game.IsBattling = !Game.IsBattling;
            Title = Game.IsBattling ? "[ IdleSlayer ]" : "[ IdleSlayer - Paused ]";

        })
    });
            battleView.Y = Pos.Bottom(playerFrame);

            Debug.WriteLine("Battle screen created");
            Add(playerFrame, battleView, statusBar);
        }
    }
}
