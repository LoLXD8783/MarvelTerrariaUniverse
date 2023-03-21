using MarvelTerrariaUniverse.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Projectiles
{
    public class IronManHelmet : ModProjectile
    {
        public override string Texture => "MarvelTerrariaUniverse/IMTransformations/TransformationTextures/EmptyPixel";

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
            IronManModPlayer ModPlayer = Main.LocalPlayer.GetModPlayer<IronManModPlayer>();

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

            if (!ModPlayer.TransformationActive_IronMan || (Projectile.Hitbox.Contains(Main.MouseWorld.ToPoint()) && PlayerInput.Triggers.JustPressed.MouseLeft))
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke);
                }

                Projectile.Kill();
                ModPlayer.HelmetDropped = false;
                ModPlayer.HelmetOn = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            IronManModPlayer IronManModPlayer = Main.LocalPlayer.GetModPlayer<IronManModPlayer>();
            MTUModPlayer MTUModPlayer = Main.LocalPlayer.GetModPlayer<MTUModPlayer>();

            Texture2D Texture = ModContent.Request<Texture2D>($"MarvelTerrariaUniverse/IMTransformations/TransformationTextures/{MTUModPlayer.ActiveTransformation}/{MTUModPlayer.ActiveTransformation}_Helmet").Value;
            int HelmetFrame = (!IronManModPlayer.TransformationActive_IronManMk1 && IronManModPlayer.FaceplateOn) || IronManModPlayer.TransformationActive_IronManMk1 ? 0 : 1;

            Main.spriteBatch.Draw(Texture, Projectile.Center - Main.screenPosition, new Rectangle(0, (Texture.Height / 2) * HelmetFrame, Texture.Width, Texture.Height / 2), Color.White, Projectile.rotation, Projectile.Size / 2, 1f, SpriteEffects.None, 0);

            return false;
        }
    }
}