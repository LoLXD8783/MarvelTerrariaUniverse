using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse
{
    public class IronManMk2 : ModItem
    {
        public override string Texture => "MarvelTerrariaUniverse/Tiles/GantryTileItem";

        public override void SetStaticDefaults()
        {
            ItemID.Sets.Deprecated[Type] = true;

            ArmorIDs.Head.Sets.DrawHead[Mod.GetEquipSlot(Name, EquipType.Head)] = false;
            ArmorIDs.Body.Sets.HidesTopSkin[Mod.GetEquipSlot(Name, EquipType.Body)] = true;
            ArmorIDs.Body.Sets.HidesArms[Mod.GetEquipSlot(Name, EquipType.Body)] = true;
            ArmorIDs.Legs.Sets.HidesBottomSkin[Mod.GetEquipSlot(Name, EquipType.Legs)] = true;
        }

        public override void Load()
        {
            Mod.AddEquipTexture(new EquipTexture(), this, EquipType.Head, $"MarvelTerrariaUniverse/TransformationTextures/{Name}/{Name}_Head");
            Mod.AddEquipTexture(new EquipTexture(), this, EquipType.Body, $"MarvelTerrariaUniverse/TransformationTextures/{Name}/{Name}_Body");
            Mod.AddEquipTexture(new EquipTexture(), this, EquipType.Legs, $"MarvelTerrariaUniverse/TransformationTextures/{Name}/{Name}_Legs");
        }
    }
}