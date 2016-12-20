# Xamarin.Forms-IconTabbedPage
A Xamarin CustomRenderer that render Icon for TabbedPage for iOS & Android, using Icon property on child pages.

Use IconTabbedPage class instead of TabbedPage class, and add the name of your static image resource on the "Icon" property of your childs pages, eg. "user_icon.png", and it will be loaded and show instead of the tab text.

The behavior of this class is at constructor level, so i strongly advise you to configure your class and childs pages using your own inherited class of IconTabbedPage, or by initializing your content from a XAML context.

Default colors may not be accurate to your needs. In addition of the standards properties used on TabbedPage like "BarBackgroundColor" and "BarTextColor", IconTabbedPage expose a "UnselectedTextColor" in order to override the filled color of your Icon when a Tab is unselected.

## Nuget Installation

Install [Xamarin.Forms-IconTabbedPage](https://www.nuget.org/packages/Xamarin.Forms-IconTabbedPage) from nuget.
Xamarin.Forms (>= 2.3.0.49) is required.

**Important:** You will need to add the nuget package to **both** your *PCL project* and your *platform-dependent project*.

# Example

```csharp
 public class DashboardTabbedPage : IconTabbedPage
 {
    public DashboardTabbedPage() : base()
    {
        BarBackgroundColor = ExportedColors.AccentColor; //Your own custom color
        BarTextColor = ExportedColors.InverseTextColor; //Your own custom color
        UnselectedTextColor = ExportedColors.UnselectedTextTabBar; //Your own custom color

        Children.Add(new ContentPage { BackgroundColor = Color.Red, Title = "Page 1", Icon = "user_icon.png" });
        Children.Add(new ContentPage { BackgroundColor = Color.Blue, Title = "Page 2", Icon = "edit_icon.png" });
        Children.Add(new ContentPage { BackgroundColor = Color.Green, Title = "Page 3", Icon = "success_icon.png" });
    }
}
```

This plugin is highly inspired from solutions founds on the Xamarin forum, thanks to all contributors.
