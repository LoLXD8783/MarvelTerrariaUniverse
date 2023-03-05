using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class SpiderManLeggings : ModItem
	{
		public override void SetStaticDefaults() 
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Spider-Man Leggings");
			Tooltip.SetDefault("\n12% increased movement speed" + "\nNegates fall damage");
		}

		public override void SetDefaults() 
		{
			Item.width = 22;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = ItemRarityID.Purple;
			Item.defense = 4;
		}

		public override void UpdateEquip(Player player) 
		{
			player.moveSpeed += 0.12f;
			player.noFallDmg = false;
		}
	}
}