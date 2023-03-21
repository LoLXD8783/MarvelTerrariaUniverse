using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using MarvelTerrariaUniverse.UI;
using MarvelTerrariaUniverse.ModPlayers;

namespace MarvelTerrariaUniverse
{
    public class MarvelTerrariaUniverseModSystem : ModSystem
    {
        internal UserInterface GantryUserInterface;
        internal UserInterface IronManHUDUserInterface;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                GantryUI GantryUI = new();
                GantryUserInterface = new UserInterface();
                GantryUserInterface.SetState(GantryUI);
                GantryUI.Activate();

                IronManHUD IronManHUD = new();
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

            List<GameInterfaceLayer> moddedLayers = layers.FindAll(layer => !layer.Name.Contains("Vanilla:") && !layer.Name.Contains("MarvelTerrariaUniverse: "));

            if (!Main.gameMenu)
            {
                if (Main.LocalPlayer.GetModPlayer<IronManModPlayer>().TransformationActive_IronMan || Main.LocalPlayer.GetModPlayer<IronManModPlayer>().GantryUIActive)
                {
                    layers.Remove(hotbarLayer);
                    layers.Remove(barsLayer);
                    Main.mapEnabled = false;

                    moddedLayers.ForEach(layer => layers.Remove(layer));
                }
                else
                {
                    layers.Insert(hotbarIndex, hotbarLayer);
                    layers.Insert(barsIndex, barsLayer);
                    Main.mapEnabled = true;

                    moddedLayers.ForEach(layer => layers.Insert(moddedLayers.IndexOf(layer), layer));
                }
            }
        }

        public string ToRoman(int number)
        {
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            return string.Empty;
        }
    }
}
