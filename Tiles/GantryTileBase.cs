using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace MarvelTerrariaUniverse.Tiles
{
    public class GantryTileBase : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileMergeDirt[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.Width = 5;
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.Origin = new Point16(2, 0);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.CoordinateHeights = new[] { 16 };
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(0, 0, 127));
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(Main.LocalPlayer.GetSource_TileInteraction(i, j), i * 16, j * 16, 32, 16, ModContent.ItemType<GantryTileItem>());
        }

        public override bool Slope(int i, int j) => false;

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);

            if (Framing.GetTileSafely(i - 2, j).BlockType != Terraria.ID.BlockType.Solid && Framing.GetTileSafely(i + 2, j).BlockType != Terraria.ID.BlockType.Solid)
            {
                spriteBatch.Draw(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/Tiles/GantryTileTexture").Value, new Vector2(i * 16 - (int)Main.screenPosition.X - 48, j * 16 - (int)Main.screenPosition.Y - 64) + zero, Color.White);
            }

            return false;
        }

        public override void FloorVisuals(Player player)
        {
            player.GetModPlayer<MarvelTerrariaUniverseModPlayer>().GantryUIActive = true;
        }
    }
}