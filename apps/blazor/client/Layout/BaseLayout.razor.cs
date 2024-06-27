﻿using FSH.Blazor.Infrastructure.Preferences;
using FSH.Blazor.Infrastructure.Themes;
using MudBlazor;

namespace FSH.Blazor.Client.Layout;

public partial class BaseLayout
{
    private ClientPreference? _themePreference;
    private MudTheme _theme = new FshTheme();
    private bool _themeDrawerOpen;
    private bool _rightToLeft;
    private bool _isDarkMode;

    protected override async Task OnInitializedAsync()
    {
        _themePreference = await ClientPreferences.GetPreference() as ClientPreference;
        if (_themePreference == null) _themePreference = new ClientPreference();
        SetCurrentTheme(_themePreference);

        Toast.Add("Like this project? ", Severity.Info, config =>
        {
            config.BackgroundBlurred = true;
            config.Icon = Icons.Custom.Brands.GitHub;
            config.Action = "Star us on Github!";
            config.ActionColor = Color.Info;
            config.Onclick = snackbar =>
            {
                Navigation.NavigateTo("https://github.com/fullstackhero/dotnet-starter-kit");
                return Task.CompletedTask;
            };
        });
    }

    private async Task ToggleDarkLightMode(bool isDarkMode)
    {
        if (_themePreference is not null)
        {
            _themePreference.IsDarkMode = isDarkMode;
            await ThemePreferenceChanged(_themePreference);
        }
    }

    private async Task ThemePreferenceChanged(ClientPreference themePreference)
    {
        SetCurrentTheme(themePreference);
        await ClientPreferences.SetPreference(themePreference);
    }

    private void SetCurrentTheme(ClientPreference themePreference)
    {
        _isDarkMode = themePreference.IsDarkMode;
        //_currentTheme = new FshTheme();
        //if (themePreference.IsDarkMode)
        //{
        //    _currentTheme.
        //}
        //_currentTheme.Palette.Primary = themePreference.PrimaryColor;
        //_currentTheme.Palette.Secondary = themePreference.SecondaryColor;
        //_currentTheme.LayoutProperties.DefaultBorderRadius = $"{themePreference.BorderRadius}px";
        //_currentTheme.LayoutProperties.DefaultBorderRadius = $"{themePreference.BorderRadius}px";
        //_rightToLeft = themePreference.IsRTL;
    }
}