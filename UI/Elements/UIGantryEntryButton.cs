using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace MarvelTerrariaUniverse.UI.Elements
{
    public class UIGantryEntryButton : UIElement
    {
        public readonly UIImage BorderGlow;
        public readonly UIImage BorderOverlay;
        public readonly UIImage BorderDefault;

        public readonly List<UICharacterEditable> PreviewList;
        public string HoverText;

        public bool Unlocked;

        public UIGantryEntryButton(List<UICharacterEditable> list, string hoverText) : base()
        {
            PreviewList = list;
            HoverText = hoverText;

            Height = StyleDimension.FromPixels(72f);
            Width = StyleDimension.FromPixels(72f);

            SetPadding(0f);

            UIElement ContentContainer = new()
            {
                Width = StyleDimension.FromPixelsAndPercent(-4f, 1f),
                Height = StyleDimension.FromPixelsAndPercent(-4f, 1f),
                IgnoresMouseInteraction = true,
                OverflowHidden = true,
                HAlign = 0.5f,
                VAlign = 0.5f
            };

            ContentContainer.SetPadding(0f);
            ContentContainer.Append(new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Slot_Back"))
            {
                VAlign = 0.5f,
                HAlign = 0.5f
            });

            Append(ContentContainer);

            BorderGlow = new(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Slot_Selection"))
            {
                VAlign = 0.5f,
                HAlign = 0.5f,
                IgnoresMouseInteraction = true
            };

            BorderOverlay = new(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Slot_Overlay"))
            {
                VAlign = 0.5f,
                HAlign = 0.5f,
                IgnoresMouseInteraction = true,
                Color = Color.White * 0.6f
            };

            Append(BorderOverlay);

            UIImage PanelFront = new(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Slot_Front"))
            {
                VAlign = 0.5f,
                HAlign = 0.5f,
                IgnoresMouseInteraction = true
            };

            Append(PanelFront);

            BorderDefault = PanelFront;

            OnMouseOver += MouseOver;
            OnMouseOut += MouseOut;

            if (PreviewList.Count < 2)
            {
                UICharacterEditable SuitButtonPreview = new(new(), "IronMan", PreviewList)
                {
                    HAlign = 0.5f,
                    VAlign = 0.5f
                };

                PreviewList.Add(SuitButtonPreview);
                ContentContainer.Append(SuitButtonPreview);

                Unlocked = true;
            }
            else
            {
                UIImage LockedIcon = new(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Locked"))
                {
                    HAlign = 0.5f,
                    VAlign = 0.5f
                };

                ContentContainer.Append(LockedIcon);

                Unlocked = false;
            }
        }

        private void MouseOver(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            RemoveChild(BorderDefault);
            RemoveChild(BorderGlow);
            RemoveChild(BorderOverlay);
            Append(BorderDefault);
            Append(BorderGlow);
        }

        private void MouseOut(UIMouseEvent evt, UIElement listeningElement)
        {
            RemoveChild(BorderDefault);
            RemoveChild(BorderGlow);
            RemoveChild(BorderOverlay);
            Append(BorderOverlay);
            Append(BorderDefault);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            if (IsMouseHovering) Main.hoverItemName = Unlocked ? HoverText : "???";
        }
    }
}
