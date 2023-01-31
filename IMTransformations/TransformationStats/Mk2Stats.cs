using MarvelTerrariaUniverse.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.IMTransformations.TransformationStats
{
    public class Mk2Stats : IronManStatsTemplate
    {
        public Mk2Stats()
        {
            powerRegenStat = 0.01f;
            flightDrainStat = 0.1f;

            repulsorDrainStat = 0.3f;
            repulsorDamage = 15;
            repulsorCooldown = 20;

            unibeamDrainStat = 10f;
            unibeamDamage = 30;
            unibeamCooldown = 600;

            flightSpeed = 1f;

            Attack1 = ModContent.ProjectileType<IronManRepulsor>();
        }


    }
}
