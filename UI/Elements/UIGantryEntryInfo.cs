using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace MarvelTerrariaUniverse.UI.Elements
{
    public class UIGantryEntryInfo : UIElement
    {
        public readonly UIList ElementList;

        public readonly UIPanel DescriptionTextContainer;
        public readonly UIText DescriptionText;

        public UIGantryEntryInfo()
        {
            ElementList = new()
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPercent(1f)
            };

            ElementList.SetPadding(0f);
            Append(ElementList);

            UIScrollbar ListScrollbar = new()
            {
                Width = StyleDimension.FromPixels(20f),
                Height = StyleDimension.FromPercent(0.825f),
                Left = StyleDimension.FromPercent(0.95f),
                Top = StyleDimension.FromPercent(0.1f)
            };

            ElementList.SetScrollbar(ListScrollbar);
            ElementList.ListPadding = 10;

            UIText TitleText = new("Iron Man Mk. III")
            {
                HAlign = 0.5f,
            };

            AddElementToList(TitleText);

            UIElement PreviewPanelContentContainer = new()
            {
                Width = StyleDimension.FromPixels(193f),
                Height = StyleDimension.FromPixels(112f),
                HAlign = 0.5f
            };

            PreviewPanelContentContainer.SetPadding(0f);
            AddElementToList(PreviewPanelContentContainer);

            UIElement PreviewPanelContainer = new()
            {
                Width = StyleDimension.FromPixelsAndPercent(-4f, 1f),
                Height = StyleDimension.FromPixelsAndPercent(-4f, 1f),
                IgnoresMouseInteraction = true,
                OverflowHidden = true,
                HAlign = 0.5f,
                VAlign = 0.5f
            };

            PreviewPanelContainer.SetPadding(0f);
            PreviewPanelContentContainer.Append(PreviewPanelContainer);

            PreviewPanelContainer.Append(new UIImageWithBorder(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/GantryEntryPreviewBG"), Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Portrait_Front"))
            {
                HAlign = 0.5f,
                VAlign = 0.5f,
                ScaleToFit = true,
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPercent(1f),
            });

            DescriptionTextContainer = new()
            {
                Width = StyleDimension.FromPercent(0.94f),
                BackgroundColor = new Color(43, 56, 101),
                BorderColor = new Color(43, 56, 101),
                HAlign = 0.5f
            };

            AddElementToList(DescriptionTextContainer);

            DescriptionText = new("The third Iron Man Armor designed and created by Tony Stark as the successor to the Mark II. The Mk III was designed with upgraded technology and improved features to surpass its predecessor's capabilities. It is also the first to adopt the classic red and gold color scheme.", 0.8f)
            {
                IsWrapped = true,
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPercent(1f),
                HAlign = 0.5f
            };

            DescriptionTextContainer.Append(DescriptionText);

            UIElement PositionButtonsContainer = new()
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPixels(28f)
            };

            AddElementToList(PositionButtonsContainer);

            UIHoverImageButton PositionBackButton = new(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Back"), "Preceded by: Mk. II")
            {
                HAlign = 0.05f
            };

            PositionBackButton.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Border"));
            PositionBackButton.SetVisibility(1f, 1f);
            PositionBackButton.SetSnapPoint("BackPage", 0);
            PositionButtonsContainer.Append(PositionBackButton);

            UIText PositionText = new("Chronology")
            {
                HAlign = 0.5f,
                VAlign = 0.5f
            };

            PositionButtonsContainer.Append(PositionText);

            UIHoverImageButton PositionForwardButton = new(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Forward"), "Succeeded by: Mk. IV")
            {
                HAlign = 0.95f
            };

            PositionForwardButton.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Border"));
            PositionForwardButton.SetVisibility(1f, 1f);
            PositionForwardButton.SetSnapPoint("ForwardPage", 0);
            PositionButtonsContainer.Append(PositionForwardButton);
        }

        public void AddElementToList(UIElement element)
        {
            UIHorizontalSeparator Separator = new()
            {
                Color = new Color(89, 116, 213, 255),
                Width = StyleDimension.FromPercent(1f)
            };

            ElementList.Add(element);
            ElementList.Add(Separator);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            DescriptionTextContainer.Height.Pixels = DescriptionText.GetOuterDimensions().Height;
        }
    }
}
