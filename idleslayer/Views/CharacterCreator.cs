namespace idleslayer;

using System.Diagnostics;
using Terminal.Gui;

public class CharacterCreator : CenteredDialog
{

    Button createButton = new Button("Create Character");
    Label nameLabel = new Label("Hero name: ") { X = 1, Y = Pos.Center() - 2 };
    TextField nameField = new TextField() { Width = Dim.Fill(2), X = Pos.Center(), Y = Pos.Center() - 1 };
    public CharacterCreator() : base("Character Creator", 80, 6)
    {
        createButton.Clicked += CreateButton_Clicked;
        createButton.IsDefault = false;
        nameField.KeyPress += (e) =>
        {
            if (e.KeyEvent.Key == Key.Enter)
            {
                CreateButton_Clicked();
            }
        };
        Add(nameLabel, nameField);
        AddButton(createButton);
    }

    private void CreateButton_Clicked()
    {
        var name = nameField.Text.ToString();
        if (name != null && name.Length > 0)
        {
            App.GameSystem.Player.Name = name;
        }
        else
        {
            Debug.WriteLine("Invalid character name");
            return;
        }

        Application.RequestStop();
        App.SceneManager.MenuState = MenuState.Battle;

        Debug.WriteLine("Character created");

    }
}
