using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using MarvelTerrariaUniverse.UI.Elements;

namespace MarvelTerrariaUniverse.UI
{
    public class GantryUI : UIState
    {
        public static bool Visible => Main.LocalPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>().GantryUIActive;

        UIPanel MainPanel;
        UIPanel SuitSelectionPanel;
        UIPanel SuitInfoPanel;

        UIGrid SuitButtonGrid;
        UIScrollbar SuitSelectionScrollbar;

        public override void OnInitialize()
        {
            MainPanel = new()
            {
                HAlign = 0.5f,
                VAlign = 0.5f,
                Width = StyleDimension.FromPercent(0.5f),
                Height = StyleDimension.FromPercent(0.5f)
            };

            MainPanel.SetPadding(0f);

            SuitSelectionPanel = new()
            {
                HAlign = 0f,
                Width = StyleDimension.FromPercent(0.5f),
                Height = StyleDimension.FromPercent(1f),
                PaddingRight = 0f
            };

            SuitInfoPanel = new()
            {
                HAlign = 1f,
                Width = StyleDimension.FromPercent(0.5f),
                Height = StyleDimension.FromPercent(1f),
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

            for (int i = 0; i < 85; i++)
            {
                UIPanel Panel = new()
                {
                    Width = StyleDimension.FromPixels(64f),
                    Height = StyleDimension.FromPixels(64f)
                };

                Panel.SetPadding(0f);
                Panel.OnClick += Panel_OnClick;

                SuitButtonGrid._items.Add(Panel);
                SuitButtonGrid._innerList.Append(Panel);
            }

            SuitSelectionScrollbar.SetView(100f, 1000f);
            SuitButtonGrid.SetScrollbar(SuitSelectionScrollbar);

            Append(MainPanel);

            MainPanel.Append(SuitSelectionPanel);
            MainPanel.Append(SuitInfoPanel);

            SuitSelectionPanel.Append(SuitButtonGrid);
            SuitSelectionPanel.Append(SuitSelectionScrollbar);
        }

        private void Panel_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.LocalPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>().GantryUIActive = false;
            Main.LocalPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>().TransformationActive_IronManMk2 = !Main.LocalPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>().TransformationActive_IronManMk2;
        }

        public override void Update(GameTime gameTime)
        {
            if (MainPanel.IsMouseHovering) Main.LocalPlayer.mouseInterface = true;
        }
    }
}
