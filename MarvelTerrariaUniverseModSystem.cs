using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using MarvelTerrariaUniverse.UI;

namespace MarvelTerrariaUniverse
{
    public class MarvelTerrariaUniverseModSystem : ModSystem
    {
        internal UserInterface GantryUserInterface;
        internal GantryUI GantryUI = new();

        public override void Load()
        {
            if (!Main.dedServ)
            {
                GantryUserInterface = new UserInterface();
                GantryUserInterface.SetState(GantryUI);
                GantryUI.Activate();
            }
        }

        private GameTime _lastUpdateUiGameTime;

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;

            if (GantryUI.Visible) GantryUserInterface.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer("MarvelTerrariaUniverse: GantryUserInterface", delegate
                {
                    if (_lastUpdateUiGameTime != null && GantryUI.Visible) GantryUserInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);

                    return true;
                }, InterfaceScaleType.UI));
            }
        }
    }
}
