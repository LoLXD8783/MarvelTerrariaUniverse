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
				+ "\n7% increased ranged critical strike chance");
		}

		public override void SetDefaults() 
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 10000;
			Item.rare = ItemRarityID.Purple;
			Item.defense = 4;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Ranged) += 0.16f;
			player.GetCritChance(DamageClass.Ranged) += 7;
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
	}
}