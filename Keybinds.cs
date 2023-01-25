using Terraria.ModLoader;

namespace MarvelTerrariaUniverse
{
    public class Keybinds : ModSystem
    {
        public static ModKeybind Reinstantiate { get; private set; }

        public static ModKeybind IronMan_ToggleSuit { get; private set; }
        public static ModKeybind IronMan_ToggleFaceplate { get; private set; }
        public static ModKeybind IronMan_ToggleHelmet { get; private set; }
        public static ModKeybind IronMan_ToggleFlight { get; private set; }
        public static ModKeybind IronMan_FireRepulsor { get; private set; }
        public static ModKeybind IronMan_FireUnibeam { get; private set; }
        
        public static ModKeybind IronMan_Arsenal1 { get; private set; }
        public static ModKeybind IronMan_Arsenal2 { get; private set; }
        public static ModKeybind IronMan_Arsenal3 { get; private set; }


        public override void Load()
        {
            Reinstantiate = KeybindLoader.RegisterKeybind(Mod, "Reinstantiate UI", "R");
            IronMan_ToggleSuit = KeybindLoader.RegisterKeybind(Mod, "Iron Man - Toggle Suit", "Escape");
            IronMan_ToggleFaceplate = KeybindLoader.RegisterKeybind(Mod, "Iron Man - Toggle Faceplate", "M");
            IronMan_ToggleHelmet = KeybindLoader.RegisterKeybind(Mod, "Iron Man - Toggle Helmet", "N");
            IronMan_ToggleFlight = KeybindLoader.RegisterKeybind(Mod, "Iron Man - Toggle Flight", "F");
            IronMan_FireRepulsor = KeybindLoader.RegisterKeybind(Mod, "Iron Man - Fire Repulsor", "Mouse1");
            IronMan_FireUnibeam = KeybindLoader.RegisterKeybind(Mod, "Iron Man - Fire Unibeam", "Mouse2");
            IronMan_Arsenal1 = KeybindLoader.RegisterKeybind(Mod, "Iron Man - Arsenal 1", "1");
            IronMan_Arsenal2 = KeybindLoader.RegisterKeybind(Mod, "Iron Man - Arsenal 2", "2");
            IronMan_Arsenal3 = KeybindLoader.RegisterKeybind(Mod, "Iron Man - Arsenal 3", "3");
        }

        public override void Unload()
        {
            Reinstantiate = null;

            IronMan_ToggleSuit = null;
            IronMan_ToggleFaceplate = null;
            IronMan_ToggleHelmet = null;
            IronMan_ToggleFlight = null;
            IronMan_FireRepulsor = null;
            IronMan_FireUnibeam = null;
            IronMan_Arsenal1 = null;
            IronMan_Arsenal2 = null;
            IronMan_Arsenal3 = null;
        }
    }
}
