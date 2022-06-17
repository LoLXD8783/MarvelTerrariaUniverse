using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
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

        public UIElement ContentContainer;
        public UIText IndexText;
        public UIImage LockedIcon;

        public UICharacterEquipped Preview;

        public string HoverText;
        public int Index;
        public string InternalName;

        public bool Unlocked;

        public UIGantryEntryButton(string hoverText, int index, string internalName) : base()
        {
            HoverText = hoverText;
            Index = index;
            InternalName = internalName;

            Height = StyleDimension.FromPixels(72f);
            Width = StyleDimension.FromPixels(72f);

            SetPadding(0f);

            ContentContainer = new()
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

            IndexText = new($"{Index}")
            {
                Left = StyleDimension.FromPixels(5f),
                Top = StyleDimension.FromPixels(5f)
            };

            LockedIcon = new(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Locked"))
            {
                HAlign = 0.5f,
                VAlign = 0.5f
            };

            Preview = new(new(), InternalName)
            {
                HAlign = 0.5f,
                VAlign = 0.5f
            };

            OnMouseOver += MouseOver;
            OnMouseOut += MouseOut;
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

            if (!Unlocked)
            {
                IndexText.Remove();
                ContentContainer.Append(LockedIcon);
                Preview.Remove();
            }
            else
            {
                ContentContainer.Append(IndexText);
                LockedIcon.Remove();
                ContentContainer.Append(Preview);
            }
        }

        public override int CompareTo(object obj)
        {
            UIGantryEntryButton other = obj as UIGantryEntryButton;
            return Index.CompareTo(other.Index);
        }
    }
}
