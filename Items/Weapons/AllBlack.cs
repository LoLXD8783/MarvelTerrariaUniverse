using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Items.Weapons
{
	public class AllBlack : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("All-Black the Necrosword");
			Tooltip.SetDefault("Slowly corrupts the user"); // The (English) text shown below your weapon's name.
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 60; // The item texture's width.
			Item.height = 60; // The item texture's height.

			Item.useStyle = ItemUseStyleID.Swing; // The useStyle of the Item.
			Item.useTime = 40; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 40; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
			Item.autoReuse = true; // Whether the weapon can be used more than once automatically by holding the use button.
			Item.shoot = ProjectileID.DD2SquireSonicBoom; // ID of the projectiles the sword will shoot
			Item.shootSpeed = 8f;
			Item.DamageType = DamageClass.Melee; // Whether your item is part of the melee class.
			Item.damage = 80; // The damage your item deals.
			Item.knockBack = 6; // The force of knockback of the weapon. Maximum is 20
			Item.crit = 6; // The critical strike chance the weapon has. The player, by default, has a 4% critical strike chance.
            Item.value = Item.buyPrice(gold: 1); // The value of the weapon in copper coins.
			Item.rare = ItemRarityID.Green; // Give this item our custom rarity.
			Item.UseSound = SoundID.Item1; // The sound when the weapon is being used.
		}

        public override void HoldItem(Player player)
        {
            base.HoldItem(player);
            Dust dust;
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            Vector2 position = Main.LocalPlayer.Center;
            dust = Dust.NewDustDirect(position, 0, 0, DustID.Ash, 0f, 0f, 0, new Color(255, 255, 255), 1f);
            dust.noGravity = false;
            dust.fadeIn = 0.5f;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player, target, damage, knockBack, crit);

            if (Main.rand.NextBool(50))
            {
                Item.damage = 1000;
                //Item.crit = 100; Overkill tbh -Quidd
            }
            if (Main.rand.NextBool(2))
            {
                Item.damage = 100;
                //Item.crit = 0;
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust dust;
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            Vector2 position = Main.LocalPlayer.Center;
            dust = Dust.NewDustDirect(position, 0, 0, DustID.Ash, 0f, 0f, 0, new Color(255, 255, 255), 1f);
            dust.noGravity = true;
            dust.fadeIn = 0.5f;
        }
	}
}