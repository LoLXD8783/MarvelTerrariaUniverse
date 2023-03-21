using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Items.Weapons
{
	public class Mjolnir : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mjolnir");
			Tooltip.SetDefault("'Whosoever holds this hammer, if he be worthy, shall possess the power of Thor'" + "\nA powerful enchanted war-hammer");
		}

		public override void SetDefaults()
		{
			Item.damage = 98;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 46;
			Item.height = 44;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.noUseGraphic = true;
			Item.useStyle = 1;
			Item.knockBack = 3;
			Item.value = 80000;
			Item.rare = 8;
			Item.shootSpeed = 12f;
			Item.shoot = ModContent.ProjectileType<Projectiles.MjolnirProjectile>();
			Item.UseSound = SoundID.Item7;
			Item.autoReuse = true;
		}

		public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
		{
			for (int i = 0; i < 1000; ++i)
			{
				if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
				{
					return false;
				}
			}
			return true;
		}
	}
}