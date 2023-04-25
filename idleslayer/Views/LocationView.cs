namespace idleslayer;

using System;
using Terminal.Gui;
class LocationView : View
{
    Label name;
    public LocationView()
    {
        ColorScheme = Globals.invertedColorScheme;
        Height = 1;
        X = Pos.Center();
        Y = 1;


        name = new Label($" Current Location: {BattleEngine.CurrentLocation.Title} ") { Y = 0 };
        Width = Dim.Sized(name.Text.Length);
        Add(name);
        BattleEngine.OnLocationChanged += HandleLocationChanged;
    }

    private void HandleLocationChanged(object? sender, Location e)
    {
        name.Text = $" Current Location: {e.Title} ";
        Width = Dim.Sized(name.Text.Length);
    }
}
