using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Content.Items;
public class Gantry : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Gantry>());
        Item.maxStack = 1;
    }
}