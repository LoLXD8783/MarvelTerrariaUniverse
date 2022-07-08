using MarvelTerrariaUniverse.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;

namespace MarvelTerrariaUniverse.UI
{
    public class GantryUI : UIState
    {
        public static bool Visible => Main.LocalPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>().GantryUIActive;

        private UISearchBar SearchBar;
        private UIPanel SearchBarPanel;
        private string SearchString;

        private bool ClickedSearchBar;
        private bool ClickedSomething;

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

            MainPanel.PaddingTop -= 4f;
            MainPanel.PaddingBottom -= 4f;
            ContentArea.Append(MainPanel);

            UIElement NavSection = new()
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPixels(24f),
                Top = StyleDimension.FromPixels(6f),
                VAlign = 0f,
            };

            NavSection.SetPadding(0f);
            MainPanel.Append(NavSection);

            UIElement MainContentContainer = new()
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPixelsAndPercent(-45f, 1f),
                Top = StyleDimension.FromPixels(40f)
            };

            MainContentContainer.SetPadding(0f);
            MainPanel.Append(MainContentContainer);

            UIElement SuitSelectionGridContainer = new()
            {
                Width = StyleDimension.FromPixels(650f),
                Height = StyleDimension.FromPixelsAndPercent(-8f, 1f),
                VAlign = 0.5f
            };

            SuitSelectionGridContainer.SetPadding(0f);
            MainContentContainer.Append(SuitSelectionGridContainer);

            UIPanel SuitInfoContainer = new()
            {
                Width = StyleDimension.FromPixelsAndPercent(-660f, 1f),
                Height = StyleDimension.FromPixelsAndPercent(-8f, 1f),
                VAlign = 0.5f,
                HAlign = 1f,
                BorderColor = new Color(89, 116, 213, 255),
                BackgroundColor = new Color(73, 94, 171)
            };

            SuitInfoContainer.SetPadding(0f);
            MainContentContainer.Append(SuitInfoContainer);

            #endregion

            #region NavBar Content

            UIImageButton SearchButton = new(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Search"))
            {
                VAlign = 0.5f
            };

            SearchButton.OnClick += Click_SearchArea;
            SearchButton.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Search_Border"));
            SearchButton.SetVisibility(1f, 1f);
            SearchButton.SetSnapPoint("SearchButton", 0);
            NavSection.Append(SearchButton);

            SearchBarPanel = new()
            {
                Left = StyleDimension.FromPixels(SearchButton.Width.Pixels + 3f),
                Width = StyleDimension.FromPixels(230 - SearchButton.Width.Pixels - 3f),
                Height = StyleDimension.FromPercent(1f),
                VAlign = 0.5f,
                BackgroundColor = new Color(35, 40, 83),
                BorderColor = new Color(35, 40, 83)
            };

            SearchBarPanel.SetPadding(0f);
            NavSection.Append(SearchBarPanel);

            SearchBar = new(Language.GetText("UI.PlayerNameSlot"), 0.8f)
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

            #region Suit Selection Grid

            UIGrid SuitButtonGrid = new()
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPercent(1f),
                ListPadding = 10f,
            };

            SuitSelectionGridContainer.Append(SuitButtonGrid);

            for (int i = 1; i <= 7; i++)
            {
                UIGantryEntryButton SuitButton = new(i);
                SuitButtonGrid.Add(SuitButton);

                if (i < 7) SuitButton.Unlocked = true;
            }

            #endregion

            #region Suit Info Content

            UIGantryEntryInfo test = new()
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPercent(1f),
                PaddingTop = 10f
            };

            SuitInfoContainer.Append(test);

            #endregion
        }

        private static void ExitUI()
        {
            SoundEngine.PlaySound(SoundID.MenuClose);
            Main.LocalPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>().GantryUIActive = false;
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
            ExitUI();
        }

        private void Click_SearchArea(UIMouseEvent evt, UIElement listeningElement)
        {
            if (listeningElement.Parent != SearchBarPanel)
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
            ClickedSomething = false;
            ClickedSearchBar = false;

            if (Main.keyState.IsKeyDown(Keys.Escape) && !Main.oldKeyState.IsKeyDown(Keys.Escape))
            {
                if (SearchBar.IsWritingText) GoBackHere();
                else ExitUI();
            }
        }
    }
}
