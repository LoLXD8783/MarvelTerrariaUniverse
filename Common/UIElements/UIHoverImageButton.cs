using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace MarvelTerrariaUniverse.Common.UIElements;
internal class UIHoverImageButton : UIImageButton
{
    internal string hoverText { get; set; }

    public UIHoverImageButton(Asset<Texture2D> texture, string hoverText) : base(texture)
    {
        this.hoverText = hoverText;

        /*OnMouseOver += PlaySound;
        OnMouseOut += PlaySound;
        OnMouseDown += PlaySound;*/
    }

    private void PlaySound(UIMouseEvent evt, UIElement listeningElement)
    {
        SoundEngine.PlaySound(SoundID.MenuTick);
    }

    public void SetHoverText(string hoverText)
    {
        this.hoverText = hoverText;
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);

        if (IsMouseHovering) Main.hoverItemName = hoverText;
    }
}