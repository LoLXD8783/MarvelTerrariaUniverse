using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.ModLoader.UI.Elements;
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
            #region General Elements

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

            #endregion

            #region Title

            UIText TitleText = new("Iron Man Mk. III")
            {
                HAlign = 0.5f,
            };

            AddElementToList(TitleText);

            #endregion

            #region Preview

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

            #endregion

            #region Description

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

            #endregion

            #region Stat Panels

            UIElement StatPanelsContainer = new()
            {
                Width = StyleDimension.FromPercent(0.92f),
                Height = StyleDimension.FromPixels(103f),
                HAlign = 0.5f
            };

            StatPanelsContainer.SetPadding(0f);

            AddElementToList(StatPanelsContainer);

            UIGrid StatPanelsGrid = new()
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPercent(1f),
                ListPadding = 5f,
            };

            StatPanelsContainer.Append(StatPanelsGrid);

            UIHoverImage StatPanel_SuitIntegrity = new(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Defense", ReLogic.Content.AssetRequestMode.ImmediateLoad), "Suit Integrity");
            StatPanelsGrid.Add(StatPanel_SuitIntegrity);

            UIHoverImage StatPanel_SuitPower = new(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_HP", ReLogic.Content.AssetRequestMode.ImmediateLoad), "Suit Power");
            StatPanelsGrid.Add(StatPanel_SuitPower);

            UIHoverImage StatPanel_RepulsorDamage = new(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/Stat_RepulsorDamage", ReLogic.Content.AssetRequestMode.ImmediateLoad), "Repulsor Damage");
            StatPanelsGrid.Add(StatPanel_RepulsorDamage);

            UIHoverImage StatPanel_UnibeamDamage = new(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/Stat_UnibeamDamage", ReLogic.Content.AssetRequestMode.ImmediateLoad), "Unibeam Damage");
            StatPanelsGrid.Add(StatPanel_UnibeamDamage);

            UIHoverImage StatPanel_FlightPower = new(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/Stat_FlightPower", ReLogic.Content.AssetRequestMode.ImmediateLoad), "Flight Power");
            StatPanelsGrid.Add(StatPanel_FlightPower);

            foreach (var item in StatPanelsGrid._items)
            {
                UIText StatValue = new("99")
                {
                    HAlign = 0.93f,
                    VAlign = 0.5f
                };

                item.Append(StatValue);
            }

            #endregion

            #region Chronology

            UIElement ChronologyButtonsContainer = new()
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPixels(28f)
            };

            AddElementToList(ChronologyButtonsContainer, false);

            UIHoverImageButton ChronologyBackButton = new(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Back"), "Preceded by: Mk. II")
            {
                HAlign = 0.05f
            };

            ChronologyBackButton.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Border"));
            ChronologyBackButton.SetVisibility(1f, 1f);
            ChronologyBackButton.SetSnapPoint("BackPage", 0);
            ChronologyButtonsContainer.Append(ChronologyBackButton);

            UIText ChronologyText = new("Chronology")
            {
                HAlign = 0.5f,
                VAlign = 0.5f
            };

            ChronologyButtonsContainer.Append(ChronologyText);

            UIHoverImageButton ChronologyForwardButton = new(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Forward"), "Succeeded by: Mk. IV")
            {
                HAlign = 0.95f
            };

            ChronologyForwardButton.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Border"));
            ChronologyForwardButton.SetVisibility(1f, 1f);
            ChronologyForwardButton.SetSnapPoint("ForwardPage", 0);
            ChronologyButtonsContainer.Append(ChronologyForwardButton);

            #endregion
        }

        public void AddElementToList(UIElement element, bool DrawSeparator = true)
        {
            ElementList.Add(element);

            if (DrawSeparator)
            {
                UIHorizontalSeparator Separator = new()
                {
                    Color = new Color(89, 116, 213, 255),
                    Width = StyleDimension.FromPercent(1f)
                };

                ElementList.Add(Separator);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            DescriptionTextContainer.Height.Pixels = DescriptionText.GetOuterDimensions().Height;
        }
    }
}
