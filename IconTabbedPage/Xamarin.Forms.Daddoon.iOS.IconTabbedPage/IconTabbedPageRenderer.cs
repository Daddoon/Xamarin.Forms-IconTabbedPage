using Xamarin.Forms.Daddoon;
using Xamarin.Forms.Daddoon.iOS;
using System.IO;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(IconTabbedPage), typeof(IosIconTabbedPageRenderer))]
namespace Xamarin.Forms.Daddoon.iOS
{
    public class IosIconTabbedPageRenderer : TabbedRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            IconTabbedPage Control = (IconTabbedPage)Element;
            if (Control == null)
                return;

            //Récupération de la couleur défini sur le control
            UIColor selectionColor = Control.BarTextColor != null ? Control.BarTextColor.ToUIColor() : null;
            UIColor unselectionColor = Control.UnselectedTextColor != null ? Control.UnselectedTextColor.ToUIColor() : null;

            if (selectionColor != null)
                TabBar.SelectedImageTintColor = selectionColor;

            if (unselectionColor != null)
                TabBar.UnselectedItemTintColor = unselectionColor;

            var items = TabBar.Items;
            for (var i = 0; i < items.Length; i++)
            {
                string icon = Control.GetIconAtIndex(i);
                if (icon == string.Empty)
                    continue;

                //Les ressources PNG n'ont pas besoin d'extension explicite
                if (Path.GetExtension(icon).ToLowerInvariant() == ".png")
                    icon = Path.GetFileNameWithoutExtension(icon);

                items[i].Image = UIImage.FromBundle(icon);
                items[i].SelectedImage = UIImage.FromBundle(icon);

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
            }
        }
    }
}