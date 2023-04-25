namespace idleslayer;

using System.Diagnostics;
using Terminal.Gui;

class BattleView : View
{


    public BattleView()
    {
        Width = Dim.Fill();
        Height = Dim.Fill() - 1;
        X = Pos.Center();
        Y = Pos.Center();
        var enemyView = new EnemyView();
        Add(enemyView);
    }







}
