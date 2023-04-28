namespace idleslayer;

using System;
using Terminal.Gui;
class LocationView : View
{
    Label name;
    Label locationNumber;
    public LocationView()
    {
        ColorScheme = Globals.invertedColorScheme;
        Height = 2;
        X = Pos.Center();
        Y = 1;
        TextAlignment = TextAlignment.Centered;

        name = new Label($" Current Location: {BattleEngine.CurrentLocation.Title} ") { Y = 0 };
        locationNumber = new Label($" ({BattleEngine.CurrentLocation.index + 1}/{BattleEngine.Locations.Count}) ") { Y = 1 };
        locationNumber.X = Pos.Center() - locationNumber.Text.Length / 2;
        Width = Dim.Sized(name.Text.Length);
        Add(name,locationNumber);
        BattleEngine.OnLocationChanged += HandleLocationChanged;
    }

    private void HandleLocationChanged(object? sender, Location e)
    {
        name.Text = $" Current Location: {e.Title} ";
        locationNumber.Text = $" ({e.index + 1}/{BattleEngine.Locations.Count}) ";
        locationNumber.X = Pos.Center() - locationNumber.Text.Length / 2;
        Width = Dim.Sized(name.Text.Length);
    }
}
