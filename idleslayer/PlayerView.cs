namespace idleslayer;
using Terminal.Gui;
class PlayerView : View
{
    public PlayerView()
    {
        AutoSize = true;
        X = Pos.Center();
        Y = 1;
        Width = Dim.Fill(4);
        Height = Dim.Fill(4);
        Border = new Border()
        {
            BorderStyle = BorderStyle.Rounded,
            BorderBrush = Color.Black,
            Background = Color.White,
            DrawMarginFrame = true,
        };

        ColorScheme = Colors.Base;
        var name = new Label("Name: Bob") { Y = 0 };
        var gold = new Label("💳 gold: 1") { Y = 1 };
        var damage = new Label("⚔️ damage: 1") { Y = 2 };


        Add(name, gold, damage);
    }
}
