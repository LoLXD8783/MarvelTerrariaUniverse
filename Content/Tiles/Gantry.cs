using MarvelTerrariaUniverse.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace MarvelTerrariaUniverse.Content.Tiles;
public class Gantry : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileFrameImportant[Type] = true;
        TileID.Sets.DisableSmartCursor[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.Width = 4;
        TileObjectData.newTile.Height = 1;
        TileObjectData.newTile.Origin = new Point16(2, 0);
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
        TileObjectData.newTile.CoordinateHeights = new[] { 18 };
        TileObjectData.addTile(Type);
        ModTranslation name = CreateMapEntryName();
        name.SetDefault("Gantry");
        AddMapEntry(new Color(53, 53, 54), name);
    }

    public override bool Slope(int i, int j) => false;

    public override void KillMultiTile(int x, int y, int frameX, int frameY)
    {
        Item.NewItem(new EntitySource_TileBreak(x, y), x * 16, y * 16, 48, 16, ModContent.ItemType<Items.Gantry>());
    }

    public override void FloorVisuals(Player player)
    {
        player.GetModPlayer<IronManPlayer>().ShowGantryUI = true;
    }
}