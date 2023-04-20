namespace idleslayer;
using Terminal.Gui;
public static class Globals
{
    public static ColorScheme baseColorScheme { get; } = new ColorScheme();
    public static string GameTitle { get; } = "[ Idle Slayer ]";

    public static List<Skill> SkillList = new List<Skill>();

    static Globals()
    {
        baseColorScheme.Normal = new Terminal.Gui.Attribute(Color.White, Color.Black);
        baseColorScheme.Focus = new Terminal.Gui.Attribute(Color.Blue, Color.Black);

        SkillList.Add(new Skill("Slash", 1, 1));
        SkillList.Add(new Skill("Stab", 2, 2));
        SkillList.Add(new Skill("Punch", 5, 10));
    }
}
