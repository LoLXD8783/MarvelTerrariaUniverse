using MarvelTerrariaUniverse.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Projectiles
{
    public class IronManSuit : ModProjectile
    {
        public override string Texture => "MarvelTerrariaUniverse/IMTransformations/TransformationTextures/EmptyPixel";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Suit");
        }

        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 40;
            Projectile.friendly = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override void AI()
        {
            IronManModPlayer ModPlayer = Main.LocalPlayer.GetModPlayer<IronManModPlayer>();

            Projectile.velocity.X = 0;
            Projectile.velocity.Y += 0.1f;
            if (Projectile.velocity.Y > 16f) Projectile.velocity.Y = 16f;

            if (!ModPlayer.SuitEjected)
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke);
                }

                Projectile.Kill();
                ModPlayer.SuitEjected = false;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            IronManModPlayer IronManModPlayer = Main.LocalPlayer.GetModPlayer<IronManModPlayer>();
            MTUModPlayer MTUModPlayer = Main.LocalPlayer.GetModPlayer<MTUModPlayer>();

            Texture2D Texture = ModContent.Request<Texture2D>($"MarvelTerrariaUniverse/IMTransformations/TransformationTextures/{MTUModPlayer.ActiveTransformation}/{MTUModPlayer.ActiveTransformation}_Preview").Value;

            Main.spriteBatch.Draw(Texture, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, Projectile.rotation, Projectile.Size / 2, 1f, IronManModPlayer.EjectedSuitDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);

            return false;
        }
    }
}