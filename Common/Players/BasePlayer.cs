using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace MarvelTerrariaUniverse.Common.Players;
public class BasePlayer : ModPlayer
{
    public Transformations transformation = Transformations.None;

    private static Dictionary<int, Func<Color>> BodyColor { get; set; }

    public static void RegisterData(int bodySlot, Func<Color> color)
    {
        if (!BodyColor.ContainsKey(bodySlot)) BodyColor.Add(bodySlot, color);
    }

    public override void Load()
    {
        BodyColor = new Dictionary<int, Func<Color>>();
    }

    public override void Unload()
    {
        BodyColor = null;
    }

    public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
    {
        if (!BodyColor.TryGetValue(Player.body, out Func<Color> color)) return;

        drawInfo.bodyGlowColor = color();
        drawInfo.armGlowColor = color();
    }
}

public class TestCommand : ModCommand
{
    public override CommandType Type => CommandType.Chat;
    public override string Command => "im";
    public override string Usage => "/im <mark>";

    public override void Action(CommandCaller caller, string input, string[] args)
    {
        var basePlayer = caller.Player.GetModPlayer<BasePlayer>();
        var ironManPlayer = caller.Player.GetModPlayer<IronManPlayer>();

        if (args.Length == 0)
        {
            basePlayer.transformation = Transformations.None;
            ironManPlayer.ResetEverything();
        }
        else
        {
            if (!int.TryParse(args[0], out int _))
            {
                Main.NewText("Mark required as integer");
                return;
            }

            basePlayer.transformation = Transformations.IronMan;
            ironManPlayer.Mark = int.Parse(args[0]);
        }

        Main.NewText($"{basePlayer.transformation} {ironManPlayer.Mark}");
    }
}