using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Widget;
using Xamarin.Forms.Daddoon;
using Xamarin.Forms.Daddoon.Droid;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Runtime;
using Java.Lang;
using System.Collections.Generic;
using Java.Lang.Reflect;

[assembly: ExportRenderer(typeof(IconTabbedPage), typeof(AndroidIconTabbedPageRenderer))]
namespace Xamarin.Forms.Daddoon.Droid
{
    internal static class AndroidColorHelper
    {
        private static string DoubleToHexa(double value)
        {
            //Keep color in a valid range
            if (value < 0.0f) //0% / 0 Octet MIN
                value = 0.0f;
            else if (value > 1.0f) //100% / 255 Octet MAX
                value = 1.0f;

            int octetValue = (int)System.Math.Round(value * 255.0f, 0);

            return octetValue.ToString("X2");
        }

        /// <summary>
        /// Return Color in Hexa string format, without the '#' caracter
        /// </summary>
        /// <param name="color"></param>
        /// <param name="withAlpha"></param>
        /// <returns></returns>
        public static string ToHex(this Color color, bool withAlpha = false)
        {
            return (withAlpha ? DoubleToHexa(color.A) : string.Empty) + DoubleToHexa(color.R) + DoubleToHexa(color.G) + DoubleToHexa(color.B);
        }
    }

    public class AndroidIconTabbedPageRenderer : TabbedRenderer
    {
        private Activity activity;
        private TabbedPage _tabbedPage;
        private string COLOR = "#FFFFFF";
        private IconTabbedPage Control;
        private global::Android.Graphics.Color selectionColor;
        private global::Android.Graphics.Color unselectionColor;
        private int PlatformDefaultHeight = -1;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            Control = (IconTabbedPage)Element;

            if (Control == null)
                return;

            COLOR = "#" + AndroidColorHelper.ToHex(Control.BarBackgroundColor);
            //Récupération de la couleur défini sur le control
            selectionColor = Control.BarTextColor != null ? Control.BarTextColor.ToAndroid() : Xamarin.Forms.Color.Default.ToAndroid();

            unselectionColor = Control.UnselectedTextColor != null ? Control.UnselectedTextColor.ToAndroid() : Xamarin.Forms.Color.Default.ToAndroid();

            base.OnElementChanged(e);

            activity = this.Context as Activity;
            _tabbedPage = e.NewElement as TabbedPage;

            //HackPolicy();
        }

        protected override void DispatchDraw(Canvas canvas)
        {
            ActionBar actionBar = activity.ActionBar;

            if (actionBar.TabCount > 0)
            {
                ColorDrawable colorDrawable = new ColorDrawable(global::Android.Graphics.Color.ParseColor(COLOR));
                actionBar.SetStackedBackgroundDrawable(colorDrawable);

                #region NOT WORKING JUST TEST
                //try
                //{
                //    if (PlatformDefaultHeight == -1)
                //    {
                //        //Initialization of the default height for this platform in case of restoring
                //        PlatformDefaultHeight = actionBar.Height;
                //    }

                //    int height = Control.BarHeight;

                //    Class cls = actionBar.Class;

                //    Class integer = Java.Lang.Integer.Type;
                //    Java.Lang.Integer intValue = new Integer(height);

                //    Method setContentHeightMethod = cls.GetMethod("setContentHeight", new List<Class>() { integer }.ToArray());

                //    setContentHeightMethod.Invoke(actionBar, intValue);

                //    //Set the current bar size
                //    if (actionBar.CustomView != null && actionBar.CustomView.Height != Control.BarHeight)
                //    {
                //        actionBar.CustomView.SetMinimumHeight((int)Control.BarHeight);
                //        actionBar.CustomView.LayoutParameters.Height = (int)Control.BarHeight;
                //    }
                //}
                //catch (System.Exception)
                //{
                //}

                #endregion

                ActionBarTabsSetup(actionBar);

            }

            base.DispatchDraw(canvas);
        }

        private void ActionBarTabsSetup(ActionBar actionBar)
        {
            try
            {
                for (int i = 0; i < actionBar.TabCount; i++)
                {
                    global::Android.App.ActionBar.Tab dashboardTab = actionBar.GetTabAt(i);
                    if (TabIsEmpty(dashboardTab))
                    {
                        string file = string.Empty;

                        if (_tabbedPage.Children[i].Icon != null)
                            file = System.IO.Path.GetFileNameWithoutExtension(_tabbedPage.Children[i].Icon.File);

                        int id = Resources.GetIdentifier(file, "drawable", Context.PackageName);

                        global::Android.App.ActionBar.Tab selectedTab = actionBar.SelectedTab;

                        bool isSelected = false;
                        if (selectedTab == dashboardTab)
                            isSelected = true;

                        TabSetup(dashboardTab, id, isSelected);
                    }
                }
            }
            catch (System.Exception)
            {

            }

        }

        private bool TabIsEmpty(ActionBar.Tab tab)
        {
            if (tab != null)
                if (tab.CustomView == null)
                    return true;
            return false;
        }

        private void TabSetup(ActionBar.Tab tab, int resourceID, bool isSelected)
        {
            ImageView iv = new ImageView(activity);
            iv.SetImageResource(resourceID);
            iv.SetPadding(0, 10, 0, 0);

            try
            {
                tab.TabSelected += Tab_TabSelected;
            }
            catch (System.Exception)
            {
            }

            tab.TabUnselected += Tab_TabUnselected;

            if (isSelected)
                iv.SetColorFilter(selectionColor);
            else
                iv.SetColorFilter(unselectionColor);

            tab.SetCustomView(iv);
        }

        private void Tab_TabUnselected(object sender, ActionBar.TabEventArgs e)
        {
            global::Android.App.ActionBar.Tab myTab = (global::Android.App.ActionBar.Tab)sender;

            if (myTab == null)
                return;

            ImageView iv = (ImageView)myTab.CustomView;

            if (iv != null)
                iv.SetColorFilter(unselectionColor);
        }

        private void Tab_TabSelected(object sender, ActionBar.TabEventArgs e)
        {
            global::Android.App.ActionBar.Tab myTab = (global::Android.App.ActionBar.Tab)sender;

            if (myTab == null)
                return;

            ImageView iv = (ImageView)myTab.CustomView;

            if (iv != null)
                iv.SetColorFilter(selectionColor);
        }

        #region Bar Height Overriding

        /// <summary>
        /// NOT WORKING JUST FOR TEST PURPOSE
        /// </summary>
        private void HackPolicy()
        {
            global::Android.Views.View container = FindScrollingTabContainer();
            if (container == null) return;

            try
            {
                //TODO: Uncomment when needed
                //int height = Control.BarHeight;
                int height = 0;

                Class cls = container.Class;

                Class integer = Java.Lang.Integer.Type;
                Java.Lang.Integer intValue = new Integer(height);
                
                Method setContentHeightMethod = cls.GetMethod("setContentHeight", new List<Class>() { integer }.ToArray());

                setContentHeightMethod.Invoke(container, intValue);
            }
            catch (System.Exception e)
            {
            }
        }

        /// <summary>
        /// NOT WORKING JUST FOR TEST PURPOSE
        /// </summary>
        private global::Android.Views.View FindScrollingTabContainer()
        {
            global::Android.Views.View decor = activity.Window.DecorView;

            int containerId = activity.Resources.GetIdentifier("action_bar_container", "id", "android");

            // check if appcompat library is used
            if (containerId == 0)
                return null;

            FrameLayout container = (FrameLayout)decor.FindViewById(containerId);

            for (int i = 0; i < container.ChildCount; i++)
            {
                global::Android.Views.View scrolling = container.GetChildAt(i);

                if (scrolling.Class.SimpleName == "ActionBarContextView")
                    return scrolling;
            }

            return null;
        }

        #endregion
    }
}