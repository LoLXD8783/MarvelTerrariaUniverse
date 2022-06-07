using Terraria;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse
{
    public class MarvelTerrariaUniverseModPlayer : ModPlayer
    {
        public bool GantryUIActive;

        public bool TransformationActive;
        public bool TransformationActive_IronMan;
        public bool TransformationActive_IronManMk2;
        public bool TransformationActive_IronManMk3;

        private void UseEquipSlot(string texture)
        {
            Player.head = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Head);
            Player.body = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Body);
            Player.legs = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Legs);
        }

        public override void FrameEffects()
        {
            if (TransformationActive_IronManMk2) UseEquipSlot("IronManMk2");
            if (TransformationActive_IronManMk3) UseEquipSlot("IronManMk3");
        }

        public override void PreUpdate()
        {
            TransformationActive = TransformationActive_IronManMk2 || TransformationActive_IronManMk3;
            TransformationActive_IronMan = TransformationActive_IronManMk2 || TransformationActive_IronManMk3;
        }
    }
}