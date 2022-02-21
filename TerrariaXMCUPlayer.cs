using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TerrariaXMCU.Items.TransformationItems;

namespace TerrariaXMCU
{
    public class TerrariaXMCUPlayer : ModPlayer
    {
        public bool Active_IronManMK2 = false;

        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if (Active_IronManMK2)
            {
                Player.cHead = Player.cBody = Player.cLegs = 0;
            }
        }

        public override void FrameEffects()
        {
            if (Active_IronManMK2)
            {
                var DummyItemIronManMK2 = ModContent.GetInstance<DummyItemIronManMK2>();

                Player.head = Mod.GetEquipSlot(DummyItemIronManMK2.Name, EquipType.Head);
                Player.body = Mod.GetEquipSlot(DummyItemIronManMK2.Name, EquipType.Body);
                Player.legs = Mod.GetEquipSlot(DummyItemIronManMK2.Name, EquipType.Legs);
            }
        }
    }
}