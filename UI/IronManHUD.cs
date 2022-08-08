using MarvelTerrariaUniverse.ModPlayers;
using MarvelTerrariaUniverse.UI.Elements;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace MarvelTerrariaUniverse.UI
{
    public class IronManHUD : UIState
    {
        public static bool Visible => Main.LocalPlayer.GetModPlayer<IronManModPlayer>().TransformationActive_IronMan && Main.LocalPlayer.GetModPlayer<IronManModPlayer>().FaceplateOn && Main.LocalPlayer.GetModPlayer<IronManModPlayer>().HelmetOn;

        public override void OnInitialize()
        {
/*            UIAnimatedImage StatBarsPanel = new(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/UI/Textures/IronManHUDStatBarsPanel", ReLogic.Content.AssetRequestMode.ImmediateLoad), 35)
            {
                HAlign = 0.98f,
                VAlign = 0.01f
            };

            Append(StatBarsPanel);*/
        }
    }
}
