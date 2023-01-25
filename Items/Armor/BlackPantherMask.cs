using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class BlackPantherMask : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Black Panther Mask");
			Tooltip.SetDefault("\n16% increased ranged damage"
				+ "\n7% increased ranged critical strike chance"
				+ "\n9% increased melee damage and critical strike chance"
				+ "\n9% increased melee speed");
		}

		public override void SetDefaults() 
		{
			Item.width = 16;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = ItemRarityID.Purple;
			Item.defense = 20;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Melee) += 0.09f;
			player.GetCritChance(DamageClass.Melee) += 9;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<BlackPantherHabit>() && legs.type == ModContent.ItemType<BlackPantherBoots>();
		}

		public override void UpdateArmorSet(Player player) 
		{
			player.GetDamage(DamageClass.Melee) += 0.4f;
			player.GetDamage(DamageClass.Ranged) += 0.4f;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
			.AddIngredient(ItemID.GoldBar, 1)
			.AddIngredient(ItemID.TitaniumBar, 12)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}