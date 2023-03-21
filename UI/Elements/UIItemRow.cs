using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace MarvelTerrariaUniverse.UI.Elements
{
    public class UIItemRow : UIHoverPanel
    {
        List<int> ItemList;
        List<Item> DisplayedItems;
        List<UIItemIcon> ItemIcons;

        Item DisplayedItem;

        public UIItemRow(string hoverText, List<int> itemList) : base(hoverText)
        {
            ItemList = itemList;
            DisplayedItems = new();
            ItemIcons = new();

            Height = StyleDimension.FromPixels(56f);

            ItemList.ForEach(item =>
            {
                DisplayedItem = new();
                DisplayedItem.SetDefaults(item);

                DisplayedItems.Add(DisplayedItem);

                UIItemIcon ItemIcon = new(DisplayedItems[ItemList.IndexOf(item)], false)
                {
                    HAlign = (1f / ItemList.Count) * ItemList.IndexOf(item) + (1f / ItemList.Count / 2f),
                    VAlign = 0.5f
                };

                ItemIcons.Add(ItemIcon);
                Append(ItemIcon);
            });
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            ItemIcons.ForEach(icon =>
            {
                if (icon.IsMouseHovering) Main.HoverItem = DisplayedItems[ItemIcons.IndexOf(icon)];
            });
        }
    }
}
