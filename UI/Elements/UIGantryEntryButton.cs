using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace MarvelTerrariaUniverse.UI.Elements
{
    public class UIGantryEntryButton : UIElement
    {
        MarvelTerrariaUniverseModSystem ModSystem => ModContent.GetInstance<MarvelTerrariaUniverseModSystem>();

        public bool Initialized = false;

        public readonly UIImage BorderGlow;
        public readonly UIImage BorderOverlay;
        public readonly UIImage BorderDefault;

        public UIElement ContentContainer;
        public UIText IndexText;
        public UIImage LockedIcon;
        public UIImage Preview;

        public int Index;
        public string Codename = "";

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
                Left = StyleDimension.FromPixels(7f),
                Top = StyleDimension.FromPixels(7f)
            };

            LockedIcon = new(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Locked"))
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

        public void SetCodename(string codename)
        {
            Codename = codename;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            if (Unlocked && !Initialized)
            {
                Preview = new(ModContent.Request<Texture2D>($"MarvelTerrariaUniverse/IMTransformations/TransformationTextures/IronManMk{Index}/IronManMk{Index}_Preview", ReLogic.Content.AssetRequestMode.ImmediateLoad))
                {
                    HAlign = 0.5f,
                    VAlign = 0.5f
                };

                Initialized = true;
            }

            if (IsMouseHovering) Main.hoverItemName = Unlocked ? $"Iron Man Mk. {ModSystem.ToRoman(Index)}{(Codename == "" ? "" : $"\n\"{Codename}\"")}" : "???";

            if (!Unlocked)
            {
                IndexText.Remove();
                ContentContainer.Append(LockedIcon);
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
