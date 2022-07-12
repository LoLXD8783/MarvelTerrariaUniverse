using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Mounts
{
    public class IronManFlight : ModMount
    {
        public const float speed = 4f;

        public override void SetStaticDefaults()
        {
            MountData.totalFrames = 1;
            MountData.heightBoost = 0;
            MountData.flightTimeMax = int.MaxValue;
            MountData.fatigueMax = int.MaxValue;
            MountData.fallDamage = 0f;
            MountData.usesHover = true;
            MountData.runSpeed = 9f;
            MountData.dashSpeed = 9f;
            MountData.acceleration = 0.16f;
            MountData.jumpHeight = 10;
            MountData.jumpSpeed = 4f;
            MountData.blockExtraJumps = true;
            int[] array = new int[MountData.totalFrames];
            for (int l = 0; l < array.Length; l++) array[l] = 0;
            MountData.playerYOffsets = new int[] { 0 };
            MountData.xOffset = 0;
            MountData.bodyFrame = 0;
            MountData.yOffset = 0;
            MountData.playerHeadOffset = 0;
            MountData.standingFrameCount = 0;
            MountData.standingFrameDelay = 0;
            MountData.standingFrameStart = 0;
            MountData.runningFrameCount = 0;
            MountData.runningFrameDelay = 0;
            MountData.runningFrameStart = 0;
            MountData.flyingFrameCount = 0;
            MountData.flyingFrameDelay = 0;
            MountData.flyingFrameStart = 0;
            MountData.inAirFrameCount = 0;
            MountData.inAirFrameDelay = 0;
            MountData.inAirFrameStart = 0;
            MountData.idleFrameCount = 0;
            MountData.idleFrameDelay = 0;
            MountData.idleFrameStart = 0;
            MountData.idleFrameLoop = true;
            MountData.swimFrameCount = 0;
            MountData.swimFrameDelay = 0;
            MountData.swimFrameStart = 0;

            if (!Main.dedServ || Main.netMode != NetmodeID.Server)
            {
                MountData.textureWidth = MountData.backTexture.Width();
                MountData.textureHeight = MountData.backTexture.Height();
            }
        }

        public override void SetMount(Player player, ref bool skipDust)
        {
            // skipDust = true;

            if (player.velocity.Y == 0) player.velocity.Y -= 4f;
        }
    }
}
