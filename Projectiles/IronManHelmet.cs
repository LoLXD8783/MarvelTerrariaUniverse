using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Projectiles
{
    public class IronManHelmet : ModProjectile
    {
        public override string Texture => "MarvelTerrariaUniverse/TransformationTextures/EmptyPixel";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Helmet");
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 20;
            Projectile.friendly = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f) Projectile.velocity.X = oldVelocity.X * -0.3f;
            if (Projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f) Projectile.velocity.Y = oldVelocity.Y * -0.3f;

            return false;
        }

        public override void AI()
        {
            MarvelTerrariaUniverseModPlayer ModPlayer = Main.LocalPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>();

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

        public override void PostDraw(Color lightColor)
        {
            MarvelTerrariaUniverseModPlayer ModPlayer = Main.LocalPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>();
            Texture2D Texture = ModContent.Request<Texture2D>($"MarvelTerrariaUniverse/TransformationTextures/{ModPlayer.ActiveTransformation}/{ModPlayer.ActiveTransformation}_Helmet").Value;
            int HelmetFrame = (!ModPlayer.TransformationActive_IronManMk1 && ModPlayer.FaceplateOn) || ModPlayer.TransformationActive_IronManMk1 ? 0 : 1;

            Main.EntitySpriteDraw(Texture, Projectile.Center, new Rectangle(0, 20 * HelmetFrame, 18, ModPlayer.TransformationActive_IronManMk1 ? 18 : 20), Color.White, Projectile.rotation, Projectile.Center, 1f, SpriteEffects.None, 0);
        }
    }
}