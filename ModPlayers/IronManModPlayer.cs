using MarvelTerrariaUniverse.Mounts;
using MarvelTerrariaUniverse.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.ModPlayers
{
    public class IronManModPlayer : ModPlayer
    {
        MTUModPlayer MTUModPlayer => Player.GetModPlayer<MTUModPlayer>();

        public bool GantryUIActive;

        public float HeadRotation;
        public float TargetHeadRotation;
        public int TargetArmFrame = 2;

        public bool SuitToggleRequested = false;
        public bool SuitEjected = false;
        public int EjectedSuitDirection = -1;

        public bool FaceplateOn = true;
        public bool FaceplateMoving = false;
        public int FaceplateFrameCount = 0;
        public int FaceplateFrameTimer = 0;

        public bool HelmetOn = true;
        public bool HelmetDropped = false;

        public bool FlightToggled = false;
        public bool Flying = false;
        public bool Hovering => Flying && !Player.controlUp && !Player.controlDown && !Player.controlLeft && !Player.controlRight;

        public int FlameFrameCount = 0;
        public int FlameFrameTimer = 0;

        public int RepulsorCooldown = 0;
        public bool RepulsorRequested = false;

        public int UnibeamCooldown = 0;
        public bool UnibeamRequested = false;

        public readonly List<string> IronManSuitTextures = new();

        public bool TransformationActive_IronMan => TransformationActive_WarMachineMk1 || TransformationActive_IronManMk1 || TransformationActive_IronManMk2 || TransformationActive_IronManMk3 || TransformationActive_IronManMk4 || TransformationActive_IronManMk5 || TransformationActive_IronManMk6 || TransformationActive_IronManMk7;
        public bool TransformationActive_WarMachineMk1;
        public bool TransformationActive_IronManMk1;
        public bool TransformationActive_IronManMk2;
        public bool TransformationActive_IronManMk3;
        public bool TransformationActive_IronManMk4;
        public bool TransformationActive_IronManMk5;
        public bool TransformationActive_IronManMk6;
        public bool TransformationActive_IronManMk7;

        public override void Load()
        {
            On.Terraria.Player.HorizontalMovement += static (orig, player) =>
            {
                orig(player);
                Main.LocalPlayer.GetModPlayer<IronManModPlayer>()?.ForceDirection();
            };

            On.Terraria.Player.ChangeDir += static (orig, player, dir) =>
            {
                orig(player, dir);
                Main.LocalPlayer.GetModPlayer<IronManModPlayer>()?.ForceDirection();
            };
        }

        private void ForceDirection()
        {
            if (TransformationActive_IronMan && (RepulsorRequested || UnibeamRequested))
            {
                if (!Flying || (Flying && Hovering)) Player.direction = Main.MouseWorld.X >= Player.Center.X ? 1 : -1;
                else Player.direction = (Main.MouseWorld.Y >= Player.Center.Y ? 1 : -1) * Math.Sign(Player.velocity.X);
            }
        }

        public void ResetSuits_IronMan(bool eject = false)
        {
            FlightToggled = false;
            Flying = false;
            FlameFrameCount = 0;

            Player.mount.Dismount(Player);

            if (!eject)
            {
                SuitToggleRequested = false;
                SuitEjected = false;

                FaceplateFrameCount = 0;
                FaceplateOn = true;

                HelmetOn = true;

                RepulsorCooldown = 0;
                RepulsorRequested = false;

                UnibeamCooldown = 0;
                UnibeamRequested = false;

                TransformationActive_WarMachineMk1 = false;
                TransformationActive_IronManMk1 = false;
                TransformationActive_IronManMk2 = false;
                TransformationActive_IronManMk3 = false;
                TransformationActive_IronManMk4 = false;
                TransformationActive_IronManMk5 = false;
                TransformationActive_IronManMk6 = false;
                TransformationActive_IronManMk7 = false;
            }
        }

        public void SuitToggle()
        {
            if (SuitToggleRequested)
            {
                EjectedSuitDirection = Player.direction;
                if (!SuitEjected) Projectile.NewProjectile(Terraria.Entity.GetSource_None(), EjectedSuitDirection == -1 ? Player.Center : new Vector2(Player.Center.X - 1, Player.Center.Y), new Vector2(Player.velocity.X, 0f), ModContent.ProjectileType<IronManSuit>(), 0, 0);

                ResetSuits_IronMan(true);

                SuitEjected = !SuitEjected;
                SuitToggleRequested = false;
            }
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

                    if (FlameFrameTimer > 5)
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

                    if (FlameFrameTimer > 5)
                    {
                        FlameFrameCount--;
                        FlameFrameTimer = 0;
                    }
                }

                Flying = false;
                Player.mount.Dismount(Player);
            }
        }

        public void WeaponFunctions()
        {
            if (RepulsorRequested)
            {
                RepulsorCooldown++;

                Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (Player.Center - Main.MouseWorld).ToRotation() + MathHelper.PiOver2 - Player.fullRotation);
                if (RepulsorCooldown == 60)
                {
                    // SoundEngine.PlaySound(new SoundStyle("MarvelTerrariaUniverse/SoundEffects/IronMan/Repulsor_Blast"));
                    Projectile.NewProjectile(Terraria.Entity.GetSource_None(), Player.Center, Vector2.Zero, ModContent.ProjectileType<IronManRepulsor>(), 0, 0);
                }
            }
            else RepulsorCooldown = 0;

            if (UnibeamRequested)
            {
                UnibeamCooldown++;

                if (UnibeamCooldown == 180)
                {
                    SoundEngine.PlaySound(new SoundStyle("MarvelTerrariaUniverse/SoundEffects/IronMan/Repulsor_Blast"));
                }
            }
            else UnibeamCooldown = 0;
        }

        public override void FrameEffects()
        {
            if (TransformationActive_IronMan && !SuitEjected)
            {
                if (TransformationActive_WarMachineMk1) MTUModPlayer.UseEquipSlot("WarMachineMk1");
                if (TransformationActive_IronManMk1) MTUModPlayer.UseEquipSlot("IronManMk1");
                if (TransformationActive_IronManMk2) MTUModPlayer.UseEquipSlot("IronManMk2");
                if (TransformationActive_IronManMk3) MTUModPlayer.UseEquipSlot("IronManMk3");
                if (TransformationActive_IronManMk4) MTUModPlayer.UseEquipSlot("IronManMk4");
                if (TransformationActive_IronManMk5) MTUModPlayer.UseEquipSlot("IronManMk5");
                if (TransformationActive_IronManMk6) MTUModPlayer.UseEquipSlot("IronManMk6");
                if (TransformationActive_IronManMk7) MTUModPlayer.UseEquipSlot("IronManMk7");
            }
        }

        public override void PreUpdate()
        {
            if (TransformationActive_IronMan)
            {
                Vector2 Offset = Main.MouseWorld - Player.Center;

                if (Player.sleeping.isSleeping || !Flying) TargetHeadRotation = 0;
                else
                {
                    if (Flying)
                    {
                        if (!Hovering) TargetHeadRotation = 0.8f * -Player.direction;
                        else
                        {
                            Player.direction = Math.Sign(Offset.X);

                            if (Math.Sign(Offset.X) == Player.direction) TargetHeadRotation = (Offset * Player.direction).ToRotation() * 0.55f;
                        }
                    }
                }

                HeadRotation = MathHelper.Lerp(HeadRotation, TargetHeadRotation, 16f * (1f / 60));
            }
        }

        public override void PostUpdate()
        {
            if (TransformationActive_IronMan)
            {
                SuitToggle();

                if (!SuitEjected)
                {
                    Main.playerInventory = false;

                    if (Flying)
                    {
                        Player.legFrame.Y = 0 * Player.legFrame.Height;

                        FlameFrameTimer++;

                        if (FlameFrameTimer > 5)
                        {
                            if (FlameFrameCount >= (Hovering ? 1 : 2)) FlameFrameCount = 0;
                            else FlameFrameCount++;

                            FlameFrameTimer = 0;
                        }
                    }

                    FaceplateToggle();
                    HelmetToggle();
                    FlightToggle();
                    WeaponFunctions();
                }
            }
        }

        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            drawInfo.rotationOrigin = drawPlayer.Hitbox.Size() / 2f;

            if (TransformationActive_IronMan)
            {
                if (Flying)
                {
                    if (Hovering) drawPlayer.fullRotation = drawPlayer.fullRotation.AngleLerp(((Main.MouseWorld - Player.Center) * Player.direction).ToRotation() * 0.55f, 0.1f);
                    else drawPlayer.fullRotation = drawPlayer.fullRotation.AngleLerp(drawPlayer.velocity.ToRotation() + MathHelper.PiOver2, 0.1f);
                }

                drawPlayer.headRotation = HeadRotation;
            }
        }

        public override void SetControls()
        {
            if ((TransformationActive_IronMan && !SuitEjected) || GantryUIActive)
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

            if (TransformationActive_IronMan && Flying) Player.controlJump = false;
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (TransformationActive_IronMan)
            {
                if (Keybinds.IronMan_ToggleSuit.JustPressed)
                {
                    if (!SuitEjected) SoundEngine.PlaySound(new SoundStyle("MarvelTerrariaUniverse/SoundEffects/IronMan/Depower"));

                    SuitToggleRequested = true;
                }

                if (!SuitEjected)
                {
                    if (Keybinds.IronMan_ToggleFaceplate.JustPressed && HelmetOn)
                    {
                        SoundEngine.PlaySound(new SoundStyle($"MarvelTerrariaUniverse/SoundEffects/IronMan/Faceplate_{(FaceplateOn ? "Off" : "On")}"));
                        FaceplateMoving = true;
                    }

                    if (Keybinds.IronMan_ToggleHelmet.JustPressed) HelmetOn = false;

                    if (Keybinds.IronMan_ToggleFlight.JustPressed) FlightToggled = !FlightToggled;

                    if (Keybinds.IronMan_FireRepulsor.JustPressed && !RepulsorRequested) SoundEngine.PlaySound(new SoundStyle("MarvelTerrariaUniverse/SoundEffects/IronMan/Repulsor_Charge"));
                    if (Keybinds.IronMan_FireRepulsor.Current && !RepulsorRequested) RepulsorRequested = true;
                    if (Keybinds.IronMan_FireRepulsor.JustReleased) RepulsorRequested = false;

                    if (Keybinds.IronMan_FireUnibeam.JustPressed && !UnibeamRequested) SoundEngine.PlaySound(new SoundStyle("MarvelTerrariaUniverse/SoundEffects/IronMan/Unibeam_Charge"));
                    if (Keybinds.IronMan_FireUnibeam.Current && !UnibeamRequested) UnibeamRequested = true;
                    if (Keybinds.IronMan_FireUnibeam.JustReleased) UnibeamRequested = false;
                }
            }
        }
    }
}