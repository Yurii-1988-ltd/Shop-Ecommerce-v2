using MudBlazor;

namespace Ecommerce.Admin.Theme;

public static class AppTheme
{
    public static readonly MudTheme Theme = new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#FF9900",      // Amazon Orange
            Secondary = "#374151",
            Background = "#F3F4F6",
            Surface = "#FFFFFF",

            AppbarBackground = "#FFFFFF",
            AppbarText = "#111827",

            DrawerBackground = "#5C5C5C",
            DrawerText = "#FFFFFF",

            TextPrimary = "#111827",
            TextSecondary = "#6B7280",

            Divider = "#D1D5DB"
        },

        LayoutProperties = new LayoutProperties
        {
            DrawerWidthLeft = "270px"
        },

        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = new[]
                {
                    "Inter",
                    "Segoe UI",
                    "sans-serif"
                }
            },

            H4 = new H4Typography
            {
                FontWeight = "700"
            }
        }
    };
}