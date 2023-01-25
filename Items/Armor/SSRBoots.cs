using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class SSRBoots : ModItem
	{
		public override void SetStaticDefaults() 
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("SSR Boots");
			Tooltip.SetDefault("\n7% increased ranged critical strike chance"
				+ "\n12% increased movement speed");
		}

		public override void SetDefaults() 
		{
			Item.width = 22;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = ItemRarityID.Purple;
			Item.defense = 24;
		}

		public override void UpdateEquip(Player player) 
		{
			player.moveSpeed += 0.12f;
			player.noFallDmg = false;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
			.AddIngredient(ItemID.GoldBar, 1)
			.AddIngredient(ItemID.TitaniumBar, 19)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}