using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;

namespace MarvelTerrariaUniverse.Items.Weapons
{
    public class Moonerangs : Terraria.ModLoader.ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Moon Knight's Boomerang"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("One of Khonshu's weapons used by Moon Knight");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.autoReuse = true;
            Item.damage = 25;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 2;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Purple;
            Item.shootSpeed = 6f;
            Item.useAnimation = 20;
            Item.useTime = 19; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.value = Item.buyPrice(gold: 1);
            Item.shoot = ModContent.ProjectileType<Projectiles.MoonerangsProjectile>();
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
        }
    }
}