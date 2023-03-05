using System;
using Terraria;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Projectiles
{
	public class YakaArrowProjectile : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 42;
			Projectile.height = 42;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.light = 1f;
			Projectile.penetrate = 25;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
		}

		public override void AI()
		{
			float rotationSpeed = (float)Math.PI / 10;
			Projectile.rotation += rotationSpeed;
			//Only do thing on the controller's client perspective
			if (Main.myPlayer == Projectile.owner)
			{
				//Do net updatey thing. Syncs this projectile.
				Projectile.netUpdate = true;

				float maxVelocity = 50f; //maximum velocity projectile can approach cursor
										  //only do this stuff if player is actively channeling
				if (Main.player[Projectile.owner].channel)
				{
					Main.player[Projectile.owner].itemTime = 2;
					Main.player[Projectile.owner].itemAnimation = 2;

					//move towards cursor
					Projectile.velocity = Projectile.DirectionTo(Main.MouseWorld) * maxVelocity;
					float distToMouse = Projectile.Distance(Main.MouseWorld);

					//slows down projectile when getting close to cursor
					if (distToMouse <= maxVelocity * 3)
					{
						Projectile.velocity *= distToMouse / (distToMouse + maxVelocity / 2);
					}
				}
				else
				{
					Projectile.Kill(); //kill projectile if no longer channeling
				}
			}
		}
	}
}