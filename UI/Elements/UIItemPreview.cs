using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace MarvelTerrariaUniverse.UI.Elements
{
    internal class UIItemPreview : UIElement
    {
        internal Item Item;
        private readonly int _context;
        private readonly float _scale;

        public UIItemPreview(int itemType, int context = ItemSlot.Context.BankItem, float scale = 1f)
        {
            _context = context;
            _scale = scale;
            Item = new() { type = itemType };

            Width.Set(Terraria.GameContent.TextureAssets.InventoryBack9.Width() * scale, 0f);
            Height.Set(Terraria.GameContent.TextureAssets.InventoryBack9.Height() * scale, 0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            float oldScale = Main.inventoryScale;
            Main.inventoryScale = _scale;
            Rectangle rectangle = GetDimensions().ToRectangle();

            if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface) Main.LocalPlayer.mouseInterface = true;

            ItemSlot.Draw(spriteBatch, ref Item, _context, rectangle.TopLeft());
            Main.inventoryScale = oldScale;
        }
    }
}
