using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class Mark1Armor : ModItem
	{
		public override void SetStaticDefaults() 
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Mark I Armor");
			Tooltip.SetDefault("\n4% increased melee damage & critical strike chance"
				+ "\n3% increased ranged damage & critical strike chance"
				+ "\n+20 max life");
		}

		public override void SetDefaults() 
		{
			Item.width = 17;
			Item.height = 12;
			Item.value = 10000;
			Item.rare = ItemRarityID.Purple;
			Item.defense = 6;
		}

		public override void UpdateEquip(Player player) 
		{
			// player.buffImmune[BuffID.OnFire] = true;
			// player.buffImmune[BuffID.Burning] = true;
			// player.buffImmune[BuffID.ObsidianSkin] = true;
			player.statLifeMax2 += 20;
			player.GetDamage(DamageClass.Melee) += 0.04f;
			player.GetDamage(DamageClass.Ranged) += 0.03f;
			player.GetCritChance(DamageClass.Melee) += 4;
			player.GetCritChance(DamageClass.Ranged) += 3;
			Lighting.AddLight(player.position, Microsoft.Xna.Framework.Color.White.ToVector3());
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
			.AddIngredient(ItemID.CopperBar, 1)
			.AddIngredient(ItemID.IronBar, 25)
			.AddIngredient(ItemID.Leather, 5)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}