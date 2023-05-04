namespace idleslayer;

using System;
using Terminal.Gui;

public class CharacterCreator : CenteredDialog
{

    Button createButton = new Button("Create Character");
    TextField nameField = new TextField("Hero name: ") { Width = Dim.Fill(2), X = Pos.Center(), Y = Pos.Center() - 1 };
    public CharacterCreator() : base("Character Creator", 80, 6)
    {
        createButton.Clicked += CreateButton_Clicked;
        createButton.IsDefault = false;
        Add(nameField);
        AddButton(createButton);
    }

    private void CreateButton_Clicked()
    {   
        App.GameSystem.Player.Name = nameField.Text.ToString() ?? "Hero";
        Application.RequestStop();
    }
}
