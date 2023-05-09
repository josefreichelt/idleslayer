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

    LocationSystem locationSystem;
    public BattleControls()
    {
        locationSystem = App.GameSystem.LocationSystem;
        AddItemAt(0, quitButton);
        AddItemAt(1, pauseButton);
        AddItemAt(2, skillShopButton);
        AddItemAt(3, nextLocationButton);
        if (locationSystem.CurrentLocation.index > 0)
        {
            AddItemAt(4, previousLocationButton);
        }
        App.SceneManager.OnMenuStateChanged += HandleMenuStateChanged;
        locationSystem.OnLocationChanged += HandleLocationChanged;
        App.GameSystem.OnGamePaused += OnGamePaused;
        App.GameSystem.OnGameResumed += OnGameResumed;
    }

    private void HandleMenuStateChanged(MenuState state)
    {
        if (state == MenuState.Battle)
        {
            var previousButtonIndex = Items.ToList().FindIndex(v => (v.Title.ToString() ?? "").Contains("Previous"));
            var nextButtonIndex = Items.ToList().FindIndex(v => (v.Title.ToString() ?? "").Contains("Next"));
            if (nextButtonIndex != -1)
            {
                RemoveItem(nextButtonIndex);
            }
            if (previousButtonIndex != -1)
            {
                RemoveItem(previousButtonIndex);
            }

            HandleLocationChanged(locationSystem.CurrentLocation);
        }
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

        // First location, remove previous button
        if (location.index == 0 && previousButtonIndex != -1)
        {
            RemoveItem(previousButtonIndex);
        }
        if (previousButtonIndex == -1)
        {
            AddItemAt(Items.Count(), new StatusItem(Key.b, "~b~ - Previous Location", () =>
            {
                locationSystem.ChangeLocation(false);
            }));
        }
        // Last location, remove next button
        if (location.IsLast && nextButtonIndex != -1)
        {
            RemoveItem(nextButtonIndex);
        }
        if (nextButtonIndex == -1)
        {
            AddItemAt(Items.Count(), new StatusItem(Key.n, "~n~ - Next Location", () =>
             {
                 locationSystem.ChangeLocation(true);
             }));
        }
    }
    ~BattleControls()
    {
        App.SceneManager.OnMenuStateChanged -= HandleMenuStateChanged;
        locationSystem.OnLocationChanged -= HandleLocationChanged;
        App.GameSystem.OnGamePaused -= OnGamePaused;
        App.GameSystem.OnGameResumed -= OnGameResumed;
    }
}
