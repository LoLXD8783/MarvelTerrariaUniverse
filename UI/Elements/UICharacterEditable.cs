using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace MarvelTerrariaUniverse.UI.Elements
{
    public class UICharacterEditable : UICharacter
    {
        internal static Player Player;
        internal string Name;
        internal List<UICharacterEditable> List;

        public UICharacterEditable(Player player, string name, List<UICharacterEditable> list) : base(player)
        {
            Player = player;
            Name = name;
            List = list;
        }

        readonly Mod Mod = ModContent.GetInstance<MarvelTerrariaUniverse>();
        public void ChangeEquipTextures(string texture)
        {
            Player.head = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Head);
            Player.body = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Body);
            Player.legs = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Legs);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            Vector2 vector = dimensions.Position() + new Vector2(dimensions.Width * 0.5f - (Player.width >> 1), dimensions.Height * 0.5f - (Player.height >> 1));

            Main.PlayerRenderer.DrawPlayer(Main.Camera, Player, vector + Main.screenPosition, 0f, Vector2.Zero);

            if (Name == "IronMan") ChangeEquipTextures($"IronManMk{List.Count - List.IndexOf(this) + 1}");
        }
    }
}