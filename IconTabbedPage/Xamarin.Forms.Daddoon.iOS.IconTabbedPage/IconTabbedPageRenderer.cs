using Xamarin.Forms.Daddoon;
using Xamarin.Forms.Daddoon.iOS;
using System.IO;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Daddoon.IconTabbedPage), typeof(IconTabbedPageRenderer))]
namespace Xamarin.Forms.Daddoon.iOS
{
    public class IconTabbedPageRenderer : TabbedRenderer
    {
        public static void Initialize()
        {
            #if DEBUG
                System.Diagnostics.Debug.WriteLine("IconTabbedPageRenderer Initiated!");
            #endif
        }

        public IconTabbedPageRenderer()
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            IconTabbedPage Control = (IconTabbedPage)Element;
            if (Control == null)
                return;

            //Resolving color
            UIColor selectionColor = Control.BarTextColor != null ? Control.BarTextColor.ToUIColor() : null;
            UIColor unselectionColor = Control.UnselectedTextColor != null ? Control.UnselectedTextColor.ToUIColor() : null;

            if (selectionColor != null)
                TabBar.SelectedImageTintColor = selectionColor;

            if (unselectionColor != null)
                TabBar.UnselectedItemTintColor = unselectionColor;

            TabBar.Translucent = Control.TranslucideBarOniOS;

            var items = TabBar.Items;
            for (var i = 0; i < items.Length; i++)
            {
                #region UNUSED (iOS load icon automatically actually)

                //string icon = Control.GetIconAtIndex(i);
                //if (icon == string.Empty)
                //    continue;

                ////PNG Ressource don't need explicit extension
                //if (Path.GetExtension(icon).ToLowerInvariant() == ".png")
                //    icon = Path.GetFileNameWithoutExtension(icon);

                //items[i].Image = UIImage.FromBundle(icon);
                //items[i].SelectedImage = UIImage.FromBundle(icon);

                #endregion

                if (selectionColor != null)
                {
                    UITextAttributes selectedColorAttribute = new UITextAttributes();
                    selectedColorAttribute.TextColor = selectionColor;

                    items[i].SetTitleTextAttributes(selectedColorAttribute, UIControlState.Selected);
                }

                if (unselectionColor != null)
                {
                    UITextAttributes unselectedColorAttribute = new UITextAttributes();
                    unselectedColorAttribute.TextColor = unselectionColor;

                    items[i].SetTitleTextAttributes(unselectedColorAttribute, UIControlState.Normal);
                }

                if (Control.HideText)
                {
                    //Hiding current text

                    items[i].Title = @"";
                    items[i].ImageInsets = new UIEdgeInsets(6, 0, -6, 0);
                }
            }

            base.ViewDidAppear(animated);
        }
    }
}