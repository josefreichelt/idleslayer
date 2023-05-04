namespace idleslayer;

using System.Diagnostics;
using Terminal.Gui;

public class MainMenu : CenteredWindow
{
    Button startButton = new Button("Start")
    {
        X = Pos.Center(),
        Y = Pos.Center(),
        ColorScheme = Globals.baseColorScheme,
        TextAlignment = TextAlignment.Centered,

    };
    Button exitButton = new Button("Exit")
    {
        X = Pos.Center(),
        Y = Pos.Center() + 1,
        ColorScheme = Globals.baseColorScheme,
        TextAlignment = TextAlignment.Centered,
    };


    const string title = @"
 _____     _  _           _____  _                           
|_   _|   | || |         /  ___|| |                          
  | |   __| || |  ___    \ `--. | |  __ _  _   _   ___  _ __ 
  | |  / _` || | / _ \    `--. \| | / _` || | | | / _ \| '__|
 _| |_| (_| || ||  __/   /\__/ /| || (_| || |_| ||  __/| |   
 \___/ \__,_||_| \___|   \____/ |_| \__,_| \__, | \___||_|   
                                            __/ |            
                                           |___/             
    ";

    Label titleLabel = new Label(title) { X = Pos.Center(), Y = Pos.Center() - 10 };
    public MainMenu() : base("Main Menu")
    {
        Height = Dim.Fill(0);
        Width = Dim.Fill(2);
        ColorScheme = Globals.baseColorScheme;
        Debug.WriteLine("Main menu created");
        exitButton.Clicked += ExitButton_Clicked;
        startButton.Clicked += StartButton_Clicked;
        Add(titleLabel, startButton, exitButton);
    }

    private void StartButton_Clicked()
    {
        Application.Run(new CharacterCreator());
    }

    private void ExitButton_Clicked()
    {
        Application.RequestStop();
    }
}
