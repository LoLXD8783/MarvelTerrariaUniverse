using MarvelTerrariaUniverse.Dusts;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Items.Weapons
{
    class Tesseract : Terraria.ModLoader.ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tesseract");
            Tooltip.SetDefault("A crystalline cube-shaped containment vessel for the Space Stone");
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<TesseractDust>());
        }
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 5;

            Item.autoReuse = true;
            Item.damage = 70;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 8f;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Purple;
            Item.shootSpeed = 8f;
            Item.useAnimation = 5;
            Item.useTime = 5; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Thrust;
            Item.value = Item.buyPrice(gold: 1);
            Item.shoot = ModContent.ProjectileType<Projectiles.TesseractProjectile>();
            Item.UseSound = SoundID.Item15;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int rdm = Main.rand.Next(1, 10);
            for (int i = 0; i < rdm; i++)
            {
                // Credit to https://forums.terraria.org/index.php?threads/tutorial-tmodloader-projectile-help.68337/
                // User: AwesomePerson159 (great name btw)
                Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(12));

                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, Main.myPlayer);
            }
            return true;
        }
    }
}