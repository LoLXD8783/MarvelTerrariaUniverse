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
            powerRegenStat = 0.03f;
            flightDrainStat = 0.04f;

            repulsorDrainStat = 0.2f;
            repulsorDamage = 80;
            repulsorCooldown = 60;

            unibeamDrainStat = 2f;
            unibeamDamage = 12;
            unibeamCooldown = 600;

            flightSpeed = 1f;

            Attack1 = ModContent.ProjectileType<IronManRepulsor>();
        }


    }
}
