using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Projectiles
{
    public class IronManHelmet : ModProjectile
    {
        public string TexturePath = "MarvelTerrariaUniverse/TransformationTextures/IronManMk3/IronManMk3_Helmet";

        public override string Texture => TexturePath;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Helmet");

            Main.projFrames[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 20;
            Projectile.friendly = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
            {
                Projectile.velocity.X = oldVelocity.X * -0.3f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
            {
                Projectile.velocity.Y = oldVelocity.Y * -0.3f;
            }

            return false;
        }

        public override void AI()
        {
            MarvelTerrariaUniverseModPlayer ModPlayer = Main.LocalPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>();

            if (!Main.gameMenu) TexturePath = $"MarvelTerrariaUniverse/TransformationTextures/{ModPlayer.ActiveTransformation}/Helmet/{ModPlayer.ActiveTransformation}_Helmet";

            Projectile.velocity.Y += 0.1f;
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }

            if (Projectile.velocity.Y == 0f && Projectile.velocity.X != 0f)
            {
                if (Projectile.velocity.X > -0.01 && Projectile.velocity.X < 0.01)
                {
                    Projectile.velocity.X = 0f;
                    Projectile.netUpdate = true;
                }
            }

            Projectile.velocity.X = Projectile.velocity.X * 0.97f;
            Projectile.rotation += Projectile.velocity.X * 0.1f;

            Projectile.frame = ModPlayer.FaceplateOn ? 0 : 1;

            if (!ModPlayer.TransformationActive_IronMan)
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke);
                }

                Projectile.Kill();
                ModPlayer.HelmetDropped = false;
            }
        }
    }
}