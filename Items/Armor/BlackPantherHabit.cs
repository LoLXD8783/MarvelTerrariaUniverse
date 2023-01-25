using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class BlackPantherHabit : ModItem
	{
		public override void SetStaticDefaults() 
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Black Panther Habit");
			Tooltip.SetDefault("\nGrants immunity to fire blocks and fire"
				+ "\n4% increased melee damage"
				+ "\n3% increased melee critical strike chance");
		}

		public override void SetDefaults() 
		{
			Item.width = 30;
			Item.height = 20;
			Item.value = 10000;
			Item.rare = ItemRarityID.Purple;
			Item.defense = 28;
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
			.AddIngredient(ItemID.GoldBar, 1)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}