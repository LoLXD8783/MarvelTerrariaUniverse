using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Items.Weapons
{
	public class YakaArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yaka Arrow");
			Tooltip.SetDefault("A whistle-controlled arrow");
		}

		public override void SetDefaults()
		{
			Item.damage = 60; //The damage
			Item.DamageType = DamageClass.Ranged; //Whether or not it is a magic weapon
			Item.width = 42; //Item width
			Item.height = 42; //Item height
			Item.maxStack = 1; //How many of this item you can stack
			Item.useTime = 30; //How long it takes for the item to be used
			Item.useAnimation = 30; //How long the animation of the item takes
			Item.knockBack = 7f; //How much knockback the item produces
			Item.noMelee = true; //Whether the weapon should do melee damage or not
			Item.useStyle = 6; //How the weapon is held, 5 is the gun hold style
			Item.value = 120000; //How much the item is worth
			Item.rare = 8; //The rarity of the item
			Item.shoot = ModContent.ProjectileType<Projectiles.YakaArrowProjectile>(); //What the item shoots, retains an int value
			Item.shootSpeed = 20f; //How fast the projectile fires
			Item.channel = true;
			Item.noUseGraphic = true;
			Item.UseSound = SoundID.Item128;
		}
	}
}
