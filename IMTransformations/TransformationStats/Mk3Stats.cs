using MarvelTerrariaUniverse.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.IMTransformations.TransformationStats
{
    public class Mk3Stats : IronManStatsTemplate
    {
        public Mk3Stats()
        {
            powerRegenStat = 0.05f;
            flightDrainStat = 0.15f;

            repulsorDrainStat = 0.25f;
            repulsorDamage = 20;
            repulsorCooldown = 19;

            unibeamDrainStat = 10f;
            unibeamDamage = 40;
            unibeamCooldown = 600;

            flightSpeed = 1f;

            Attack1 = ModContent.ProjectileType<IronManRepulsor>();
        }


    }
}
