using Terraria.ModLoader;

namespace MarvelTerrariaUniverse
{
    public class Keybinds : ModSystem
    {
        public static ModKeybind IronMan_ToggleFaceplate { get; private set; }

        public override void Load()
        {
            IronMan_ToggleFaceplate = KeybindLoader.RegisterKeybind(Mod, "Iron Man - Remove / Replace Faceplate", "M");
        }

        public override void Unload()
        {
            IronMan_ToggleFaceplate = null;
        }
    }
}
