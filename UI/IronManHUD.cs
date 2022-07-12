using Terraria;
using Terraria.UI;

namespace MarvelTerrariaUniverse.UI
{
    public class IronManHUD : UIState
    {
        public static bool Visible => Main.LocalPlayer.GetModPlayer<IronManModPlayer>().TransformationActive_IronMan && Main.LocalPlayer.GetModPlayer<IronManModPlayer>().FaceplateOn && Main.LocalPlayer.GetModPlayer<IronManModPlayer>().HelmetOn;
    }
}
