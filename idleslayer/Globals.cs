namespace idleslayer;
using Terminal.Gui;
public static class Globals
{
    public static ColorScheme baseColorScheme { get; } = new ColorScheme();
    public static ColorScheme invertedColorScheme { get; } = new ColorScheme();
    public static string GameTitle { get; } = "[ Idle Slayer ]";

    public static List<Skill> SkillList = new List<Skill>();
    public static List<Location> Locations = new List<Location>();

    static Globals()
    {
        baseColorScheme.Normal = new Terminal.Gui.Attribute(Color.White, Color.Black);
        baseColorScheme.Focus = new Terminal.Gui.Attribute(Color.Blue, Color.Black);
        invertedColorScheme.Normal = new Terminal.Gui.Attribute(Color.Black, Color.White);
        invertedColorScheme.Focus = new Terminal.Gui.Attribute(Color.Black, Color.Blue);
        GenerateSkills();
        GenerateLocations();

    }

    static void GenerateLocations()
    {
        Locations.Add(new Location()
        {
            Title = "Training Camp",
            Enemies = new List<Enemy>() {
                new Enemy("Training Dummy", 10, 1),
                new Enemy("Hay Stack", 20, 1),
            }
        });
        Locations.Add(new Location()
        {
            Title = "Camp Outskirts",
            Enemies = new List<Enemy>() {
                new Enemy("Chicken", 20, 2),
                new Enemy("Killer Rabbit", 50, 3),
            }
        });
        foreach (var location in Locations)
        {
            location.index = Locations.IndexOf(location);
        }
    }

    static void GenerateSkills()
    {
        SkillList.Add(new Skill("Slash", 1, 1));
        SkillList.Add(new Skill("Stab", 2, 2));
        SkillList.Add(new Skill("Punch", 5, 10));
    }
}
