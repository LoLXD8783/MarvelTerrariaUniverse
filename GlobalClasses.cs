using Terraria;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse
{
    public class MarvelTerrariaUniverseGlobalInfoDisplay : GlobalInfoDisplay
    {
        public override bool? Active(InfoDisplay currentDisplay)
        {
            if (Main.LocalPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>().TransformationActive_IronMan || Main.LocalPlayer.GetModPlayer<MarvelTerrariaUniverseModPlayer>().GantryUIActive) return false;
            else return base.Active(currentDisplay);
        }
    }

    public class MarvelTerrariaUniverseGlobalItem : GlobalItem
    {
        public override void UpdateInventory(Item item, Player player)
        {
            if (!player.GetModPlayer<MarvelTerrariaUniverseModPlayer>().TransformationActive_IronMan) base.UpdateInventory(item, player);
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!player.GetModPlayer<MarvelTerrariaUniverseModPlayer>().TransformationActive_IronMan) base.UpdateAccessory(item, player, hideVisual);
        }

        public override void UpdateEquip(Item item, Player player)
        {
            if (!player.GetModPlayer<MarvelTerrariaUniverseModPlayer>().TransformationActive_IronMan) base.UpdateEquip(item, player);
        }
    }
}