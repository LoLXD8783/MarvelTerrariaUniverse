using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class WingedHelmet : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Winged Helmet");
			Tooltip.SetDefault("\n9% increased melee damage and critical strike chance");
		}

		public override void SetDefaults() 
		{
			Item.width = 24;
			Item.height = 24;
			Item.value = 10000;
			Item.rare = ItemRarityID.Purple;
			Item.defense = 4;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Melee) += 0.09f;
			player.GetCritChance(DamageClass.Melee) += 9;
		}

		/*public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<SSRUniform>() && legs.type == ModContent.ItemType<SSRBoots>();
		}*/

		/*public override void UpdateArmorSet(Player player) 
		{
			player.GetDamage(DamageClass.Melee) += 0.4f;
		}*/
	}
}