using MarvelTerrariaUniverse.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.IMTransformations.TransformationStats
{
    public class Mk5Stats : IronManStatsTemplate
    {
        public Mk5Stats()
        {
            powerRegenStat = 1f;
            flightDrainStat = 4f;

            repulsorDrainStat = 0.3f;
            repulsorDamage = 60;
            repulsorCooldown = 10;

            unibeamDrainStat = 1f;
            unibeamDamage = 120;
            unibeamCooldown = 600;

            flightSpeed = 1f;

            Attack1 = ModContent.ProjectileType<IronManRepulsor>();
        }


    }
}
