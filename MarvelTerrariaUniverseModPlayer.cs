using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse
{
    public class MarvelTerrariaUniverseModPlayer : ModPlayer
    {
        public Texture2D[] TransformationTextures;

        public bool TransformationActive;
        public List<bool> ActiveTransformations = new();
        public bool TransformationActive_IronManMk2;
        public bool TransformationActive_IronManMk3;

        public bool GantryUIActive = false;

        public override void ResetEffects()
        {
            GantryUIActive = false;
        }

        public override void Load()
        {
            ActiveTransformations.AddRange(new bool[] { TransformationActive_IronManMk2, TransformationActive_IronManMk3 });
        }
        public override void FrameEffects()
        {
            TransformationActive = ActiveTransformations.Any(item => item);

            if (TransformationActive_IronManMk2)
            {
                Main.playerInventory = false;


                Player.head = Mod.GetEquipSlot("IronManMk2", EquipType.Head);
                Player.body = Mod.GetEquipSlot("IronManMk2", EquipType.Body);
                Player.legs = Mod.GetEquipSlot("IronManMk2", EquipType.Legs);
            }
        }
    }
}