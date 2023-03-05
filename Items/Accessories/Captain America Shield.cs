using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Items.Accessories
{
	[AutoloadEquip(EquipType.Shield)] // Load the spritesheet you create as a shield for the player when it is equipped.
	public class CaptainAmericaShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'I can do this all day'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.buyPrice(10);
			Item.rare = ItemRarityID.Green;
			Item.accessory = true;

			Item.defense = 10;
			// Item.lifeRegen = 10;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.Generic) += 1f; // Increase ALL player damage by 100%
			player.endurance = 1f - (0.1f * (1f - player.endurance));  // The percentage of damage reduction
		}
	}
}