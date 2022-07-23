using MarvelTerrariaUniverse.ModPlayers;
using MarvelTerrariaUniverse.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;

namespace MarvelTerrariaUniverse.UI
{
    public class GantryUI : UIState
    {
        public static bool Visible => Main.LocalPlayer.GetModPlayer<IronManModPlayer>().GantryUIActive;

        MarvelTerrariaUniverseModSystem ModSystem => ModContent.GetInstance<MarvelTerrariaUniverseModSystem>();

        UIImageButton SearchButton;
        UISearchBar SearchBar;
        UIPanel SearchBarPanel;

        UIGrid SuitButtonGrid;
        List<UIGantryEntryButton> SuitButtons = new();

        string SearchString;

        bool ClickedSearchBar = false;
        bool ClickedSomething = false;

        UIGantryEntryButton SelectedOption = null;

        UIPanel EntryInfoPanel;
        UIElement PreviewContent;
        UIImage PreviewBorder;
        UIImage PreviewBackground;
        UIImage LockedIcon;
        UIText TitleText;
        UIImage PreviewImage;
        UIHorizontalSeparator Separator1;
        UIImageButton EquipSuitButton;
        UIHorizontalSeparator Separator2;
        UIElement DescriptionPanel;
        UIText DescriptionText;

        public override void OnInitialize()
        {
            #region Main Elements

            UIElement ContentArea = new()
            {
                Width = StyleDimension.FromPercent(0.875f),
                MaxWidth = StyleDimension.FromPixels(800f + (Utils.ToInt(value: true) * 100)),
                MinWidth = StyleDimension.FromPixels(600f + (Utils.ToInt(value: true) * 100)),
                Top = StyleDimension.FromPixels(220f),
                Height = StyleDimension.FromPixelsAndPercent(-220f, 1f),
                HAlign = 0.5f
            };

            Append(ContentArea);

            UITextPanel<LocalizedText> ExitButton = new(Language.GetText("UI.Back"), 0.7f, large: true)
            {
                Width = StyleDimension.FromPixelsAndPercent(-10f, 0.5f),
                Height = StyleDimension.FromPixels(50f),
                VAlign = 1f,
                HAlign = 0.5f,
                Top = StyleDimension.FromPixels(-25f)
            };

            ExitButton.OnMouseOver += FadedMouseOver;
            ExitButton.OnMouseOut += FadedMouseOut;
            ExitButton.OnMouseDown += Click_GoBack;
            ExitButton.SetSnapPoint("ExitButton", 0);
            ContentArea.Append(ExitButton);

            UIPanel MainPanel = new()
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPixelsAndPercent(-90f, 1f),
                BackgroundColor = new Color(33, 43, 79) * 0.8f
            };

            MainPanel.SetPadding(0f);
            ContentArea.Append(MainPanel);

            #endregion

            #region Suit Selection

            UIElement SuitSelectionContent = new()
            {
                Width = StyleDimension.FromPixels((72f * 4) + (15f * 3) + 78f),
                Height = StyleDimension.FromPercent(1f),
                HAlign = 0
            };

            SuitSelectionContent.SetPadding(12f);
            MainPanel.Append(SuitSelectionContent);

            #region Search Bar

            UIElement SearchBarSection = new()
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPixels(24f),
                VAlign = 0f,
            };

            SearchBarSection.SetPadding(0f);
            SuitSelectionContent.Append(SearchBarSection);

            SearchButton = new(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Search"))
            {
                VAlign = 0.5f,
            };

            SearchButton.OnClick += Click_SearchArea;
            SearchButton.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Search_Border"));
            SearchButton.SetVisibility(1f, 1f);
            SearchButton.SetSnapPoint("SearchButton", 0);
            SearchBarSection.Append(SearchButton);

            SearchBarPanel = new()
            {
                Width = StyleDimension.FromPixelsAndPercent(-SearchButton.Width.Pixels - 3f, 1f),
                Height = StyleDimension.FromPercent(1f),
                HAlign = 1f,
                VAlign = 0.5f,
                BackgroundColor = new Color(35, 40, 83),
                BorderColor = new Color(35, 40, 83)
            };

            SearchBarPanel.SetPadding(0f);
            SearchBarSection.Append(SearchBarPanel);

            SearchBar = new(Language.GetText("Number / Codename"), 0.8f)
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPercent(1f),
                HAlign = 0f,
                VAlign = 0.5f
            };

            SearchBar.OnContentsChanged += OnSearchContentsChanged;
            SearchBar.OnStartTakingInput += OnStartTakingInput;
            SearchBar.OnEndTakingInput += OnEndTakingInput;
            SearchBar.OnNeedingVirtualKeyboard += OpenVirtualKeyboardWhenNeeded;
            SearchBarPanel.Append(SearchBar);
            SearchBar.SetContents(null, forced: true);

            UIImageButton SearchCancelButton = new(Main.Assets.Request<Texture2D>("Images/UI/SearchCancel"))
            {
                HAlign = 1f,
                VAlign = 0.5f,
                Left = StyleDimension.FromPixels(-2f)
            };

            SearchCancelButton.OnMouseOver += SearchCancelButton_OnMouseOver;
            SearchCancelButton.OnClick += SearchCancelButton_OnClick;
            SearchBar.Append(SearchCancelButton);

            #endregion

            UIElement SuitSelectionGridContainer = new()
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPixelsAndPercent(-36f, 1f),
                Top = StyleDimension.FromPixels(36f)
            };

            SuitSelectionGridContainer.SetPadding(0f);
            SuitSelectionContent.Append(SuitSelectionGridContainer);

            UIPanel SuitButtonGridContainer = new()
            {
                Width = StyleDimension.FromPixels((72f * 4) + (15f * 3) + 24f),
                Height = StyleDimension.FromPercent(1f),
                Left = StyleDimension.FromPixels(30f),
                BorderColor = new Color(89, 116, 213, 255),
                BackgroundColor = new Color(73, 94, 171)
            };

            SuitSelectionGridContainer.Append(SuitButtonGridContainer);

            SuitButtonGrid = new()
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPercent(1f),
                ListPadding = 15f
            };

            SuitButtonGridContainer.Append(SuitButtonGrid);

            for (int i = 2; i <= 50; i++)
            {
                UIGantryEntryButton SuitButton = new(i);
                SuitButtonGrid.Add(SuitButton);
                SuitButtons.Add(SuitButton);
                SuitButton.OnClick += SuitButton_OnClick;

                if (i <= 7) SuitButton.Unlocked = true;

                switch (i)
                {
                    case 5:
                        SuitButton.SetCodename("Suitcase");
                        break;
                    case 25:
                        UIScrollbar SuitButtonGridScrollbar = new()
                        {
                            VAlign = 0.5f,
                            Height = StyleDimension.FromPercent(0.98f),
                        };
                        SuitButtonGrid.SetScrollbar(SuitButtonGridScrollbar);
                        SuitSelectionGridContainer.Append(SuitButtonGridScrollbar);
                        break;
                }
            }

            #endregion

            #region Suit Info

            #region Preview

            UIElement SuitInfoContent = new()
            {
                Width = StyleDimension.FromPixelsAndPercent(-((72f * 4) + (15f * 3) + 78f), 1f),
                Height = StyleDimension.FromPercent(1f),
                HAlign = 1f
            };

            MainPanel.Append(SuitInfoContent);

            EntryInfoPanel = new()
            {
                Width = StyleDimension.FromPixelsAndPercent(-24f, 1f),
                Height = StyleDimension.FromPixelsAndPercent(-24f, 1f),
                HAlign = 0.5f,
                VAlign = 0.5f,
                BorderColor = new Color(89, 116, 213, 255),
                BackgroundColor = new Color(73, 94, 171),
                PaddingLeft = 0f,
                PaddingRight = 0f
            };

            SuitInfoContent.Append(EntryInfoPanel);

            PreviewContent = new()
            {
                Width = StyleDimension.FromPixelsAndPercent(-24f, 1f),
                Height = StyleDimension.FromPixels(252f),
                HAlign = 0.5f
            };

            PreviewContent.SetPadding(0f);
            EntryInfoPanel.Append(PreviewContent);

            PreviewBorder = new(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/GantryEntryInfoPreviewBorder", ReLogic.Content.AssetRequestMode.ImmediateLoad))
            {
                HAlign = 0.5f,
                VAlign = 0.5f
            };

            PreviewBackground = new(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/GantryEntryInfoPreviewBackground", ReLogic.Content.AssetRequestMode.ImmediateLoad))
            {
                HAlign = 0.5f,
                VAlign = 0.5f
            };

            LockedIcon = new(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/GantryEntryLockedIcon", ReLogic.Content.AssetRequestMode.ImmediateLoad))
            {
                HAlign = 0.5f,
                VAlign = 0.5f
            };

            TitleText = new("")
            {
                Left = StyleDimension.FromPixels(15f),
                Top = StyleDimension.FromPixels(15f),
            };

            PreviewBackground.Append(TitleText);

            PreviewImage = new(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/TransformationTextures/IronManMk2/IronManMk2_Preview", ReLogic.Content.AssetRequestMode.ImmediateLoad))
            {
                ImageScale = 2f,
                HAlign = 0.5f,
                VAlign = 0.65f
            };

            PreviewBackground.Append(PreviewImage);

            #endregion

            Separator1 = new()
            {
                Color = new Color(89, 116, 213, 255),
                Width = StyleDimension.FromPercent(1f),
                Top = StyleDimension.FromPixels(264f)
            };

            EquipSuitButton = new(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/EquipSuitButton", ReLogic.Content.AssetRequestMode.ImmediateLoad))
            {
                HAlign = 0.5f,
                Top = StyleDimension.FromPixels(280f)
            };

            EquipSuitButton.OnClick += EquipSuitButton_OnClick;
            EquipSuitButton.SetHoverImage(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/EquipSuitButton_Hover", ReLogic.Content.AssetRequestMode.ImmediateLoad));
            EquipSuitButton.SetVisibility(1f, 1f);
            EquipSuitButton.SetSnapPoint("EquipSuitButton", 0);

            UIText EquipSuitButtonText = new("Equip Suit")
            {
                HAlign = 0.5f,
                VAlign = 0.5f
            };

            EquipSuitButton.Append(EquipSuitButtonText);

            Separator2 = new()
            {
                Color = new Color(89, 116, 213, 255),
                Width = StyleDimension.FromPercent(1f),
                Top = StyleDimension.FromPixels(332f)
            };

            #region Description

            DescriptionPanel = new()
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPixelsAndPercent(-344f, 1f),
                VAlign = 1f
            };

            UIPanel DescriptionTextPanel = new(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel"), null, 12, 7)
            {
                Width = StyleDimension.FromPercent(0.75f),
                Height = StyleDimension.FromPercent(1f),
                BackgroundColor = new Color(43, 56, 101),
                BorderColor = Color.Transparent,
                Left = new StyleDimension(15f, 0f),
            };

            DescriptionPanel.Append(DescriptionTextPanel);

            DescriptionText = new("", 0.8f)
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPercent(1f),
                IsWrapped = true
            };

            DescriptionTextPanel.Append(DescriptionText);

            #endregion

            #endregion
        }

        private void EquipSuitButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            IronManModPlayer ModPlayer = Main.LocalPlayer.GetModPlayer<IronManModPlayer>();

            switch (SelectedOption.Index)
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

        private void SuitButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if (SelectedOption != null && SelectedOption == listeningElement)
            {
                SelectedOption = null;
                RemoveEntryInfoContent();
            }
            else
            {
                SelectedOption = listeningElement as UIGantryEntryButton;

                if (SelectedOption.Unlocked)
                {
                    LockedIcon.Remove();
                    PreviewContent.Append(PreviewBorder);
                    PreviewContent.Append(PreviewBackground);
                    TitleText.SetText($"Iron Man Mk. {ModSystem.ToRoman(SelectedOption.Index)}{(SelectedOption.Codename == "" ? "" : $"\n\"{SelectedOption.Codename}\"")}");
                    PreviewImage.SetImage(ModContent.Request<Texture2D>($"MarvelTerrariaUniverse/TransformationTextures/IronManMk{SelectedOption.Index}/IronManMk{SelectedOption.Index}_Preview", ReLogic.Content.AssetRequestMode.ImmediateLoad));
                    EntryInfoPanel.Append(Separator1);
                    EntryInfoPanel.Append(EquipSuitButton);
                    EntryInfoPanel.Append(Separator2);
                    EntryInfoPanel.Append(DescriptionPanel);
                    DescriptionText.SetText(Language.GetTextValue($"Mods.MarvelTerrariaUniverse.GantryEntryDescription.{SelectedOption.Index}"));
                }
                else
                {
                    RemoveEntryInfoContent();
                    PreviewContent.Append(PreviewBorder);
                    PreviewContent.Append(LockedIcon);
                }
            }
        }

        private void RemoveEntryInfoContent()
        {
            PreviewBorder.Remove();
            PreviewBackground.Remove();
            LockedIcon.Remove();
            TitleText.SetText("");
            Separator1.Remove();
            EquipSuitButton.Remove();
            Separator2.Remove();
            DescriptionPanel.Remove();
            DescriptionText.SetText("");
        }

        private static void ExitUI()
        {
            SoundEngine.PlaySound(SoundID.MenuClose);
            Main.LocalPlayer.GetModPlayer<IronManModPlayer>().GantryUIActive = false;
        }

        private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            ((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
            ((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
        }

        private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
        {
            ((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
            ((UIPanel)evt.Target).BorderColor = Color.Black;
        }

        private void Click_GoBack(UIMouseEvent evt, UIElement listeningElement)
        {
            SelectedOption = null;
            RemoveEntryInfoContent();
            ExitUI();
        }

        private void Click_SearchArea(UIMouseEvent evt, UIElement listeningElement)
        {
            if (listeningElement == SearchBar || listeningElement == SearchButton || listeningElement == SearchBarPanel)
            {
                SearchBar.ToggleTakingText();

                ClickedSearchBar = true;
            }
        }

        private void OnSearchContentsChanged(string contents)
        {
            SearchString = contents;
        }

        private void OnStartTakingInput()
        {
            SearchBarPanel.BorderColor = Main.OurFavoriteColor;
        }

        private void OnEndTakingInput()
        {
            SearchBarPanel.BorderColor = new Color(35, 40, 83);
        }

        private void OpenVirtualKeyboardWhenNeeded()
        {
            int maxInputLength = 40;
            UIVirtualKeyboard uIVirtualKeyboard = new(Language.GetText("UI.PlayerNameSlot").Value, SearchString, OnFinishedSettingName, GoBackHere, 0, allowEmpty: true);
            uIVirtualKeyboard.SetMaxInputLength(maxInputLength);
            UserInterface.ActiveInstance.SetState(uIVirtualKeyboard);
        }

        private void OnFinishedSettingName(string name)
        {
            string contents = name.Trim();
            SearchBar.SetContents(contents);
            GoBackHere();
        }

        private void GoBackHere()
        {
            UserInterface.ActiveInstance.SetState(this);
            if (SearchBar.IsWritingText) SearchBar.ToggleTakingText();
        }

        private void SearchCancelButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if (SearchBar.HasContents)
            {
                SearchBar.SetContents(null, forced: true);
                SoundEngine.PlaySound(SoundID.MenuClose);
            }
            else SoundEngine.PlaySound(SoundID.MenuTick);

            SuitButtons.ForEach(item =>
            {
                if (!SuitButtonGrid._items.Any(i => i == item)) SuitButtonGrid.Add(item);
            });

            GoBackHere();
        }

        private void SearchCancelButton_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
        }

        private void AttemptStoppingUsingSearchbar()
        {
            ClickedSomething = true;
        }

        public override void Click(UIMouseEvent evt)
        {
            base.Click(evt);
            AttemptStoppingUsingSearchbar();
        }

        public override void Update(GameTime gameTime)
        {
            Main.playerInventory = false;

            if (IsMouseHovering)
            {
                PlayerInput.LockVanillaMouseScroll("MarvelTerrariaUniverse/UI/GantryUI");
                Main.LocalPlayer.mouseInterface = true;
            }

            if (ClickedSomething && !ClickedSearchBar && SearchBar.IsWritingText) SearchBar.ToggleTakingText();

            if (Main.keyState.IsKeyDown(Keys.Escape) && !Main.oldKeyState.IsKeyDown(Keys.Escape))
            {
                if (SearchBar.IsWritingText) GoBackHere();
                else
                {
                    SelectedOption = null;
                    RemoveEntryInfoContent();
                    ExitUI();
                }
            }

            if (SearchString != null)
            {
                SuitButtons.ForEach(item =>
                {
                    if (!item.Codename.ToLower().Contains(SearchString) && !item.Index.ToString().Contains(SearchString)) SuitButtonGrid.Remove(item);
                    else if (!SuitButtonGrid._items.Any(i => i == item)) SuitButtonGrid.Add(item);
                });
            }
        }
    }
}
