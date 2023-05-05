namespace idleslayer;

using System;
using System.Diagnostics;
using Terminal.Gui;

public class BattleControls : StatusBar
{

    StatusItem quitButton = new StatusItem(Key.CtrlMask | Key.Q, "~CTRL-Q~ - Quit", () =>
    {
        Debug.WriteLine("Pressed CTRL-Q");
        App.SceneManager.MenuState = MenuState.Exit;
    });


    StatusItem skillShopButton = new StatusItem(Key.s, "~s~ - Skills Shop", () =>
    {
        Debug.WriteLine("Pressed S");
        App.SceneManager.MenuState = MenuState.Shop;
        App.GameSystem.PauseGame();
    });
    StatusItem pauseButton = new StatusItem(Key.p, App.GameSystem.isPaused ? "~p~  - Unpause" : "~p~ - Pause", () =>
    {
        App.GameSystem.TogglePause();
    });

    StatusItem nextLocationButton = new StatusItem(Key.n, "~n~ - Next Location", () =>
    {
        App.GameSystem.LocationSystem.ChangeLocation(true);
    });

    StatusItem previousLocationButton = new StatusItem(Key.b, "~b~ - Previous Location", () =>
{
    App.GameSystem.LocationSystem.ChangeLocation(true);
});

    LocationSystem _locationSystem;

    public BattleControls()
    {
        _locationSystem = App.GameSystem.LocationSystem;
        AddItemAt(0, quitButton);
        AddItemAt(1, pauseButton);
        AddItemAt(2, skillShopButton);
        AddItemAt(3, nextLocationButton);
        if (_locationSystem.CurrentLocation.index > 0)
        {
            AddItemAt(4, previousLocationButton);
        }
        _locationSystem.OnLocationChanged += HandleLocationChanged;
        App.GameSystem.OnGamePaused += OnGamePaused;
        App.GameSystem.OnGameResumed += OnGameResumed;
    }

    void OnGameResumed()
    {
        pauseButton.Title = "~p~ - Pause";
    }

    void OnGamePaused()
    {
        pauseButton.Title = "~p~ - Unpause";
    }

    void HandleLocationChanged(Location location)
    {
        var previousButtonIndex = Items.ToList().FindIndex(v => (v.Title.ToString() ?? "").Contains("Previous"));
        var nextButtonIndex = Items.ToList().FindIndex(v => (v.Title.ToString() ?? "").Contains("Next"));
        if (location.index == 0 && previousButtonIndex != -1)
        {
            RemoveItem(previousButtonIndex);
        }
        else if (previousButtonIndex == -1)
        {
            AddItemAt(Items.Count(), new StatusItem(Key.b, "~b~ - Previous Location", () =>
            {
                _locationSystem.ChangeLocation(false);
            }));
        }
        if (location.IsLast && nextButtonIndex != -1)
        {
            RemoveItem(nextButtonIndex);
        }
        else if (nextButtonIndex == -1)
        {
            AddItemAt(Items.Count(), new StatusItem(Key.n, "~n~ - Next Location", () =>
             {
                 _locationSystem.ChangeLocation(true);
             }));
        }
    }
}
