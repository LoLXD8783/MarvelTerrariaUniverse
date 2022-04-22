using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace MarvelTerrariaUniverse.Tiles
{
    public class GantryTileItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gantry");
        }

        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.width = 58;
            Item.height = 46;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<GantryTile>();
        }
    }

    public class GantryTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileMergeDirt[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.Width = 7;
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.Origin = new Point16(3, 4);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16, 16 };
            TileObjectData.addTile(Type);
            Main.placementPreview = true;
            AddMapEntry(new Color(0, 0, 127));
        }

        public override void PlaceInWorld(int i, int j, Item item)
        {
            WorldGen.KillTile(i, j);
            WorldGen.PlaceTile(i, j, ModContent.TileType<GantryTileBase>(), true, true);
            WorldGen.SlopeTile(i - 2, j, 2);
            WorldGen.SlopeTile(i + 2, j, 1);
        }
    }
}