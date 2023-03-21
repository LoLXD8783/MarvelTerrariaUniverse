using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ReLogic.Content;
using Terraria.UI;
using Terraria.ModLoader;
using MarvelTerrariaUniverse.ModPlayers;
using System.Diagnostics.Tracing;
using System;

namespace MarvelTerrariaUniverse.UI.Elements
{
    public class UIAnimatedImage : UIElement
    {
        public Asset<Texture2D> Texture;
        public int FrameCount;
        public string HoverText;
        public float PowerBars;
        public int PowerValue;
        public float horizontalIncrementPowerbar = -45.5f;
        public int j = 0;
        public bool IronManPowerHUDActive = false;

        public UIAnimatedImage(Asset<Texture2D> texture, int frameCount, string hoverText = null, bool IronManPowerHUDActivated = false) : base()
        {
            Texture = texture;
            FrameCount = frameCount;
            HoverText = hoverText;
            IronManPowerHUDActive = IronManPowerHUDActivated;
            

            Width = StyleDimension.FromPixels(Texture.Width());
            Height = StyleDimension.FromPixels(Texture.Height() / FrameCount);
        }

        int CurrentFrame = 0;
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            IronManModPlayer IronManModPlayer = Main.LocalPlayer.GetModPlayer<IronManModPlayer>();
            PowerBars = IronManModPlayer.Power;
            PowerValue = (int)Math.Round(IronManModPlayer.Power);
            Rectangle Dimensions = GetDimensions().ToRectangle();
            Texture2D powerBar = ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/IronManHUD/Iron_Man_UI_Suit_Power_Bar").Value;
            Texture2D percentageMeter = ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/IronManHUD/SuitPowerPercentages").Value;
            spriteBatch.Draw(Texture.Value, new Vector2(Dimensions.X, Dimensions.Y), new Rectangle(0, CurrentFrame * Texture.Height() / FrameCount, Texture.Width(), Texture.Height() / FrameCount), Color.White);
            if (HoverText != null)
            {
                if (IsMouseHovering) Main.hoverItemName = HoverText;
            }
            #region IronManPowerHUD
            if (IronManPowerHUDActive)
            {
                if (CurrentFrame >= FrameCount - 1) CurrentFrame = 0;
                else CurrentFrame++;
                spriteBatch.Draw(percentageMeter, new Vector2(Dimensions.X + 17.5f, Dimensions.Y + 26.5f), new Rectangle(0, (PowerValue) * percentageMeter.Height / 101, percentageMeter.Width, percentageMeter.Height / 101), Color.White);
                if (PowerBars / 20 > 0)
                {
                    spriteBatch.Draw(powerBar, new Vector2(Dimensions.X + 263.5f, Dimensions.Y + 27.75f), Color.White);
                }
                if (PowerBars / 20 >= 1)
                {
                    spriteBatch.Draw(powerBar, new Vector2(Dimensions.X + 263.5f + horizontalIncrementPowerbar, Dimensions.Y + 27.75f), Color.White);
                }
                if (PowerBars / 20 >= 2)
                {
                    spriteBatch.Draw(powerBar, new Vector2(Dimensions.X + 263.5f + 2 * horizontalIncrementPowerbar, Dimensions.Y + 27.75f), Color.White);
                }
                if (PowerBars / 20 >= 3)
                {
                    spriteBatch.Draw(powerBar, new Vector2(Dimensions.X + 263.5f - 0.5f + 3 * horizontalIncrementPowerbar, Dimensions.Y + 27.75f), Color.White);
                }
                if (PowerBars / 20 > 4)
                {
                    spriteBatch.Draw(powerBar, new Vector2(Dimensions.X + 263.5f - 1f + 4 * horizontalIncrementPowerbar, Dimensions.Y + 27.75f), Color.White);
                }
            }
            #endregion
        }
    }
}
