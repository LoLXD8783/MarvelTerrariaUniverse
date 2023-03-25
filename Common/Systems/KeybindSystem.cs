using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Common.Systems;
public class KeybindSystem : ModSystem
{
    public static ModKeybind ToggleFlight { get; set; }
    public static ModKeybind ToggleFaceplate { get; set; }
    public static ModKeybind DropHelmet { get; set; }
    public static ModKeybind EjectSuit { get; set; }

    public override void Load()
    {
        ToggleFlight = KeybindLoader.RegisterKeybind(Mod, "Toggle Flight", "F");
        ToggleFaceplate = KeybindLoader.RegisterKeybind(Mod, "Toggle Faceplate", "G");
        DropHelmet = KeybindLoader.RegisterKeybind(Mod, "Drop Helmet", "H");
        EjectSuit = KeybindLoader.RegisterKeybind(Mod, "Eject Suit", "X");
    }

    public override void Unload()
    {
        ToggleFlight = null;
        ToggleFaceplate = null;
        DropHelmet = null;
        EjectSuit = null;
    }
}
