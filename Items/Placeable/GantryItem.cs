using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaXMCU.Tiles;

namespace TerrariaXMCU.Items.Placeable
{
	public class GantryItem : ModItem
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
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<GantryTile>();
		}
	}
}