using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.UI.Elements
{
    public class UICharacterEditable : UICharacter
    {
        public Player Player;

        public UICharacterEditable(Player player, bool animated = false, bool hasBackPanel = true, float characterScale = 1f) : base(player, animated, hasBackPanel, characterScale)
        {
            Player = player;
        }

        readonly Mod Mod = ModContent.GetInstance<MarvelTerrariaUniverse>();
        public void ChangeEquipTextures(string texture)
        {
            Player.head = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Head);
            Player.body = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Body);
            Player.legs = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Legs);
        }
    }
}
