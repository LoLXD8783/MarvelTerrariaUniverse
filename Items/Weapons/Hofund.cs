using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Items.Weapons
{
	public class Hofund : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("The key to opening the Bifrost"); // The (English) text shown below your weapon's name.
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 68; // The item texture's width.
			Item.height = 68; // The item texture's height.

			Item.useStyle = ItemUseStyleID.Swing; // The useStyle of the Item.
			Item.useTime = 40; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 40; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
			Item.autoReuse = true; // Whether the weapon can be used more than once automatically by holding the use button.
            Item.DamageType = DamageClass.Melee; // Whether your item is part of the melee class.
			Item.damage = 80; // The damage your item deals.
			Item.knockBack = 6; // The force of knockback of the weapon. Maximum is 20
			Item.crit = 6; // The critical strike chance the weapon has. The player, by default, has a 4% critical strike chance.
            Item.value = Item.buyPrice(gold: 1); // The value of the weapon in copper coins.
			Item.rare = ItemRarityID.Green; // Give this item our custom rarity.
			Item.UseSound = SoundID.Item1; // The sound when the weapon is being used.
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(3))
			{
				// Emit dusts when the sword is swung
				// Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.Sparkle>());
			}
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// Inflict the OnFire debuff for 1 second onto any NPC/Monster that this hits.
			// 60 frames = 1 second
			// target.AddBuff(BuffID.OnFire, 60);
		}
	}
}