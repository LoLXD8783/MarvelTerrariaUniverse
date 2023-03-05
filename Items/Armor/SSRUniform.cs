using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class SSRUniform : ModItem
	{
		public override void SetStaticDefaults() 
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Captain America SSR Field Uniform");
			Tooltip.SetDefault("\n4% increased melee damage"
				+ "\n3% increased melee critical strike chance");
		}

		public override void SetDefaults() 
		{
			Item.width = 30;
			Item.height = 20;
			Item.value = 10000;
			Item.rare = ItemRarityID.Purple;
			Item.defense = 6;
		}

		public override void UpdateEquip(Player player) 
		{
			player.GetDamage(DamageClass.Melee) += 0.04f;
			player.GetCritChance(DamageClass.Melee) += 3;
		}
	}
}