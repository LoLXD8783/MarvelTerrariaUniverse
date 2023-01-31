using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class SpiderManMask : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Spider-Man Mask");
			Tooltip.SetDefault("\n16% increased ranged damage"
				+ "\n7% increased ranged critical strike chance"
				+ "\n9% increased melee damage and critical strike chance"
				+ "\n9% increased melee speed");
		}

		public override void SetDefaults() 
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = ItemRarityID.Purple;
			Item.defense = 6;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Melee) += 0.09f;
			player.GetCritChance(DamageClass.Melee) += 9;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<SpiderManSuit>() && legs.type == ModContent.ItemType<SpiderManLeggings>();
		}

		public override void UpdateArmorSet(Player player) 
		{
			player.GetDamage(DamageClass.Melee) += 0.4f;
			player.GetDamage(DamageClass.Ranged) += 0.4f;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
            .AddIngredient(ItemID.Cobweb, 10)
            .AddIngredient(ItemID.Silk, 1)
            .AddIngredient(ItemID.SpiderFang, 6)
            .AddTile(TileID.Loom)
			.Register();
		}
	}
}