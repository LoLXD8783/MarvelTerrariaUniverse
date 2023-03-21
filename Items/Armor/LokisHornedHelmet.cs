using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class LokisHornedHelmet : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Loki's Horned Helmet");
			Tooltip.SetDefault("\n9% increased melee damage and critical strike chance");
		}

		public override void SetDefaults() 
		{
			Item.width = 22;
			Item.height = 28;
			Item.value = 10000;
			Item.rare = ItemRarityID.Purple;
			Item.defense = 4;
			Item.vanity = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Melee) += 0.09f;
			player.GetCritChance(DamageClass.Melee) += 9;
		}
	}
}