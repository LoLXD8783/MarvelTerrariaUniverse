using MarvelTerrariaUniverse.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace MarvelTerrariaUniverse.UI.Elements
{
    public class UIGantryEntryInfoPanel : UIPanel
    {
        MarvelTerrariaUniverseModSystem ModSystem = ModContent.GetInstance<MarvelTerrariaUniverseModSystem>();
        IronManModPlayer ModPlayer = Main.LocalPlayer.GetModPlayer<IronManModPlayer>();

        int Index;
        int SuitPowerRating;
        int SuitIntegrityRating;
        string Alias;
        public bool Locked;

        public readonly UIList ElementList;

        UIPanel DescriptionTextPanel;
        UIText DescriptionText;

        private bool DescriptionPanelSizeFixed = false;

        public List<string> Weapons = null;
        public List<string> Strengths = null;
        public List<string> Weaknesses = null;

        public UIGantryEntryInfoPanel(int index, int suitPowerRating, int suitIntegrityRating, string alias = null, bool locked = true)
        {
            Index = index;
            SuitPowerRating = suitPowerRating;
            SuitIntegrityRating = suitIntegrityRating;
            Alias = alias;
            Locked = locked;

            Width = StyleDimension.FromPercent(1f);
            Height = StyleDimension.FromPercent(1f);
            HAlign = 0.5f;
            VAlign = 0.5f;
            BorderColor = new Color(89, 116, 213, 255);
            BackgroundColor = new Color(73, 94, 171);
            PaddingLeft = 0f;
            PaddingRight = 0f;

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

            #region Preview

            UIElement PreviewContent = new()
            {
                Width = StyleDimension.FromPixelsAndPercent(-24f, 1f),
                Height = StyleDimension.FromPixels(252f),
                HAlign = 0.5f
            };

            PreviewContent.SetPadding(0f);
            AddElementToList(PreviewContent);

            UIImage PreviewBorder = new(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/GantryEntryInfo/GantryEntryInfoPreviewBorder", ReLogic.Content.AssetRequestMode.ImmediateLoad))
            {
                HAlign = 0.5f,
                VAlign = 0.5f
            };

            PreviewContent.Append(PreviewBorder);

            UIImage LockedIcon = new(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/GantryEntryInfo/GantryEntryLockedIcon", ReLogic.Content.AssetRequestMode.ImmediateLoad))
            {
                HAlign = 0.5f,
                VAlign = 0.5f
            };

            if (Locked)
            {
                PreviewBorder.Append(LockedIcon);
                return;
            }

            UIImage PreviewBackground = new(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/GantryEntryInfo/GantryEntryInfoPreviewBackground", ReLogic.Content.AssetRequestMode.ImmediateLoad))
            {
                HAlign = 0.5f,
                VAlign = 0.5f
            };

            PreviewContent.Append(PreviewBackground);

            UIText TitleText = new($"Iron Man Mk. {ModSystem.ToRoman(Index)}")
            {
                Left = StyleDimension.FromPixels(15f),
                Top = StyleDimension.FromPixels(15f),
            };

            if (Alias != null) TitleText.SetText($"Iron Man Mk. {ModSystem.ToRoman(Index)}\n\"{Alias}\"");

            PreviewBackground.Append(TitleText);

            UIImage PreviewImage = new(ModContent.Request<Texture2D>($"MarvelTerrariaUniverse/TransformationTextures/IronManMk{Index}/IronManMk{Index}_Preview", ReLogic.Content.AssetRequestMode.ImmediateLoad))
            {
                ImageScale = 2f,
                HAlign = 0.5f,
                VAlign = 0.65f
            };

            PreviewBackground.Append(PreviewImage);

            UIElement SuitStatsPanel = new()
            {
                Width = StyleDimension.FromPixels(132f),
                Height = StyleDimension.FromPixels(54f),
                Left = StyleDimension.FromPixelsAndPercent(-147f, 1f),
                Top = StyleDimension.FromPixels(15f)
            };

            PreviewBackground.Append(SuitStatsPanel);

            UIHoverPanel SuitPowerStatPanel = new("Suit Power")
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPixels(24f),
                VAlign = 0f,
                BackgroundColor = Color.Transparent,
                BorderColor = Color.Transparent
            };

            SuitPowerStatPanel.SetPadding(0f);
            SuitStatsPanel.Append(SuitPowerStatPanel);

            int EmptySuitPowerStatIconsRequired = (10 - (SuitPowerRating % 2 == 0 ? SuitPowerRating : SuitPowerRating + 1)) / 2;
            while (SuitPowerRating > 0)
            {
                if (SuitPowerRating > 1)
                {
                    SuitPowerRating -= 2;

                    SuitPowerStatPanel.Append(new UIImage(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/GantryEntryInfo/SuitPowerStat_Full", ReLogic.Content.AssetRequestMode.ImmediateLoad))
                    {
                        Left = StyleDimension.FromPixels(3f + (24f * SuitPowerStatPanel.Children.Count()) + (3f * (SuitPowerStatPanel.Children.Count() - 1)))
                    });
                }
                else
                {
                    SuitPowerRating--;

                    SuitPowerStatPanel.Append(new UIImage(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/GantryEntryInfo/SuitPowerStat_Half", ReLogic.Content.AssetRequestMode.ImmediateLoad))
                    {
                        Left = StyleDimension.FromPixels(3f + (24f * SuitPowerStatPanel.Children.Count()) + (3f * (SuitPowerStatPanel.Children.Count() - 1)))
                    });
                }
            }

            for (int i = 0; i < EmptySuitPowerStatIconsRequired; i++)
            {
                SuitPowerStatPanel.Append(new UIImage(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/GantryEntryInfo/SuitPowerStat_Empty", ReLogic.Content.AssetRequestMode.ImmediateLoad))
                {
                    Left = StyleDimension.FromPixels(3f + (24f * SuitPowerStatPanel.Children.Count()) + (3f * (SuitPowerStatPanel.Children.Count() - 1)))
                });
            }

            UIHoverPanel SuitIntegrityStatPanel = new("Suit Integrity")
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPixels(24f),
                VAlign = 1f,
                BackgroundColor = Color.Transparent,
                BorderColor = Color.Transparent
            };

            SuitIntegrityStatPanel.SetPadding(0f);
            SuitStatsPanel.Append(SuitIntegrityStatPanel);

            int EmptySuitIntegrityStatIconsRequired = (10 - (SuitIntegrityRating % 2 == 0 ? SuitIntegrityRating : SuitIntegrityRating + 1)) / 2;
            while (SuitIntegrityRating > 0)
            {
                if (SuitIntegrityRating > 1)
                {
                    SuitIntegrityRating -= 2;

                    SuitIntegrityStatPanel.Append(new UIImage(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/GantryEntryInfo/SuitIntegrityStat_Full", ReLogic.Content.AssetRequestMode.ImmediateLoad))
                    {
                        Left = StyleDimension.FromPixels(3f + (24f * SuitIntegrityStatPanel.Children.Count()) + (3f * (SuitIntegrityStatPanel.Children.Count() - 1)))
                    });
                }
                else
                {
                    SuitIntegrityRating--;

                    SuitIntegrityStatPanel.Append(new UIImage(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/GantryEntryInfo/SuitIntegrityStat_Half", ReLogic.Content.AssetRequestMode.ImmediateLoad))
                    {
                        Left = StyleDimension.FromPixels(3f + (24f * SuitIntegrityStatPanel.Children.Count()) + (3f * (SuitIntegrityStatPanel.Children.Count() - 1)))
                    });
                }
            }

            for (int i = 0; i < EmptySuitIntegrityStatIconsRequired; i++)
            {
                SuitIntegrityStatPanel.Append(new UIImage(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/GantryEntryInfo/SuitIntegrityStat_Empty", ReLogic.Content.AssetRequestMode.ImmediateLoad))
                {
                    Left = StyleDimension.FromPixels(3f + (24f * SuitIntegrityStatPanel.Children.Count()) + (3f * (SuitIntegrityStatPanel.Children.Count() - 1)))
                });
            }

            #endregion

            UIImageButton EquipSuitButton = new(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/GantryEntryInfo/EquipSuitButton", ReLogic.Content.AssetRequestMode.ImmediateLoad))
            {
                HAlign = 0.5f,
            };

            AddElementToList(EquipSuitButton);

            EquipSuitButton.OnClick += EquipSuitButton_OnClick;
            EquipSuitButton.SetHoverImage(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/GantryEntryInfo/EquipSuitButton_Hover", ReLogic.Content.AssetRequestMode.ImmediateLoad));
            EquipSuitButton.SetVisibility(1f, 1f);
            EquipSuitButton.SetSnapPoint("EquipSuitButton", 0);

            UIText EquipSuitButtonText = new("Equip Suit")
            {
                HAlign = 0.5f,
                VAlign = 0.5f
            };

            EquipSuitButton.Append(EquipSuitButtonText);

            #region Description

            DescriptionTextPanel = new(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel", ReLogic.Content.AssetRequestMode.ImmediateLoad), null, 12, 7)
            {
                Width = StyleDimension.FromPixelsAndPercent(-30f, 1f),
                Height = StyleDimension.FromPercent(1f),
                BackgroundColor = new Color(43, 56, 101),
                BorderColor = Color.Transparent,
                HAlign = 0.5f
            };

            AddElementToList(DescriptionTextPanel);

            DescriptionText = new(Language.GetTextValue($"Mods.MarvelTerrariaUniverse.GantryEntryDescription.{Index}"), 0.9f)
            {
                Width = StyleDimension.FromPercent(1f),
                IsWrapped = true
            };

            DescriptionTextPanel.Append(DescriptionText);

            #endregion
        }

        private void EquipSuitButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            switch (Index)
            {
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

        public void FixDescriptionPanelHeight()
        {
            DescriptionTextPanel.Height.Set(DescriptionText.GetOuterDimensions().Height, 0f);
        }

        bool GridDrawn = false;
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Locked)
            {
                DescriptionTextPanel.Height.Set(DescriptionText.GetOuterDimensions().Height, 0f);
                DescriptionPanelSizeFixed = true;
                base.Draw(spriteBatch);
            }
            else base.Draw(spriteBatch);

            if (!GridDrawn)
            {
                if (Weapons.Count > 0)
                {
                    UIItemRow WeaponsRow = new("Weapons", new() { Main.rand.Next(5124) + 1, Main.rand.Next(5124) + 1, Main.rand.Next(5124) + 1, Main.rand.Next(5124) + 1, Main.rand.Next(5124) + 1, Main.rand.Next(5124) + 1 })
                    {
                        BackgroundColor = Color.DarkRed * 0.6f,
                        BorderColor = Color.Transparent,
                        Width = StyleDimension.FromPixelsAndPercent(-30f, 1f),
                        HAlign = 0.5f
                    };

                    AddElementToList(WeaponsRow, false);
                }

                if (Strengths.Count > 0)
                {
                    UIItemRow StrengthsRow = new("Strengths", new() { Main.rand.Next(5124) + 1, Main.rand.Next(5124) + 1, Main.rand.Next(5124) + 1, Main.rand.Next(5124) + 1, Main.rand.Next(5124) + 1, Main.rand.Next(5124) + 1 })
                    {
                        BackgroundColor = Color.ForestGreen * 0.6f,
                        BorderColor = Color.Transparent,
                        Width = StyleDimension.FromPixelsAndPercent(-30f, 1f),
                        HAlign = 0.5f
                    };

                    AddElementToList(StrengthsRow, false);
                }

                if (Weaknesses.Count > 0)
                {
                    UIItemRow WeaknessesRow = new("Weaknesses", new() { Main.rand.Next(5124) + 1, Main.rand.Next(5124) + 1, Main.rand.Next(5124) + 1, Main.rand.Next(5124) + 1, Main.rand.Next(5124) + 1, Main.rand.Next(5124) + 1 })
                    {
                        BackgroundColor = Color.LightGoldenrodYellow * 0.6f,
                        BorderColor = Color.Transparent,
                        Width = StyleDimension.FromPixelsAndPercent(-30f, 1f),
                        HAlign = 0.5f
                    };

                    AddElementToList(WeaknessesRow, false);
                }

                UIHorizontalSeparator Separator = new()
                {
                    Color = new Color(89, 116, 213, 255),
                    Width = StyleDimension.FromPercent(1f)
                };

                ElementList.Add(Separator);

                GridDrawn = true;
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (DescriptionPanelSizeFixed || Locked) base.DrawSelf(spriteBatch);
        }
    }
}
