namespace Xamarin.Forms.Daddoon
{
    public class IconTabbedPage : TabbedPage
    {
        /// <summary>
        /// Couleur par défaut du texte des items nont sélectionné sur la barre
        /// La valeur définie par défaut est la constante AccentColor
        /// </summary>
        public Color UnselectedTextColor { get; set; }

        public string GetIconAtIndex(int index)
        {
            if (Children.Count - 1 < index)
                return string.Empty;

            return Children[index].Icon != null ? Children[index].Icon.File : string.Empty;
        }

        public IconTabbedPage() : base()
        {
            UnselectedTextColor = Color.Gray;
        }

        public IconTabbedPage(Color BarBackgroundColor, Color BarTextColor, Color UnselectedTextColor) : base()
        {
            this.BarBackgroundColor = BarBackgroundColor;
            this.BarTextColor = BarTextColor;
            this.UnselectedTextColor = UnselectedTextColor;
        }
    }
}
