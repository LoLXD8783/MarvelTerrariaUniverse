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
            powerRegenStat = 0.08f;
            flightDrainStat = 0.20f;

            repulsorDrainStat = 0.25f;
            repulsorDamage = 30;
            repulsorCooldown = 16;

            unibeamDrainStat = 10f;
            unibeamDamage = 60;
            unibeamCooldown = 600;

            flightSpeed = 1f;

            Attack1 = ModContent.ProjectileType<IronManRepulsor>();
        }


    }
}
