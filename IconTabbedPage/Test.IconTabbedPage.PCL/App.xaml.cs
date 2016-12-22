using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Daddoon;

namespace Test.IconTabbedPage.PCL
{
    public class MockTabbedPage : Xamarin.Forms.Daddoon.IconTabbedPage
    {
        public MockTabbedPage() : base()
        {
            BarBackgroundColor = Color.Red;
            BarTextColor = Color.White;
            UnselectedTextColor = Color.Gray;
            //BarHeight = 500;

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
