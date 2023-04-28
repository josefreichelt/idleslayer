namespace idleslayer;
using Terminal.Gui;
public static class Globals
{
    public static ColorScheme baseColorScheme { get; } = new ColorScheme();
    public static ColorScheme invertedColorScheme { get; } = new ColorScheme();
    public static string GameTitle { get; } = "[ Idle Slayer ]";


    static Globals()
    {
        baseColorScheme.Normal = new Terminal.Gui.Attribute(Color.White, Color.Black);
        baseColorScheme.Focus = new Terminal.Gui.Attribute(Color.Blue, Color.Black);
        invertedColorScheme.Normal = new Terminal.Gui.Attribute(Color.Black, Color.White);
        invertedColorScheme.Focus = new Terminal.Gui.Attribute(Color.Black, Color.Blue);
    }

   


}
