using MarvelTerrariaUniverse.ModPlayers;
using Terraria;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse
{
    public class MarvelTerrariaUniverse : Mod
    {

    }

    public class ScoreCommand : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "e";

        public override string Usage => "/e InternalName";

        public override string Description => "Equip Iron Man / War Machine Suit";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            string InternalName = args[0];
            IronManModPlayer ModPlayer = Main.LocalPlayer.GetModPlayer<IronManModPlayer>();

            ModPlayer.ResetSuits_IronMan();
            switch (InternalName)
            {
                case "W1":
                    ModPlayer.TransformationActive_WarMachineMk1 = true;
                    break;
                case "I1":
                    ModPlayer.TransformationActive_IronManMk1 = true;
                    break;
                case "I2":
                    ModPlayer.TransformationActive_IronManMk2 = true;
                    break;
                case "I3":
                    ModPlayer.TransformationActive_IronManMk3 = true;
                    break;
                case "I4":
                    ModPlayer.TransformationActive_IronManMk4 = true;
                    break;
                case "I5":
                    ModPlayer.TransformationActive_IronManMk5 = true;
                    break;
                case "I6":
                    ModPlayer.TransformationActive_IronManMk6 = true;
                    break;
                case "I7":
                    ModPlayer.TransformationActive_IronManMk7 = true;
                    break;
            }
        }
    }
}