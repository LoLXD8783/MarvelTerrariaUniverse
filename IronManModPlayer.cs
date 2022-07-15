using MarvelTerrariaUniverse.Mounts;
using MarvelTerrariaUniverse.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse
{
    public class IronManModPlayer : ModPlayer
    {
        MTUModPlayer MTUModPlayer => Player.GetModPlayer<MTUModPlayer>();

        public bool GantryUIActive;

        public float HeadRotation;
        public float TargetHeadRotation;

        public bool FaceplateOn = true;
        public bool FaceplateMoving = false;
        public int FaceplateFrameCount = 0;
        public int FaceplateFrameTimer = 0;

        public bool HelmetOn = true;
        public bool HelmetDropped = false;

        public bool FlightToggled = false;
        public bool Flying = false;
        public bool Hovering => Flying && !Player.controlUp && !Player.controlDown && !Player.controlLeft && !Player.controlRight;

        public Color FlameColor = Color.Yellow;
        public int FlameFrameCount = 0;
        public int FlameFrameTimer = 0;

        public readonly List<string> IronManSuitTextures = new();

        public bool TransformationActive_IronMan => TransformationActive_IronManMk1 || TransformationActive_IronManMk2 || TransformationActive_IronManMk3 || TransformationActive_IronManMk4 || TransformationActive_IronManMk5 || TransformationActive_IronManMk6;
        public bool TransformationActive_IronManMk1;
        public bool TransformationActive_IronManMk2;
        public bool TransformationActive_IronManMk3;
        public bool TransformationActive_IronManMk4;
        public bool TransformationActive_IronManMk5;
        public bool TransformationActive_IronManMk6;

        public void ResetSuits_IronMan()
        {
            FaceplateFrameCount = 0;
            FaceplateOn = true;
            HelmetOn = true;
            FlightToggled = false;
            Flying = false;
            FlameFrameCount = 0;

            TransformationActive_IronManMk1 = false;
            TransformationActive_IronManMk2 = false;
            TransformationActive_IronManMk3 = false;
            TransformationActive_IronManMk4 = false;
            TransformationActive_IronManMk5 = false;
            TransformationActive_IronManMk6 = false;
        }

        public void FaceplateToggle()
        {
            if (!FaceplateMoving)
            {
                FaceplateFrameCount = FaceplateOn ? 0 : 3;
                FaceplateFrameTimer = 0;
            }
            else
            {
                if (FaceplateFrameCount < (FaceplateOn ? 2 : 5))
                {
                    FaceplateFrameTimer++;

                    if (FaceplateFrameTimer > 5)
                    {
                        FaceplateFrameCount++;
                        FaceplateFrameTimer = 0;
                    }
                }
                else
                {
                    FaceplateMoving = false;
                    FaceplateOn = !FaceplateOn;
                }
            }
        }

        public void HelmetToggle()
        {
            if (!HelmetOn)
            {
                Player.head = -1;

                if (!HelmetDropped)
                {
                    Projectile.NewProjectile(Terraria.Entity.GetSource_None(), new Vector2(Player.Center.X + 20 * Player.direction, Player.Center.Y), new Vector2(Player.velocity.X, 0f), ModContent.ProjectileType<IronManHelmet>(), 0, 0);

                    HelmetDropped = true;
                }
            }
        }

        public void FlightToggle()
        {
            if (FlightToggled)
            {
                Flying = true;
                Player.mount.SetMount(ModContent.MountType<IronManFlight>(), Player, Player.direction == -1);

                if (FlameFrameCount < 2)
                {
                    FlameFrameTimer++;

                    if (FlameFrameTimer > 15)
                    {
                        FlameFrameCount++;
                        FlameFrameTimer = 0;
                    }
                }
            }
            else
            {
                if (FlameFrameCount > 0)
                {
                    FlameFrameTimer++;

                    if (FlameFrameTimer > 15)
                    {
                        FlameFrameCount--;
                        FlameFrameTimer = 0;
                    }
                }

                Flying = false;
                Player.mount.Dismount(Player);
            }
        }

        public override void FrameEffects()
        {
            if (TransformationActive_IronManMk1) MTUModPlayer.UseEquipSlot("IronManMk1");
            if (TransformationActive_IronManMk2) MTUModPlayer.UseEquipSlot("IronManMk2");
            if (TransformationActive_IronManMk3) MTUModPlayer.UseEquipSlot("IronManMk3");
            if (TransformationActive_IronManMk4) MTUModPlayer.UseEquipSlot("IronManMk4");
            if (TransformationActive_IronManMk5) MTUModPlayer.UseEquipSlot("IronManMk5");
            if (TransformationActive_IronManMk6) MTUModPlayer.UseEquipSlot("IronManMk6");
        }

        public override void PreUpdate()
        {
            Vector2 Offset = Main.MouseWorld - Player.Center;

            if (Player.sleeping.isSleeping) TargetHeadRotation = 0;
            else
            {
                if (Flying && !Hovering) TargetHeadRotation = 0.8f * -Player.direction;
                else
                {
                    Player.direction = Math.Sign(Offset.X);
                    if (Math.Sign(Offset.X) == Player.direction) TargetHeadRotation = (Offset * Player.direction).ToRotation() * 0.55f;
                    else TargetHeadRotation = 0;
                }
            }

            HeadRotation = MathHelper.Lerp(HeadRotation, TargetHeadRotation, 16f * (1f / 60));
        }

        public override void PostUpdate()
        {
            if (TransformationActive_IronMan)
            {
                Main.playerInventory = false;

                if (Flying) Player.legFrame.Y = 0 * Player.legFrame.Height;
            }

            FaceplateToggle();
            HelmetToggle();
            FlightToggle();
        }

        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            drawInfo.rotationOrigin = drawPlayer.Hitbox.Size() / 2f;

            drawPlayer.headRotation = HeadRotation;

            if (Flying)
            {
                if (!Hovering) drawPlayer.fullRotation = drawPlayer.fullRotation.AngleLerp(drawPlayer.velocity.ToRotation() + MathHelper.PiOver2, 0.1f);
                else drawPlayer.fullRotation = drawPlayer.fullRotation.AngleLerp(0f, 0.1f);
            }
        }

        public override void SetControls()
        {
            if (TransformationActive_IronMan || GantryUIActive)
            {
                Player.controlCreativeMenu = false;
                Player.controlHook = false;
                Player.controlInv = false;
                Player.controlMount = false;
                Player.controlThrow = false;
                Player.controlTorch = false;
                Player.controlUseItem = false;
                // Player.controlUseTile = false;
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (TransformationActive_IronMan)
            {
                if (Keybinds.IronMan_ToggleFaceplate.JustPressed && HelmetOn) FaceplateMoving = true;
                if (Keybinds.IronMan_ToggleHelmet.JustPressed) HelmetOn = false;
                if (Keybinds.IronMan_ToggleFlight.JustPressed) FlightToggled = !FlightToggled;
            }
        }
    }
}
