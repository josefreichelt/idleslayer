using Terminal.Gui;
using idleslayer;
internal class Program
{
    private static void Main(string[] args)
    {
        Application.Init();

        var battleWindow = new Window("[ IdleSlayer ]");
        battleWindow.TextAlignment = TextAlignment.Right;

        var playerFrame = new View()
        {
            X = Pos.Center(),
            Y = 0,
            Height = Dim.Percent(50),
            Width = Dim.Fill(),
        };
        playerFrame.Add(new PlayerView());

        var statusBar = new StatusBar(new StatusItem[] {
        new StatusItem(Key.Q | Key.CtrlMask,"~CTRL-Q~ Quit",()=>{
            Application.RequestStop();
        })
    });

        battleWindow.Add(playerFrame, statusBar);
        Application.Top.Add(battleWindow);

        Application.Run();
        Application.Shutdown();
    }
}
