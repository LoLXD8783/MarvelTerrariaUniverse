﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace MarvelTerrariaUniverse.Content.Projectiles.IronMan;

/*public class Laser2 : ModProjectile // making another version of the laser
{
    public override string Texture => ModContent.GetInstance<Laser>().Texture;
    public override void SetDefaults()
    {

    }
    public override void AI()
    {

    }
    public override bool PreDrawExtras()
    {
        Texture2D tex = ModContent.Request<Texture2D>(Texture).Value;
        Rectangle middle = new Rectangle(0, 26, 20, 21);
        var start = Projectile.Center;
        var end = start + Vector2.Normalize(Projectile.velocity) * 10 * 16;
        DrawLaser(tex, default, middle, default, start, end);
        return base.PreDrawExtras();
    }


    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        Player player = Main.player[Projectile.owner];
        Vector2 unit = Projectile.velocity;
        float point = 0f;
        // Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
        // It will look for collisions on the given line using AABB
        return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Origin,
            Origin + unit * Distance, 5, ref point);
    }
    void DrawLaser(Texture2D texture, Rectangle startRect, Rectangle middleRect, Rectangle endRect, Vector2 start, Vector2 end)
    {
        Vector2 unit = start.DirectionTo(end) * texture.Width;
        float len = Vector2.Distance(start, end);
        // draw start texture
        float rot = unit.ToRotation();
        for (float k = 0; k < len; k++)
        {
            Vector2 drawLocation = start + unit * k;
            Main.EntitySpriteDraw(texture, drawLocation, middleRect, Color.White, rot, middleRect.Size() / 2, 1f, SpriteEffects.None, 0);
        }
        // draw end texture
    }
}*/
/*public class Laser : ModProjectile
{
    //The distance of beam from the player center
    private const float MOVE_DISTANCE = 25;

    public float Distance
    {
        get => Projectile.ai[0];
        set => Projectile.ai[0] = value;
    }

    public override void SetDefaults()
    {
        Projectile.width = 10;
        Projectile.height = 10;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = true;
        Projectile.hide = false;
        Projectile.timeLeft = 5; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)

    }

    Vector2 Origin => Main.player[Projectile.owner].Center + Projectile.Size / 2 - new Vector2(5, 0);

    public override bool PreDraw(ref Color color)
    {
        // Needed because of parameters
        Texture2D Texture = ModContent.Request<Texture2D>($"MarvelTerrariaUniverse/Content/Projectiles/IronMan/Laser").Value;

        DrawLaser(Texture, Origin, Projectile.velocity, 10, Projectile.damage, -1.57f, 1f, 1000f, Color.White, (int)MOVE_DISTANCE);
        return false;
    }

    // The core function of drawing a laser
    public void DrawLaser(Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default(Color), int transDist = 50)
    {
        float r = unit.ToRotation() + rotation;

        // Draws the laser 'body'
        for (float i = transDist; i <= Distance; i += step)
        {
            Color c = Color.White;
            var origin = start + i * unit;
            Main.spriteBatch.Draw(texture, origin - Main.screenPosition,
                 new Rectangle(0, 26, 20, 21), i < transDist ? Color.Transparent : c, r,
                 new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
        }

        //// Draws the laser 'tail'
        //Main.spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
        //    new Rectangle(0, 0, 21, 21), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);

        //// Draws the laser 'head'
        //Main.spriteBatch.Draw(texture, start + (Distance + step) * unit - Main.screenPosition,
        //    new Rectangle(0, 52, 21, 35), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
    }

    // Change the way of collision check of the Projectile
    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        // We can only collide if we are at max charge, which is when the laser is actually fired

        Player player = Main.player[Projectile.owner];
        Vector2 unit = Projectile.velocity;
        float point = 0f;
        // Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
        // It will look for collisions on the given line using AABB
        return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Origin,
            Origin + unit * Distance, 5, ref point);
    }

    // Set custom immunity time on hitting an NPC
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        target.immune[Projectile.owner] = 5;
    }

    // The AI of the Projectile
    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        Projectile.position = player.Center + Projectile.velocity * MOVE_DISTANCE;
        Projectile.scale *= 0.3f;
        if (Projectile.scale <= 0.01f) Projectile.Kill();
        UpdatePlayer(player);
        SetLaserPosition(player);
        //SpawnDusts(player);
        CastLights();
    }

    private void SpawnDusts(Player player)
    {
        Vector2 unit = Projectile.velocity * -1;
        Vector2 dustPos = player.Center + Projectile.velocity * Distance;

        for (int i = 0; i < 2; ++i)
        {
            float num1 = Projectile.velocity.ToRotation() + (Main.rand.Next(2) == 1 ? -1.0f : 1.0f) * 1.57f;
            float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
            Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
            Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, 226, dustVel.X, dustVel.Y)];
            dust.noGravity = true;
            dust.scale = 1.2f;
            dust = Dust.NewDustDirect(Main.player[Projectile.owner].Center, 0, 0, 31,
                -unit.X * Distance, -unit.Y * Distance);
            dust.fadeIn = 0f;
            dust.noGravity = true;
            dust.scale = 0.88f;
            dust.color = Color.Yellow;
        }

        if (Main.rand.NextBool(5))
        {
            Vector2 offset = Projectile.velocity.RotatedBy(1.57f) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
            Dust dust = Main.dust[Dust.NewDust(dustPos + offset - Vector2.One * 4f, 8, 8, 31, 0.0f, 0.0f, 100, new Color(), 1.5f)];
            dust.velocity *= 0.5f;
            dust.velocity.Y = -Math.Abs(dust.velocity.Y);
            unit = dustPos - Main.player[Projectile.owner].Center;
            unit.Normalize();
            dust = Main.dust[Dust.NewDust(Main.player[Projectile.owner].Center + 55 * unit, 8, 8, 31, 0.0f, 0.0f, 100, new Color(), 1.5f)];
            dust.velocity = dust.velocity * 0.5f;
            dust.velocity.Y = -Math.Abs(dust.velocity.Y);
            dust.color = Color.Yellow;

        }
    }

    *//*
    * Sets the end of the laser position based on where it collides with something
    *//*
    private void SetLaserPosition(Player player)
    {
        for (Distance = MOVE_DISTANCE; Distance <= 2200f; Distance += 5f)
        {
            var start = player.Center + Projectile.velocity * Distance;
            if (!Collision.CanHit(player.Center, 1, 1, start, 1, 1))
            {
                Distance -= 5f;
                break;
            }
        }
    }



    private void UpdatePlayer(Player player)
    {
        // Multiplayer support here, only run this code if the client running it is the owner of the Projectile
        if (Projectile.owner == Main.myPlayer)
        {
            Vector2 diff = Main.MouseWorld - player.Center;
            diff.Normalize();
            Projectile.velocity = diff;
            Projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
            Projectile.netUpdate = true;
        }
        int dir = Projectile.direction;
        player.ChangeDir(dir); // Set player direction to where we are shooting
        player.heldProj = Projectile.whoAmI; // Update player's held Projectile

    }

    private void CastLights()
    {
        // Cast a light along the line of the laser
        DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
        Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * (Distance - MOVE_DISTANCE), 26, DelegateMethods.CastLight);
    }

    public override bool ShouldUpdatePosition() => false;

    *//*
    * Update CutTiles so the laser will cut tiles (like grass)
    *//*
    public override void CutTiles()
    {
        DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
        Vector2 unit = Projectile.velocity;
        Utils.PlotTileLine(Projectile.Center, Projectile.Center + unit * Distance, (Projectile.width + 16) * Projectile.scale, DelegateMethods.CutTiles);
    }
}*/