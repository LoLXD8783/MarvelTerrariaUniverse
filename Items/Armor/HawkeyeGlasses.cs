using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class HawkeyeGlasses : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Hawkeye Glasses");
		}

		public override void SetDefaults() 
		{
			Item.width = 24;
			Item.height = 24;
			Item.value = 10000;
			Item.rare = ItemRarityID.Purple;
			Item.vanity = true;
			Terraria.ID.ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<HawkeyeUniform>() && legs.type == ModContent.ItemType<HawkeyePants>();
		}

		/*public override void UpdateArmorSet(Player player) 
		{
			player.GetDamage(DamageClass.Melee) += 0.4f;
		}*/
	}
}