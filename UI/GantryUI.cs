using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using MarvelTerrariaUniverse.UI.Elements;
using Terraria.Audio;
using Terraria.ID;

namespace MarvelTerrariaUniverse.UI
{
    public class GantryUI : UIState
    {
        public static bool Visible => Main.LocalPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>().GantryUIActive;

        UIPanel MainPanel;
        UITextPanel<string> BackButton;

        UIPanel SuitSelectionPanel;
        UITextPanel<string> RemoveSuitButton;
        UIPanel SuitInfoPanel;

        UIGrid SuitButtonGrid;
        UIScrollbar SuitSelectionScrollbar;

        readonly List<UIElement> SuitButtonPanels = new();
        readonly List<UIElement> SuitButtonPreviews = new();

        public override void OnInitialize()
        {
            MainPanel = new()
            {
                HAlign = 0.5f,
                VAlign = 0.65f,
                Width = StyleDimension.FromPercent(0.5f),
                Height = StyleDimension.FromPercent(0.5f),
                BackgroundColor = new Color(41, 53, 97)
            };

            MainPanel.SetPadding(0f);

            BackButton = new("Back", 0.7f, true)
            {
                Width = StyleDimension.FromPercent(0.35f),
                Height = StyleDimension.FromPixels(50f),
                VAlign = 0.9f,
                HAlign = 0.5f,
            };

            BackButton.OnMouseOver += Button_OnMouseOver;
            BackButton.OnMouseOut += Button_OnMouseOut;
            BackButton.OnMouseDown += BackButton_OnMouseDown;
            BackButton.SetSnapPoint("ExitButton", 0);

            SuitSelectionPanel = new()
            {
                HAlign = 0f,
                Width = StyleDimension.FromPercent(0.5f),
                Height = StyleDimension.FromPercent(0.85f),
                PaddingRight = 0f,
                PaddingBottom = 0f,
                BackgroundColor = new Color(41, 53, 97),
                BorderColor = Color.White * 0f
            };

            RemoveSuitButton = new("Remove Suit", 0.7f, true)
            {
                Width = StyleDimension.FromPercent(0.2f),
                Height = StyleDimension.FromPixels(50f),
                VAlign = 0.97f,
                HAlign = 0.2f,
            };

            RemoveSuitButton.OnMouseOver += Button_OnMouseOver;
            RemoveSuitButton.OnMouseOut += Button_OnMouseOut;
            RemoveSuitButton.OnMouseDown += RemoveSuitButton_OnMouseDown;
            RemoveSuitButton.SetSnapPoint("RemoveSuitButton", 0);

            SuitInfoPanel = new()
            {
                HAlign = 1f,
                Width = StyleDimension.FromPercent(0.5f),
                Height = StyleDimension.FromPercent(0.85f),
                BackgroundColor = Color.White * 0f,
                BorderColor = Color.White * 0f
            };

            SuitButtonGrid = new(6)
            {
                Width = StyleDimension.FromPixelsAndPercent(-25f, 1f),
                Height = StyleDimension.FromPercent(1f),
                ListPadding = 10f
            };

            SuitSelectionScrollbar = new()
            {
                Height = StyleDimension.FromPercent(1f),
                Left = StyleDimension.FromPixels(-4f),
                HAlign = 1f
            };

            for (int i = 0; i < /*2*/85; i++)
            {
                UIPanel SuitButtonPanel = new()
                {
                    Width = StyleDimension.FromPixels(64f),
                    Height = StyleDimension.FromPixels(64f)
                };

                SuitButtonPanels.Add(SuitButtonPanel);

                SuitButtonPanel.SetPadding(0f);
                SuitButtonPanel.OnMouseOver += Button_OnMouseOver;
                SuitButtonPanel.OnMouseOut += Button_OnMouseOut;
                SuitButtonPanel.OnClick += Panel_OnClick;

                SuitButtonGrid._items.Add(SuitButtonPanel);
                SuitButtonGrid._innerList.Append(SuitButtonPanel);

                UICharacterEditable SuitButtonPreview = new(new(), hasBackPanel: false)
                {
                    HAlign = 0.5f,
                    VAlign = 0.5f
                };

                SuitButtonPreviews.Add(SuitButtonPreview);
                SuitButtonPanel.Append(SuitButtonPreview);
            }

            SuitSelectionScrollbar.SetView(100f, 1000f);
            SuitButtonGrid.SetScrollbar(SuitSelectionScrollbar);

            Append(MainPanel);
            Append(BackButton);

            MainPanel.Append(SuitSelectionPanel);
            MainPanel.Append(SuitInfoPanel);

            SuitSelectionPanel.Append(SuitButtonGrid);
            SuitSelectionPanel.Append(SuitSelectionScrollbar);

            MainPanel.Append(RemoveSuitButton);
        }

        private void Button_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            ((UIPanel)listeningElement).BackgroundColor = new Color(73, 94, 171);
            ((UIPanel)listeningElement).BorderColor = Colors.FancyUIFatButtonMouseOver;
        }

        private void Button_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
        {
            ((UIPanel)listeningElement).BackgroundColor = new Color(63, 82, 151) * 0.8f;
            ((UIPanel)listeningElement).BorderColor = Color.Black;
        }

        private void RemoveSuitButton_OnMouseDown(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuClose);
            Main.LocalPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>().GantryUIActive = false;

            ResetSuits(Main.LocalPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>());
        }

        private void BackButton_OnMouseDown(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuClose);
            Main.LocalPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>().GantryUIActive = false;
        }

        private static void ResetSuits(MarvelTerrariaUniverseModPlayer ModPlayer)
        {
            ModPlayer.TransformationActive_IronManMk2 = ModPlayer.TransformationActive_IronManMk3 = false;
        }

        private void Panel_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            MarvelTerrariaUniverseModPlayer ModPlayer = Main.LocalPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>();

            ResetSuits(ModPlayer);

            switch (SuitButtonPanels.IndexOf(listeningElement))
            {
                case 0:
                    ModPlayer.TransformationActive_IronManMk2 = true;
                    break;
                case 1:
                    ModPlayer.TransformationActive_IronManMk3 = true;
                    break;
            }

            ModPlayer.GantryUIActive = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsMouseHovering) Main.LocalPlayer.mouseInterface = true;

            for (int i = 0; i < SuitButtonPreviews.Count; i++)
            {
                if (i == 0) ((UICharacterEditable)SuitButtonPreviews[i]).ChangeEquipTextures("IronManMk2");
                if (i == 1) ((UICharacterEditable)SuitButtonPreviews[i]).ChangeEquipTextures("IronManMk3");
            }
        }
    }
}
