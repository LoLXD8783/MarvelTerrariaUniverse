using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Projectiles
{
	public class MjolnirProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Thor's mighty hammer.");
		}

		public override void SetDefaults()
		{
			Projectile.arrow = true;
			Projectile.width = 46;
			Projectile.height = 44;
			Projectile.CloneDefaults(ProjectileID.LightDisc);
			// Projectile.aiStyle = ProjAIStyleID.Boomerang; This line is not needed since CloneDefaults sets it already.
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
		}

		// Additional hooks/methods here.
	}
}