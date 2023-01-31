using MarvelTerrariaUniverse.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.IMTransformations.TransformationStats
{
    public class Mk7Stats : IronManStatsTemplate
    {


        public Mk7Stats()
        {
            powerRegenStat = 0.1f;
            flightDrainStat = 0.20f;

            repulsorDrainStat = 0.25f;
            repulsorDamage = 40;
            repulsorCooldown = 15;

            unibeamDrainStat = 10f;
            unibeamDamage = 80;
            unibeamCooldown = 600;

            flightSpeed = 1f;

            Attack1 = ModContent.ProjectileType<IronManRepulsor>();
        }

    }
}
