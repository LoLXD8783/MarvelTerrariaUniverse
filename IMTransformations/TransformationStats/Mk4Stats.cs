using MarvelTerrariaUniverse.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.IMTransformations.TransformationStats
{
    public class Mk4Stats : IronManStatsTemplate
    {
        public Mk4Stats()
        {
            powerRegenStat = 0.07f;
            flightDrainStat = 0.20f;

            repulsorDrainStat = 0.25f;
            repulsorDamage = 25;
            repulsorCooldown = 17;

            unibeamDrainStat = 10f;
            unibeamDamage = 50;
            unibeamCooldown = 600;

            flightSpeed = 1f;

            Attack1 = ModContent.ProjectileType<IronManRepulsor>();
        }


    }
}
