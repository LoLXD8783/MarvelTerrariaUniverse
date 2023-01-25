using MarvelTerrariaUniverse.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.ModPlayers
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
            if (texture.Contains("IronMan") || texture.Contains("WarMachine"))
            {
                if (texture == "IronManMk1") Player.head = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Head);
                else
                {
                    Player.head = EquipLoader.GetEquipSlot(Mod, $"{texture}_Faceplate{IronManModPlayer.FaceplateFrameCount}", EquipType.Head);
                    Player.body = EquipLoader.GetEquipSlot(Mod, IronManModPlayer.Flying ? $"{texture}_Body_Flight" : texture, EquipType.Body);
                    Player.legs = EquipLoader.GetEquipSlot(Mod, IronManModPlayer.Flying && IronManModPlayer.Hovering ? $"{texture}_Legs_Hover" : texture, EquipType.Legs);

                    if (!Main.dedServ)
                    {
                        if (IronManModPlayer.FaceplateFrameCount < 3)
                        {
                            HeadLayer.RegisterData(EquipLoader.GetEquipSlot(Mod, $"{texture}_Faceplate{IronManModPlayer.FaceplateFrameCount}", EquipType.Head), new DrawLayerData()
                            {
                                Texture = ModContent.Request<Texture2D>($"MarvelTerrariaUniverse/IMTransformations/TransformationTextures/Glowmasks/IronMan_Faceplate{IronManModPlayer.FaceplateFrameCount}_Glowmask"),
                                Color = (drawInfo) => texture.Contains("WarMachine") ? new Color(255, 202, 191) : Color.White
                            });
                        }

                        RegisterData(EquipLoader.GetEquipSlot(Mod, texture, EquipType.Body), () => Color.White);
                        RegisterData(EquipLoader.GetEquipSlot(Mod, $"{texture}_Body_Flight", EquipType.Body), () => Color.White);
                    }
                }
            }
            else
            {
                Player.head = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Head);
                Player.body = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Body);
                Player.legs = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Legs);
            }

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
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Keybinds.Reinstantiate.JustPressed)
            {
                ModContent.GetInstance<MarvelTerrariaUniverseModSystem>().Load();
            }
        }
    }
}