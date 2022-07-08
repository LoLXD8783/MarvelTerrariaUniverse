using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse
{
    public class DrawLayerData
    {
        public static Color DefaultColor(PlayerDrawSet drawInfo) => new Color(255, 255, 255, 0) * 0.8f;

        public Asset<Texture2D> Texture { get; init; }

        public Func<PlayerDrawSet, Color> Color { get; init; } = DefaultColor;
    }

    public sealed class HeadLayer : PlayerDrawLayer
    {
        private static Dictionary<int, DrawLayerData> HeadLayerData { get; set; }

        /// <summary>
        /// Add data associated with the head equip slot here, usually in <see cref="ModType.SetStaticDefaults"/>.
        /// <para>Don't forget the !Main.dedServ check!</para>
        /// </summary>
        /// <param name="headSlot">Head equip slot</param>
        /// <param name="data">Data</param>
        public static void RegisterData(int headSlot, DrawLayerData data)
        {
            if (!HeadLayerData.ContainsKey(headSlot))
            {
                HeadLayerData.Add(headSlot, data);
            }
        }

        public override void Load()
        {
            HeadLayerData = new Dictionary<int, DrawLayerData>();
        }

        public override void Unload()
        {
            HeadLayerData = null;
        }

        public override Position GetDefaultPosition()
        {
            return new AfterParent(PlayerDrawLayers.Head);
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            if (drawPlayer.dead || drawPlayer.invis || drawPlayer.head == -1)
            {
                return false;
            }

            return true;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            if (!HeadLayerData.TryGetValue(drawPlayer.head, out DrawLayerData data))
            {
                return;
            }

            Color color = drawPlayer.GetImmuneAlphaPure(data.Color(drawInfo), drawInfo.shadow);

            Texture2D texture = data.Texture.Value;
            Vector2 drawPos = drawInfo.Position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.headPosition;
            Vector2 headVect = drawInfo.headVect;
            DrawData drawData = new(texture, drawPos.Floor() + headVect, drawPlayer.bodyFrame, color, drawPlayer.headRotation, headVect, 1f, drawInfo.playerEffect, 0)
            {
                shader = drawInfo.cHead
            };
            drawInfo.DrawDataCache.Add(drawData);
        }
    }

    public sealed class LegsLayer : PlayerDrawLayer
    {
        private static Dictionary<int, DrawLayerData> LegsLayerData { get; set; }

        /// <summary>
        /// Add data associated with the leg equip slot here, usually in <see cref="ModType.SetStaticDefaults"/>.
        /// <para>Don't forget the !Main.dedServ check!</para>
        /// </summary>
        /// <param name="legSlot">Leg equip slot</param>
        /// <param name="data">Data</param>
        public static void RegisterData(int legSlot, DrawLayerData data)
        {
            if (!LegsLayerData.ContainsKey(legSlot))
            {
                LegsLayerData.Add(legSlot, data);
            }
        }

        public override void Load()
        {
            LegsLayerData = new Dictionary<int, DrawLayerData>();
        }

        public override void Unload()
        {
            LegsLayerData = null;
        }

        public override Position GetDefaultPosition()
        {
            return new AfterParent(PlayerDrawLayers.Leggings);
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            if (drawPlayer.dead || drawPlayer.invis || drawPlayer.legs == -1)
            {
                return false;
            }
            return true;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            if (!LegsLayerData.TryGetValue(drawPlayer.legs, out DrawLayerData data))
            {
                return;
            }

            Color color = drawPlayer.GetImmuneAlphaPure(data.Color(drawInfo), drawInfo.shadow);

            Texture2D texture = data.Texture.Value;
            Vector2 drawPos = drawInfo.Position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.legFrame.Width / 2, drawPlayer.height - drawPlayer.legFrame.Height + 4f) + drawPlayer.legPosition;
            Vector2 legsOffset = drawInfo.legsOffset;
            DrawData drawData = new(texture, drawPos.Floor() + legsOffset, drawPlayer.legFrame, color, drawPlayer.legRotation, legsOffset, 1f, drawInfo.playerEffect, 0)
            {
                shader = drawInfo.cLegs
            };
            drawInfo.DrawDataCache.Add(drawData);
        }
    }
}
