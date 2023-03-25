using Terraria;
using MarvelTerrariaUniverse.Common.UIElements;
using MarvelTerrariaUniverse.Utilities.Extensions;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Terraria.GameInput;

namespace MarvelTerrariaUniverse.Common.GantryUI;
public class GantryUIState : UIState
{
    public static bool Visible { get; set; }

    public Vector2 TileWorldPosition { get; set; }
    public Vector2 UIScreenPosition { get; set; }
    public Vector2 UISize { get; set; }

    public UIElement ContentContainer { get; set; }

    public override void OnInitialize()
    {
        // ELEMENT ATTRIBUTES SHOULD BE SORTED BY TYPE IN THIS ORDER:
        // - POSITIONING (Top, HAlign, etc.)
        // - DISPLAY & SIZING (Width, PaddingLeft, etc.)
        // - COLORS (BackgroundColor, BorderColor, etc.)
        // - EVERYTHING ELSE

        var tile = Main.LocalPlayer.Bottom.ToTileCoordinates();
        var tileCoords = new Vector2(tile.X - Framing.GetTileSafely(tile).TileFrameX / 18, tile.Y);

        TileWorldPosition = tileCoords * 16;

        UpdateUIDisplay();

        ContentContainer = this.AddElement(new UIElement().With(e =>
        {
            e.Left = StyleDimension.FromPixels(UIScreenPosition.X);
            e.Top = StyleDimension.FromPixels(UIScreenPosition.Y + 16f);

            e.Width = StyleDimension.FromPixels(UISize.X);
            e.Height = StyleDimension.FromPixels(UISize.Y);
        }));

        ContentContainer.AddElement(new GantrySuitSelectButton(1, "Gay").With(e =>
        {
            e.HAlign = 0.5f;
        }));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        UpdateUIDisplay();

        ContentContainer.Left = StyleDimension.FromPixels(UIScreenPosition.X);
        ContentContainer.Top = StyleDimension.FromPixels(UIScreenPosition.Y);

        ContentContainer.Width = StyleDimension.FromPixels(UISize.X);
        ContentContainer.Height = StyleDimension.FromPixels(UISize.Y);

        ContentContainer.Recalculate();
    }

    public void UpdateUIDisplay()
    {
        UIScreenPosition = new((TileWorldPosition.X - Main.screenPosition.X), (TileWorldPosition.Y - Main.screenPosition.Y + 16f));
        UISize = new(4 * 16f, 80f);
    }
}