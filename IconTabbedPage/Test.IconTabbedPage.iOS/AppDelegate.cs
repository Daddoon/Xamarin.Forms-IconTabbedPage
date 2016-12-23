using Foundation;
using Test.IconTabbedPage.PCL;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace Test.IconTabbedPage.iOS
{
    public static class Appearance
    {
        public static UIColor AccentColor = ExportedColors.AccentColor.ToUIColor();
        public static UIColor TextColor = ExportedColors.InverseTextColor.ToUIColor();

        public static void Configure()
        {
            UINavigationBar.Appearance.BarTintColor = AccentColor;
            UINavigationBar.Appearance.TintColor = TextColor;
            UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes
            {
                ForegroundColor = TextColor,
            };

            UIProgressView.Appearance.ProgressTintColor = AccentColor;

            UISlider.Appearance.MinimumTrackTintColor = AccentColor;
            UISlider.Appearance.MaximumTrackTintColor = AccentColor;
            UISlider.Appearance.ThumbTintColor = AccentColor;

            UISwitch.Appearance.OnTintColor = AccentColor;

            UITableViewHeaderFooterView.Appearance.TintColor = AccentColor;

            UITableView.Appearance.SectionIndexBackgroundColor = AccentColor;
            UITableView.Appearance.SeparatorColor = AccentColor;

            UITextField.Appearance.TintColor = AccentColor;

            UIButton.Appearance.TintColor = AccentColor;
            UIButton.Appearance.SetTitleColor(AccentColor, UIControlState.Normal);

            UITabBar.Appearance.BarTintColor = AccentColor;
            UITabBar.Appearance.TintColor = TextColor;
        }
    }

    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            Appearance.Configure();

            Xamarin.Forms.Daddoon.iOS.IconTabbedPageRenderer.Initialize();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}


