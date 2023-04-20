namespace idleslayer;

using NStack;
using Terminal.Gui;
public class CenteredWindow : Window
{
    public CenteredWindow(string title) : base(title)
    {
        X = Pos.Center();
        Y = Pos.Center();
        Width = Dim.Fill();
        Height = Dim.Fill();
    }
    public override void Redraw(Rect bounds)
    {
        base.Redraw(bounds);
        this.Move(1, 0, false);

        var title = this.Title.ToString();
        if (title == null)
        {
            title = "";
        };
        var titleWidth = title.Sum(c => Rune.ColumnWidth(c));

        if (titleWidth > bounds.Width)
        {
            title = title.Substring(0, bounds.Width);
        }
        else
        {
            if (titleWidth + 2 < bounds.Width)
            {
                title = this.Title.ToString();
            }
            titleWidth += 2;
        }

        var padLeft = ((bounds.Width - titleWidth) / 2) - 1;

        padLeft = Math.Min(bounds.Width, padLeft);
        padLeft = Math.Max(0, padLeft);

        var padRight = bounds.Width - (padLeft + titleWidth + 2);
        padRight = Math.Min(bounds.Width, padRight);
        padRight = Math.Max(0, padRight);

        Driver.SetAttribute(
                  new Attribute(this.ColorScheme.Normal.Foreground, this.ColorScheme.Normal.Background));

        Driver.AddStr(ustring.Make(Enumerable.Repeat(Driver.HLine, padLeft)));

        Driver.SetAttribute(
                  new Attribute(this.ColorScheme.Normal.Foreground, this.ColorScheme.Normal.Background));
        Driver.AddStr(title);

        Driver.SetAttribute(
                  new Attribute(this.ColorScheme.Normal.Foreground, this.ColorScheme.Normal.Background));

        Driver.AddStr(ustring.Make(Enumerable.Repeat(Driver.HLine, padRight)));
    }
}
