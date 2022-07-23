using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ReLogic.Content;
using Terraria.UI;

namespace MarvelTerrariaUniverse.UI.Elements
{
    public class UIAnimatedImage : UIElement
    {
        public Asset<Texture2D> Texture;
        public int FrameCount;
        public string HoverText;

        public UIAnimatedImage(Asset<Texture2D> texture, int frameCount, string hoverText = null) : base()
        {
            Texture = texture;
            FrameCount = frameCount;
            HoverText = hoverText;

            Width = StyleDimension.FromPixels(Texture.Width());
            Height = StyleDimension.FromPixels(Texture.Height() / FrameCount);
        }

        int CurrentFrame = 0;
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Rectangle Dimensions = GetDimensions().ToRectangle();

            spriteBatch.Draw(Texture.Value, new Vector2(Dimensions.X, Dimensions.Y), new Rectangle(0, CurrentFrame * Texture.Height() / FrameCount, Texture.Width(), Texture.Height() / FrameCount), Color.White);


            if (CurrentFrame >= FrameCount - 1) CurrentFrame = 0;
            else CurrentFrame++;

            if (HoverText != null)
            {
                if (IsMouseHovering) Main.hoverItemName = HoverText;
            }
        }
    }
}
