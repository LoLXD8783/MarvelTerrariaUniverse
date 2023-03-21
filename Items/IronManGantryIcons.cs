using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Items
{
    public class MicroMissiles : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forearm-Mounted Micro-Missiles");
            Tooltip.SetDefault("Shoot micro-missiles that explode on contact");
        }
    }

    public class MicroGuns : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shoulder-Mounted Auto-Aim Guns");
            Tooltip.SetDefault("Shoot a barrage of bullets at nearby enemies");
        }
    }

    public class Flares : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Release a bunch of flares to distract enemies or reveal your location");
        }
    }

    public class LaserSystem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shine a hot laser to burn through anything in its path");
        }
    }
}
