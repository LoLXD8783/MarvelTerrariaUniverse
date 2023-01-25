using MarvelTerrariaUniverse.Mounts;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarvelTerrariaUniverse.IMTransformations.TransformationStats;
using MarvelTerrariaUniverse.ModPlayers;
using Terraria.ModLoader;
using Terraria;
using MarvelTerrariaUniverse.Projectiles;

namespace MarvelTerrariaUniverse.IMTransformations.TransformationStats
{
    public class IronManStatsTemplate
    {

        public float powerRegenStat = 1;
        public float flightDrainStat = 1;

        public float repulsorDrainStat = 1;
        public int repulsorDamage = 1;
        public int repulsorCooldown = 1;

        public float unibeamDrainStat = 1;
        public int unibeamDamage = 1;
        public int unibeamCooldown = 1;

        public float flightSpeed = 1;
        public int Attack1 = ModContent.ProjectileType<IronManRepulsor>();
        public void Update(string ActiveTransformation)
        {
            if (ActiveTransformation == "IronManMk2")
            {
                IronManStatsTemplate mk2 = new Mk2Stats();
                SuitUpdateManagement(mk2);
            }
            else if (ActiveTransformation == "IronManMk3")
            {
                IronManStatsTemplate mk3 = new Mk3Stats();
                SuitUpdateManagement(mk3);
            }
            else if (ActiveTransformation == "IronManMk4")
            {
                IronManStatsTemplate mk4 = new Mk4Stats();
                SuitUpdateManagement(mk4);
            }
            else if (ActiveTransformation == "IronManMk5")
            {
                IronManStatsTemplate mk5 = new Mk5Stats();
                SuitUpdateManagement(mk5);
            }
            else if (ActiveTransformation == "IronManMk6")
            {
                IronManStatsTemplate mk6 = new Mk6Stats();
                SuitUpdateManagement(mk6);
            }
            else if (ActiveTransformation == "IronManMk7")
            {
                IronManStatsTemplate mk7 = new Mk7Stats();
                SuitUpdateManagement(mk7);
            }
        }
        
        public void SuitUpdateManagement(IronManStatsTemplate SuitModel)
        {
            powerRegenStat = SuitModel.powerRegenStat;
            flightDrainStat = SuitModel.flightDrainStat;
            repulsorCooldown = SuitModel.repulsorCooldown;
            repulsorDamage = SuitModel.repulsorDamage;
            repulsorDrainStat = SuitModel.repulsorDrainStat;
            unibeamCooldown = SuitModel.unibeamCooldown;
            unibeamDamage = SuitModel.unibeamDamage;
            unibeamDrainStat = SuitModel.unibeamDrainStat;
            flightSpeed = SuitModel.flightSpeed;
            Attack1 = SuitModel.Attack1;
        }
    }
}
