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
    public sealed class FaceplateLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition()
        {
            return new AfterParent(PlayerDrawLayers.Head);
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            MarvelTerrariaUniverseModPlayer ModPlayer = drawInfo.drawPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>();

            return (!drawInfo.drawPlayer.dead || !drawInfo.drawPlayer.invis) && !Main.gameInactive && ModPlayer.TransformationActive_IronMan;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            MarvelTerrariaUniverseModPlayer ModPlayer = drawPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>();
            string textureName = drawPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>().ActiveTransformation;

            Texture2D texture = ModContent.Request<Texture2D>($"MarvelTerrariaUniverse/TransformationTextures/{textureName}/{textureName}_FaceplateFrames").Value;
            Vector2 drawPos = drawInfo.Position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.headPosition;
            Vector2 headVect = drawInfo.headVect;

            DrawData drawData = new(texture, drawPos.Floor() + headVect, new Rectangle(0, 56 * ModPlayer.FaceplateAnimFrame, drawPlayer.bodyFrame.Width, drawPlayer.bodyFrame.Height), Color.White, drawPlayer.headRotation, headVect, 1f, drawInfo.playerEffect, 0);
            drawInfo.DrawDataCache.Add(drawData);
        }
    }
}