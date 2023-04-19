namespace idleslayer;
using Terminal.Gui;

public class Globals
{
    public static ColorScheme baseColorScheme { get; } = new ColorScheme();
    public static string GameTitle { get; } = "[ Idle Slayer ]";

    static Globals()
    {
        baseColorScheme.Normal = new Terminal.Gui.Attribute(Color.White, Color.Black);
    }
}
