using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace MarvelTerrariaUniverse.UI.Elements
{
    public class UIHoverPanel : UIPanel
    {
        public string HoverText;

        public UIHoverPanel(string hoverText) : base()
        {
            HoverText = hoverText;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            if (IsMouseHovering)
            {
                Main.hoverItemName = HoverText;
            }
        }
    }
}
