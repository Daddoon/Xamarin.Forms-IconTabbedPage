using System;
using System.Reflection;

namespace Xamarin.Forms.Daddoon
{
    public class IconTabbedPage : TabbedPage
    {
        /// <summary>
        /// Default color of unselected text/images on the bar
        /// </summary>
        public Color UnselectedTextColor { get; set; }

        /// <summary>
        /// Hide Tab text and only show the image (iOS Only)
        /// </summary>
        public bool HideText { get; set; }

        /// <summary>
        /// Translucide Bar On iOS. Default is true
        /// </summary>
        public bool TranslucideBarOniOS { get; set; }

        ///// <summary>
        ///// Height of the TabBar. Default is -1, for platform default size
        ///// </summary>
        //public int BarHeight { get; set; }

        public string GetIconAtIndex(int index)
        {
            if (Children.Count - 1 < index)
                return string.Empty;

            return Children[index].Icon != null ? Children[index].Icon.File : string.Empty;
        }


        private void CommonInit()
        {
            HideText = false;
            TranslucideBarOniOS = true;
        }

        public IconTabbedPage() : base()
        {
            CommonInit();
        }

        public IconTabbedPage(Color BarBackgroundColor, Color BarTextColor, Color UnselectedTextColor) : base()
        {
            CommonInit();

            this.BarBackgroundColor = BarBackgroundColor;
            this.BarTextColor = BarTextColor;
            this.UnselectedTextColor = UnselectedTextColor;
        }
    }
}
