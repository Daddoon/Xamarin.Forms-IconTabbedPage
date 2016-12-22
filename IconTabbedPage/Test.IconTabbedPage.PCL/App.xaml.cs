using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Daddoon;

namespace Test.IconTabbedPage.PCL
{
    public static class ExportedColors
    {

        public static readonly Color AccentColor = Color.FromRgba(208, 52, 92, 255);

        public static readonly Color DefaultTextColor = Color.FromRgba(85, 85, 85, 255);

        public static readonly Color InverseTextColor = Color.FromRgba(255, 255, 255, 255);

        public static readonly Color UnselectedTextTabBar = Color.FromRgba(91, 9, 31, 255);

    }

    public class MockTabbedPage : Xamarin.Forms.Daddoon.IconTabbedPage
    {
        public MockTabbedPage()
        {
            BarBackgroundColor = ExportedColors.AccentColor;
            BarTextColor = ExportedColors.InverseTextColor;
            UnselectedTextColor = ExportedColors.UnselectedTextTabBar;

            TranslucideBarOniOS = false;
            HideText = true;

            Title = "MyTitle TabbedPage";

            Children.Add(new ContentPage { Title = "First", Icon = "user_icon.png" });
            Children.Add(new ContentPage { Title = "Second", Icon = "user_icon.png" });
        }
    }

    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Page page = new NavigationPage()
            {
                Title = "Title Navigation"
            };

            MockTabbedPage tabbedPage = new MockTabbedPage();

            page.Navigation.PushAsync(tabbedPage);

            MainPage = page;
        }
    }
}
