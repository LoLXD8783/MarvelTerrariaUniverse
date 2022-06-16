using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace MarvelTerrariaUniverse.UI.Elements
{
    public class UICharacterEquipped : UICharacter
    {
        internal static Player DrawnPlayer;
        internal string Name;

        public UICharacterEquipped(Player player, string name) : base(player)
        {
            DrawnPlayer = player;
            Name = name;
        }

        readonly Mod Mod = ModContent.GetInstance<MarvelTerrariaUniverse>();
        public void ChangeEquipTextures(string texture)
        {
            DrawnPlayer.head = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Head);
            DrawnPlayer.body = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Body);
            DrawnPlayer.legs = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Legs);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            Vector2 vector = dimensions.Position() + new Vector2(dimensions.Width * 0.5f - (DrawnPlayer.width >> 1), dimensions.Height * 0.5f - (DrawnPlayer.height >> 1));

            Main.PlayerRenderer.DrawPlayer(Main.Camera, DrawnPlayer, vector + Main.screenPosition, 0f, Vector2.Zero);

            ChangeEquipTextures(Name);
        }
    }
}