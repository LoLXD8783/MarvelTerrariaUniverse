using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class HawkeyeUniform : ModItem
	{
		public override void SetStaticDefaults() 
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Hawkeye Glasses");
		}

		public override void SetDefaults() 
		{
			Item.width = 30;
			Item.height = 20;
			Item.value = 10000;
			Item.rare = ItemRarityID.Purple;
			Item.defense = 6;
			Item.vanity = true;
		}
	}
}