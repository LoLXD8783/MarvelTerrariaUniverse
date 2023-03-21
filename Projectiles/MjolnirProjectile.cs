using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Projectiles
{
	public class MjolnirProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mjolnir Projectile");
		}

		public override void SetDefaults()
		{
			Projectile.width = 28;
			Projectile.height = 28;
			Projectile.aiStyle = 3;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 10;
			Projectile.timeLeft = 600;
			Projectile.extraUpdates = 1;
		}
	}
}