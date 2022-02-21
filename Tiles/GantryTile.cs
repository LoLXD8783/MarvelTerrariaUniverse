using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TerrariaXMCU.Items.Placeable;

namespace TerrariaXMCU.Tiles
{
    public class GantryTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileMergeDirt[Type] = false;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.Origin = new Point16(1, 0);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
            TileObjectData.addTile(Type);
            Main.placementPreview = true;
            AddMapEntry(new Color(0, 0, 127));
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<GantryItem>());
        }

        public override bool Slope(int i, int j)
        {
            return false;
        }

        public override bool CanPlace(int i, int j)
        {
            return Framing.GetTileSafely(i + 2, j - 1).IsActive && Framing.GetTileSafely(i + 3, j - 1).IsActive && Framing.GetTileSafely(i - 2, j - 1).IsActive && Framing.GetTileSafely(i - 3, j - 1).IsActive;
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
        {
            if (drawData.tileFrameX == 0 && drawData.tileFrameY == 0)
            {
                Texture2D texture = ModContent.Request<Texture2D>("TerrariaXMCU/Assets/GantrySystem").Value;
                Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);

                spriteBatch.Draw(texture, new Vector2(i * 16 - (int)Main.screenPosition.X + 24, j * 16 - (int)Main.screenPosition.Y - 21) + zero, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, 0f, texture.Size() / 2f, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}