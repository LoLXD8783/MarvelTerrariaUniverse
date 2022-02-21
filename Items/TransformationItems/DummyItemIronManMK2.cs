using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaXMCU.Items.TransformationItems
{
    public class DummyItemIronManMK2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.Deprecated[Type] = true;

            SetupDrawing();
        }

        public override void Load()
        {
            Mod.AddEquipTexture(new EquipTexture(), this, EquipType.Head, "TerrariaXMCU/Assets/IronMan/IronManMK2_Head");
            Mod.AddEquipTexture(new EquipTexture(), this, EquipType.Body, "TerrariaXMCU/Assets/IronMan/IronManMK2_Body");
            Mod.AddEquipTexture(new EquipTexture(), this, EquipType.Legs, "TerrariaXMCU/Assets/IronMan/IronManMK2_Legs");
        }

        private void SetupDrawing()
        {
            int IronManMK2Head = Mod.GetEquipSlot(Name, EquipType.Head);
            int IronManMK2Body = Mod.GetEquipSlot(Name, EquipType.Body);
            int IronManMK2Legs = Mod.GetEquipSlot(Name, EquipType.Legs);


                ArmorIDs.Head.Sets.DrawHead[IronManMK2Head] = false;
                ArmorIDs.Body.Sets.HidesTopSkin[IronManMK2Body] = true;
                ArmorIDs.Body.Sets.HidesArms[IronManMK2Body] = true;
                ArmorIDs.Legs.Sets.HidesBottomSkin[IronManMK2Legs] = true;
        }
    }
}