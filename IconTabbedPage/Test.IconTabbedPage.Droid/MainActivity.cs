using Android.App;
using Android.OS;
using Android.Graphics.Drawables;
using Test.IconTabbedPage.PCL;
using Xamarin.Forms.Platform.Android;

namespace Test.IconTabbedPage.Droid
{
    [Activity(Label = "Test.IconTabbedPage.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());

            #pragma warning disable 618
            // Hiding ActionBar Icon on Android versions using Material Design
            if ((int)Android.OS.Build.VERSION.SdkInt >= 21)
            {
                ActionBar.SetIcon(
                    new ColorDrawable(
                        Resources.GetColor(Android.Resource.Color.Transparent)
                    )
                );
            }
            #pragma warning restore 618
        }
    }
}

