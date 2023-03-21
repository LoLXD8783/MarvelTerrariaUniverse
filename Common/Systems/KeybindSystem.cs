using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Common.Systems;
public class KeybindSystem : ModSystem
{
    public static ModKeybind FlightToggle { get; set; }
    public static ModKeybind FaceplateToggle { get; set; }
    public static ModKeybind DropHelmet { get; set; }
    public static ModKeybind EjectSuit { get; set; }

    public override void Load()
    {
        FlightToggle = KeybindLoader.RegisterKeybind(Mod, "Flight Toggle", "F");
        FaceplateToggle = KeybindLoader.RegisterKeybind(Mod, "Faceplate Toggle", "G");
        DropHelmet = KeybindLoader.RegisterKeybind(Mod, "Drop Helmet", "H");
        EjectSuit = KeybindLoader.RegisterKeybind(Mod, "Eject Suit", "X");
    }

    public override void Unload()
    {
        FlightToggle = null;
        FaceplateToggle = null;
        DropHelmet = null;
        EjectSuit = null;
    }
}
