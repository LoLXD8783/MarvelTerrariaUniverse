using MarvelTerrariaUniverse.Tiles;
using MarvelTerrariaUniverse.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse
{
    public class MTUModPlayer : ModPlayer
    {
        IronManModPlayer IronManModPlayer => Player.GetModPlayer<IronManModPlayer>();

        #region Glowmask Support

        private static Dictionary<int, Func<Color>> BodyColor { get; set; }

        /// <summary>
        /// Add glowmask color associated with the body equip slot here, usually in <see cref="ModType.SetStaticDefaults"/>.
        /// <para>Don't forget the !Main.dedServ check!</para>
        /// </summary>
        /// <param name="bodySlot">Body equip slot</param>
        /// <param name="color">Color</param>
        public static void RegisterData(int bodySlot, Func<Color> color)
        {
            if (!BodyColor.ContainsKey(bodySlot))
            {
                BodyColor.Add(bodySlot, color);
            }
        }

        public override void Load()
        {
            BodyColor = new Dictionary<int, Func<Color>>();
        }

        public override void Unload()
        {
            BodyColor = null;
        }

        #endregion

        public bool TransformationActive => IronManModPlayer.TransformationActive_IronMan;
        public string ActiveTransformation = "None";

        public void UseEquipSlot(string texture)
        {
            if (texture.Contains("IronMan"))
            {
                if (texture == "IronManMk1") Player.head = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Head);
                else
                {
                    Player.head = EquipLoader.GetEquipSlot(Mod, $"{texture}_Faceplate{IronManModPlayer.FaceplateFrameCount}", EquipType.Head);

                    if (!Main.dedServ)
                    {
                        if (IronManModPlayer.FaceplateFrameCount < 3)
                        {
                            HeadLayer.RegisterData(EquipLoader.GetEquipSlot(Mod, $"{texture}_Faceplate{IronManModPlayer.FaceplateFrameCount}", EquipType.Head), new DrawLayerData()
                            {
                                Texture = ModContent.Request<Texture2D>($"MarvelTerrariaUniverse/TransformationTextures/Glowmasks/IronMan_Faceplate{IronManModPlayer.FaceplateFrameCount}_Glowmask")
                            });
                        }

                        RegisterData(EquipLoader.GetEquipSlot(Mod, texture, EquipType.Body), () => new Color(255, 255, 255, 0));
                    }
                }
            }
            else Player.head = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Head);

            Player.body = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Body);
            Player.legs = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Legs);

            ActiveTransformation = texture;
        }

        public void ResetEquipSlot()
        {
            IronManModPlayer.ResetSuits_IronMan();
        }

        public override void ResetEffects()
        {
            ModContent.GetInstance<GantryTile>().PlayGantryFrames = false;
        }

        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if (!BodyColor.TryGetValue(Player.body, out Func<Color> color)) return;

            drawInfo.bodyGlowColor = color();
            drawInfo.armGlowColor = color();

            if (drawInfo.drawPlayer == UICharacterEquipped.DrawnPlayer)
            {
                drawInfo.colorArmorHead = Color.White;
                drawInfo.colorArmorBody = Color.White;
                drawInfo.colorArmorLegs = Color.White;
            }
        }
    }
}