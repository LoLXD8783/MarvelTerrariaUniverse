using Terraria;
using Terraria.UI;

namespace MarvelTerrariaUniverse.UI
{
    public class IronManHUD : UIState
    {
        public static bool Visible => Main.LocalPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>().TransformationActive_IronMan && Main.LocalPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>().FaceplateOn && Main.LocalPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>().HelmetOn;
    }
}
