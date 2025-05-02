using MudBlazor;
using Microsoft.JSInterop;
using MudBlazor.Utilities;

namespace Frontend.Themes
{
    public static class SGSCThemes
    {
        public static MudTheme DefaultTheme => new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                Primary = Colors.LightBlue.Default, // #03A9F4
                Secondary = Colors.LightBlue.Accent4,
                AppbarBackground = "#FFFFFF"
            },
            PaletteDark = new PaletteDark()
            {
                Primary = Colors.Blue.Lighten1,
                AppbarBackground = "#1E1E1E"
            },
            LayoutProperties = new LayoutProperties()
            {
                DefaultBorderRadius = "6px"
            }
        };

        public static async Task ApplyThemeToCssVarsAsync(IJSRuntime js)
        {
            await js.InvokeVoidAsync("applyCssVarsFromTheme", new
            {
                primary = DefaultTheme.PaletteLight.Primary.ToString(MudColorOutputFormats.Hex),
                secondary = DefaultTheme.PaletteLight.Secondary.ToString(MudColorOutputFormats.Hex),
                radius = DefaultTheme.LayoutProperties.DefaultBorderRadius,
            });
        }


    }
}