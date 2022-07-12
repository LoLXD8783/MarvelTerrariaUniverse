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

        internal UserInterface IronManHUDUserInterface;
        internal IronManHUD IronManHUD = new();

        public override void Load()
        {
            if (!Main.dedServ)
            {
                GantryUserInterface = new UserInterface();
                GantryUserInterface.SetState(GantryUI);
                GantryUI.Activate();

                IronManHUDUserInterface = new UserInterface();
                IronManHUDUserInterface.SetState(IronManHUD);
                IronManHUD.Activate();
            }
        }

        private GameTime _lastUpdateUiGameTime;

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;

            if (GantryUI.Visible) GantryUserInterface.Update(gameTime);
            if (IronManHUD.Visible) IronManHUDUserInterface.Update(gameTime);
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

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer("MarvelTerrariaUniverse: IronManHUDUserInterface", delegate
                {
                    if (_lastUpdateUiGameTime != null && IronManHUD.Visible) IronManHUDUserInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);

                    return true;
                }, InterfaceScaleType.UI));
            }

            int hotbarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Hotbar"));
            GameInterfaceLayer hotbarLayer = layers[hotbarIndex];

            int barsIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            GameInterfaceLayer barsLayer = layers[barsIndex];

            if (!Main.gameMenu)
            {
                if (Main.LocalPlayer.GetModPlayer<IronManModPlayer>().TransformationActive_IronMan || Main.LocalPlayer.GetModPlayer<IronManModPlayer>().GantryUIActive)
                {
                    layers.Remove(hotbarLayer);
                    layers.Remove(barsLayer);
                }
                else
                {
                    layers.Insert(hotbarIndex, hotbarLayer);
                    layers.Insert(barsIndex, barsLayer);
                }
            }
        }
    }
}
