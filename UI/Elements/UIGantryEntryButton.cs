using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace MarvelTerrariaUniverse.UI.Elements
{
    public class UIGantryEntryButton : UIElement
    {
        public bool Initialized = false;

        public readonly UIImage BorderGlow;
        public readonly UIImage BorderOverlay;
        public readonly UIImage BorderDefault;

        public UIElement ContentContainer;
        public UIText IndexText;
        public UIImage LockedIcon;

        public UICharacterEquipped Preview;

        public int Index;

        public bool Unlocked;

        public UIGantryEntryButton(int index)
        {
            Index = index;

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

            OnMouseOver += MouseOver;
            OnMouseOut += MouseOut;
            OnClick += UIGantryEntryButton_OnClick;
        }

        private void UIGantryEntryButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!(listeningElement as UIGantryEntryButton).Unlocked) return;

            IronManModPlayer ModPlayer = Main.LocalPlayer.GetModPlayer<IronManModPlayer>();

            Main.LocalPlayer.GetModPlayer<MTUModPlayer>().ResetEquipSlot();
            switch (Index)
            {
                case 1:
                    ModPlayer.TransformationActive_IronManMk1 = true;
                    break;
                case 2:
                    ModPlayer.TransformationActive_IronManMk2 = true;
                    break;
                case 3:
                    ModPlayer.TransformationActive_IronManMk3 = true;
                    break;
                case 4:
                    ModPlayer.TransformationActive_IronManMk4 = true;
                    break;
                case 5:
                    ModPlayer.TransformationActive_IronManMk5 = true;
                    break;
                case 6:
                    ModPlayer.TransformationActive_IronManMk6 = true;
                    break;
                case 7:
                    ModPlayer.TransformationActive_IronManMk7 = true;
                    break;
            }

            ModPlayer.GantryUIActive = false;
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

            if (!Initialized)
            {
                Preview = new(new(), $"IronManMk{Index}")
                {
                    HAlign = 0.5f,
                    VAlign = 0.5f
                };

                Initialized = true;
            }

            if (IsMouseHovering) Main.hoverItemName = Unlocked ? $"Iron Man Mk. {ToRoman(Index)}" : "???";

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

        public static string ToRoman(int number)
        {
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            return string.Empty;
        }
    }
}
