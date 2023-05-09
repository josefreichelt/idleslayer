namespace idleslayer;

using System.Diagnostics;
using Terminal.Gui;

public class VictoryScreen : CenteredWindow
{
    Button mainMenuButton = new Button("Back to Main Menu")
    {
        X = Pos.Center(),
        Y = Pos.Center() + 3,
        ColorScheme = Globals.baseColorScheme,
        TextAlignment = TextAlignment.Centered,

    };
    Button exitButton = new Button("Exit")
    {
        X = Pos.Center(),
        Y = Pos.Center() + 5,
        ColorScheme = Globals.baseColorScheme,
        TextAlignment = TextAlignment.Centered,
    };


    const string title = @"
 _   _  _____  _____  _____  _____ ______ __   __
| | | ||_   _|/  __ \|_   _||  _  || ___ \\ \ / /
| | | |  | |  | /  \/  | |  | | | || |_/ / \ V / 
| | | |  | |  | |      | |  | | | ||    /   \ /  
\ \_/ / _| |_ | \__/\  | |  \ \_/ /| |\ \   | |  
 \___/  \___/  \____/  \_/   \___/ \_| \_|  \_/  
    ";

    Label titleLabel = new Label(title) { X = Pos.Center(), Y = Pos.Center() - 10 };

    Label totalGoldLabel = new Label($"Total Gold Aquired: {App.GameSystem.Player.TotalGold}") { X = Pos.Center() };
    public VictoryScreen() : base("Main Menu")
    {
        Height = Dim.Fill(0);
        Width = Dim.Fill(2);
        ColorScheme = Globals.baseColorScheme;
        Debug.WriteLine("Main menu created");
        exitButton.Clicked += ExitButton_Clicked;
        mainMenuButton.Clicked += MainMenuButton_Clicked;
        totalGoldLabel.Y = Pos.Bottom(titleLabel);
        Add(titleLabel, totalGoldLabel, mainMenuButton, exitButton);
    }

    private void MainMenuButton_Clicked()
    {
        App.GameSystem.ResetGame();
    }

    private void ExitButton_Clicked()
    {
        Application.RequestStop();
    }
}
