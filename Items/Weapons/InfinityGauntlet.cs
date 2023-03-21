using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Items.Weapons
{
    public class InfinityGauntlet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinity Gauntlet");
            Tooltip.SetDefault("Designed to channel the power of all six Infinity Stones");
            Item.staff[Item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
        }

        public override void SetDefaults()
        {
            Item.damage = 69;
            Item.DamageType = DamageClass.Magic;
            Item.width = 26;
            Item.height = 46;
            Item.useTime = 3;
            Item.useAnimation = 7;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 0;
            Item.value = Item.buyPrice(0, 0, 65, 0);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.NebulaArcanum;
            Item.shootSpeed = 20f;
        }
    }
}