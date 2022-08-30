using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Projectiles
{
    public class IronManRepulsor : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.scale = 0.75f;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type]) Projectile.frame = 0;
            }

            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Projectile.spriteDirection == -1) Projectile.rotation += MathHelper.Pi;

            for (int i = 0; i < 1; i++)
            {
                int ActiveDust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.FireworkFountain_Yellow, Projectile.velocity.X);
                Main.dust[ActiveDust].position -= Vector2.One * 3f;
                Main.dust[ActiveDust].scale = 0.5f;
                Main.dust[ActiveDust].noGravity = true;
                Main.dust[ActiveDust].velocity = Projectile.velocity / 3f;
            }
        }

        public override void Kill(int timeLeft)
        {

        }
    }
}
