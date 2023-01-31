using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class SpiderManSuit : ModItem
	{
		public override void SetStaticDefaults() 
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Spider-Man Suit");
			Tooltip.SetDefault("\n4% increased melee damage"
				+ "\n3% increased melee critical strike chance");
		}

		public override void SetDefaults() 
		{
			Item.width = 30;
			Item.height = 20;
			Item.value = 10000;
			Item.rare = ItemRarityID.Purple;
			Item.defense = 8;
		}

		public override void UpdateEquip(Player player) 
		{
			player.buffImmune[BuffID.OnFire] = true;
			player.buffImmune[BuffID.Burning] = true;
			player.GetDamage(DamageClass.Melee) += 0.04f;
			player.GetCritChance(DamageClass.Melee) += 3;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
            .AddIngredient(ItemID.Cobweb, 20)
            .AddIngredient(ItemID.Silk, 2)
            .AddIngredient(ItemID.SpiderFang, 14)
            .AddTile(TileID.Loom)
			.Register();
		}
	}
}