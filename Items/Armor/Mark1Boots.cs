using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class Mark1Boots : ModItem
	{
		public override void SetStaticDefaults() 
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Mark I Boots");
			Tooltip.SetDefault("\n8% increased movement speed");
		}

		public override void SetDefaults() 
		{
			Item.width = 22;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = ItemRarityID.Purple;
			Item.defense = 5;
		}

		public override void UpdateEquip(Player player) 
		{
			player.moveSpeed += 0.08f;
			player.rocketBoots += 30;
			// player.noFallDmg = true;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
			.AddIngredient(ItemID.CopperBar, 1)
			.AddIngredient(ItemID.IronBar, 19)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}