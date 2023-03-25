using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;

namespace MarvelTerrariaUniverse.Common.GantryUI;
public class GantryUISystem : ModSystem
{
    public static ModKeybind ResetUI { get; private set; } // FOR DEVELOPMENT PURPOSES ONLY

    public UserInterface GantryUserInterface;
    private GameTime lastUpdateUiGameTime;

    public override void Load()
    {
        ResetUI = KeybindLoader.RegisterKeybind(Mod, "Reset UI (FOR DEVELOPMENT PURPOSES ONLY)", "R");

        LoadUI();
    }

    public void LoadUI()
    {
        if (!Main.dedServ)
        {
            GantryUIState GantryUI = new();
            GantryUserInterface = new UserInterface();
            GantryUserInterface.SetState(GantryUI);
            GantryUI.Activate();
        }
    }

    public override void Unload()
    {
        ResetUI = null;
    }

    public override void UpdateUI(GameTime gameTime)
    {
        lastUpdateUiGameTime = gameTime;

        if (GantryUIState.Visible) GantryUserInterface.Update(gameTime);
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
        if (mouseTextIndex != -1)
        {
            layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer("MarvelTerrariaUniverse: Gantry User Interface", delegate
            {
                if (lastUpdateUiGameTime != null && GantryUIState.Visible) GantryUserInterface.Draw(Main.spriteBatch, lastUpdateUiGameTime);

                return true;
            }, InterfaceScaleType.UI));
        }
    }
}

public class TemporaryPlayer : ModPlayer
{
    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        var system = ModContent.GetInstance<GantryUISystem>();

        if (GantryUISystem.ResetUI.JustPressed && !Main.dedServ) system.LoadUI();
    }
}