using MarvelTerrariaUniverse.Items;
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
        IronManModPlayer IronManModPlayer => Main.LocalPlayer.GetModPlayer<IronManModPlayer>();

        public static bool Visible => Main.LocalPlayer.GetModPlayer<IronManModPlayer>().GantryUIActive;

        public UIGantryEntryButton SelectedOption = null;

        UIImageButton SearchButton;
        UISearchBar SearchBar;
        UIPanel SearchBarPanel;

        UIGrid SuitButtonGrid;
        readonly List<UIGantryEntryButton> SuitButtons = new();

        string SearchString;

        bool ClickedSearchBar = false;
        bool ClickedSomething = false;

        UIElement SuitInfoContentContainer;
        UIPanel EmptySuitInfoPanel;

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

            SearchBarPanel = new()
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPercent(1f),
                HAlign = 1f,
                VAlign = 0.5f,
                BackgroundColor = new Color(35, 40, 83),
                BorderColor = new Color(35, 40, 83)
            };

            SearchBarPanel.SetPadding(0f);
            SearchBarSection.Append(SearchBarPanel);

            SearchBar = new(Language.GetText("Mods.MarvelTerrariaUniverse.IronMan.GantrySearchBarText"), 0.8f)
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPercent(1f),
                HAlign = 0f,
                VAlign = 0.5f
            };

            SearchBar.OnClick += Click_SearchArea;
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

            UIScrollbar SuitButtonGridScrollbar = new()
            {
                HAlign = 1f,
                VAlign = 0.5f,
                Height = StyleDimension.FromPercent(0.98f),
            };
            SuitButtonGrid.SetScrollbar(SuitButtonGridScrollbar);
            SuitSelectionGridContainer.Append(SuitButtonGridScrollbar);

            for (int i = 2; i <= 50; i++)
            {
                string AliasKey = $"Mods.MarvelTerrariaUniverse.IronMan.{i}.Alias";

                UIGantryEntryButton SuitButton = new(i);
                SuitButtonGrid.Add(SuitButton);
                SuitButtons.Add(SuitButton);
                SuitButton.OnClick += SuitButton_OnClick;

                if (i <= 7) SuitButton.Unlocked = true;

                if (Language.Exists(AliasKey)) SuitButton.SetCodename(Language.GetTextValue(AliasKey));
            }

            #endregion

            #region Suit Info

            SuitInfoContentContainer = new()
            {
                Width = StyleDimension.FromPixelsAndPercent(-((72f * 4) + (15f * 3) + 78f), 1f),
                Height = StyleDimension.FromPercent(1f),
                HAlign = 1f
            };

            SuitInfoContentContainer.SetPadding(12f);

            MainPanel.Append(SuitInfoContentContainer);

            EmptySuitInfoPanel = new()
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPercent(1f),
                BorderColor = new Color(89, 116, 213, 255),
                BackgroundColor = new Color(73, 94, 171)
            };

            SuitInfoContentContainer.Append(EmptySuitInfoPanel);

            #endregion
        }

        private void SuitButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if (SelectedOption != null && SelectedOption == listeningElement)
            {
                SelectedOption = null;
                SuitInfoContentContainer.RemoveAllChildren();
                SuitInfoContentContainer.Append(EmptySuitInfoPanel);
            }
            else
            {
                SelectedOption = listeningElement as UIGantryEntryButton;
                string AliasKey = $"Mods.MarvelTerrariaUniverse.IronMan.{SelectedOption.Index}.Alias";

                UIGantryEntryInfoPanel SelectedOptionInfo = new(SelectedOption.Index, 5, 5, Language.Exists(AliasKey) ? Language.GetTextValue(AliasKey) : null, !SelectedOption.Unlocked);

                switch (SelectedOption.Index)
                {
                    case 2:
                        SelectedOptionInfo.Weapons = new();
                        break;
                    case 3:
                        SelectedOptionInfo.Weapons = new() { ModContent.ItemType<MicroMissiles>(), ModContent.ItemType<MicroGuns>(), ModContent.ItemType<Flares>() };
                        break;
                    case 4:
                        SelectedOptionInfo.Weapons = new() { ModContent.ItemType<MicroMissiles>(), ModContent.ItemType<MicroGuns>(), ModContent.ItemType<Flares>() };
                        break;
                    case 5:
                        SelectedOptionInfo.Weapons = new();
                        break;
                    case 6:
                        SelectedOptionInfo.Weapons = new() { ModContent.ItemType<MicroMissiles>(), ModContent.ItemType<MicroGuns>(), ModContent.ItemType<Flares>(), ModContent.ItemType<LaserSystem>() };
                        break;
                    case 7:
                        SelectedOptionInfo.Weapons = new() { ModContent.ItemType<MicroMissiles>(), ModContent.ItemType<Flares>(), ModContent.ItemType<LaserSystem>() };
                        break;
                }

                SelectedOption.Initialize();
                SuitInfoContentContainer.Append(SelectedOptionInfo);
                EmptySuitInfoPanel.Remove();
            }
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
            SuitInfoContentContainer.RemoveAllChildren();
            SuitInfoContentContainer.Append(EmptySuitInfoPanel);
            SoundEngine.PlaySound(SoundID.MenuClose);
            IronManModPlayer.GantryUIActive = false;
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
                    SuitInfoContentContainer.RemoveAllChildren();
                    SuitInfoContentContainer.Append(EmptySuitInfoPanel);
                    SoundEngine.PlaySound(SoundID.MenuClose);
                    IronManModPlayer.GantryUIActive = false;
                }
            }

            if (SearchString != null)
            {
                SuitButtons.ForEach(item =>
                {
                    if (SearchString == "")
                    {
                        if (!SuitButtonGrid._items.Any(i => i == item)) SuitButtonGrid.Add(item);
                    }
                    else
                    {
                        if ((!item.Codename.ToLower().Contains(SearchString) && !item.Index.ToString().Contains(SearchString)) || !item.Unlocked) SuitButtonGrid.Remove(item);
                        else if (!SuitButtonGrid._items.Any(i => i == item)) SuitButtonGrid.Add(item);
                    }
                });
            }
        }
    }
}
