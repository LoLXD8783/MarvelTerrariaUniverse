using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using ReLogic.Content;
using MarvelTerrariaUniverse.Utilities.Extensions;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Common.UIElements;
internal class GantrySuitSelectButton : UIElement
{
    string texturePath = $"{MarvelTerrariaUniverse.AssetsFolder}/Textures/UI";

    public Asset<Texture2D> Texture { get; set; }
    public Asset<Texture2D> BorderTexture { get; set; }
    public Asset<Texture2D> HighlightTexture { get; set; }

    public UIHoverImageButton Panel { get; set; }
    public UIImage SelectedUIImage { get; set; }

    public bool Selected = false;

    internal string name;

    public GantrySuitSelectButton(int mark, string name = null) : base()
    {
        Width = StyleDimension.FromPixels(44f);
        Height = StyleDimension.FromPixels(80f);

        this.name = name;

        Texture = ModContent.Request<Texture2D>($"{texturePath}/Panel", AssetRequestMode.ImmediateLoad);
        BorderTexture = ModContent.Request<Texture2D>($"{texturePath}/PanelBorder", AssetRequestMode.ImmediateLoad);
        HighlightTexture = ModContent.Request<Texture2D>($"{texturePath}/PanelHighlight", AssetRequestMode.ImmediateLoad);

        SelectedUIImage = new UIImage(HighlightTexture).With(e =>
        {
            e.HAlign = 0.5f;
            e.VAlign = 0.5f;
        });

        Panel = this.AddElement(new UIHoverImageButton(Texture, $"Mark {mark}{(name == null ? "" : $" \"{name}\"")}").With(e =>
        {
            e.SetHoverImage(BorderTexture);
            e.SetVisibility(1f, 1f);
        }));

        OnClick += Click;
    }

    private void Click(UIMouseEvent evt, UIElement listeningElement)
    {
        Selected = !Selected;

        if (Selected) Panel.AddElement(SelectedUIImage);
        else SelectedUIImage.Remove();
    }
}
