using Terraria.ModLoader;

namespace MarvelTerrariaUniverse
{
    public class Keybinds : ModSystem
    {
        public static ModKeybind IronMan_ToggleFaceplate { get; private set; }
        public static ModKeybind IronMan_ToggleHelmet { get; private set; }
        public static ModKeybind IronMan_ToggleFlight { get; private set; }

        public override void Load()
        {
            IronMan_ToggleFaceplate = KeybindLoader.RegisterKeybind(Mod, "Iron Man - Toggle Faceplate", "M");
            IronMan_ToggleHelmet = KeybindLoader.RegisterKeybind(Mod, "Iron Man - Toggle Helmet", "Escape");
            IronMan_ToggleFlight = KeybindLoader.RegisterKeybind(Mod, "Iron Man - Toggle Flight", "F");
        }

        public override void Unload()
        {
            IronMan_ToggleFaceplate = null;
            IronMan_ToggleHelmet = null;
            IronMan_ToggleFlight = null;
        }
    }
}
