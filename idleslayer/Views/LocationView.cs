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

        name = new Label($" Current Location: {App.GameSystem.LocationSystem.CurrentLocation.Title} ") { Y = 0 };
        locationNumber = new Label($" ({App.GameSystem.LocationSystem.CurrentLocation.index + 1}/{App.GameSystem.LocationSystem.Locations.Count}) ") { Y = 1 };
        locationNumber.X = Pos.Center() - locationNumber.Text.Length / 2;
        Width = Dim.Sized(name.Text.Length);
        Add(name,locationNumber);
        App.GameSystem.LocationSystem.OnLocationChanged += HandleLocationChanged;
    }

    private void HandleLocationChanged(Location  loc)
    {
        name.Text = $" Current Location: {loc.Title} ";
        locationNumber.Text = $" ({loc.index + 1}/{App.GameSystem.LocationSystem.Locations.Count}) ";
        locationNumber.X = Pos.Center() - locationNumber.Text.Length / 2;
        Width = Dim.Sized(name.Text.Length);
    }
}
