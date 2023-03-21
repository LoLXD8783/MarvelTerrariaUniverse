using Terraria.Audio;
using MarvelTerrariaUniverse.Common.PlayerLayers;
using MarvelTerrariaUniverse.Common.PlayerLayers.IronMan;
using MarvelTerrariaUniverse.Common.Systems;
using MarvelTerrariaUniverse.Content.Mounts;
using MarvelTerrariaUniverse.Content.Projectiles.IronMan;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse.Common.Players;
public class IronManPlayer : ModPlayer
{
    public enum Weapon
    {
        None = 0,
        Repulsor = 1,
        Unibeam = 2
    }

    public bool IsTransformed => Player.GetModPlayer<BasePlayer>().transformation == Transformations.IronMan;

    public int Mark = 0;

    public int FaceplateFrame = 0;
    public int FaceplateFramerate = 5;
    public bool FaceplateOn = true;
    public bool FaceplateMoving = false;

    public bool Flying = false;
    public bool Hovering => Flying && !Player.controlUp && !Player.controlDown && !Player.controlLeft && !Player.controlRight;

    public float HeadRotation = 0f;

    public int FlightFlameFrame = 0;
    public int FlightFlameFramerate = 5;

    public bool HelmetDropped = false;
    public bool HelmetOn = true;

    public bool SuitEjected = false;
    public bool SuitOn = true;
    public int SuitDirection = 1;

    public int RotationCooldown = 30;

    public Weapon RequestedWeapon = Weapon.None;

    public void ResetEverything(bool soft = false)
    {
        if (!soft)
        {
            Mark = 0;
            SuitEjected = false;
            SuitOn = true;
            SuitDirection = 1;
        }

        FaceplateFrame = 0;
        FaceplateFramerate = 5;
        FaceplateOn = true;
        FaceplateMoving = false;
        Flying = false;
        Player.mount.Dismount(Player);
        HeadRotation = 0f;
        FlightFlameFrame = 0;
        FlightFlameFramerate = 5;
        HelmetDropped = false;
        HelmetOn = true;
        RotationCooldown = 30;
    }

    public void FlightAnimation()
    {
        var offset = Main.MouseWorld - Player.Center;
        var targetRot = 0f;

        if (!Flying) targetRot = 0f;
        else
        {
            FlightFlameFramerate++;

            if (FlightFlameFramerate > 5)
            {
                if (FlightFlameFrame >= (Hovering ? 1 : 2)) FlightFlameFrame = 0;
                else FlightFlameFrame++;

                FlightFlameFramerate = 0;
            }

            var distanceOffset = Player.Hitbox.Size() / 2f * 1.5f - new Vector2(Player.width - 2.5f, 0f);
            var center = Player.Hitbox.Location.ToVector2() + new Vector2(0f, 10f) + distanceOffset.RotatedBy(Player.fullRotation);

            var dust = Dust.NewDustDirect(center, Player.width, Player.height / 2, DustID.Smoke, 0f, 0f, 100, Scale: 0.5f);
            dust.scale *= 1f + Main.rand.Next(10) * 0.1f;
            dust.velocity *= 0.2f;
            dust.noGravity = true;

            if (Main.rand.NextBool(Hovering ? 5 : 1))
            {
                var dust2 = Dust.NewDustDirect(center, Player.width, Player.height / 2, DustID.Torch, Player.velocity.X * 0.2f, Player.velocity.Y * 0.2f, 100, Color.Yellow, 2f);
                dust2.noGravity = true;
                dust2.velocity *= 1.4f;
                dust2.velocity += Main.rand.NextVector2Circular(1f, 1f);
                dust2.velocity += Player.velocity * 0.15f;
            }

            Player.legFrame.Y = 0;

            if (Player.velocity.LengthSquared() > Math.Pow(7f, 2)) Player.velocity = Player.velocity.SafeNormalize(Vector2.Zero) * 7f;

            if (Hovering && RotationCooldown <= 0)
            {
                Player.direction = Math.Sign(offset.X);
                if (Math.Sign(offset.X) == Player.direction) targetRot = (offset * Player.direction).ToRotation() * 0.55f;
            }
            else targetRot = 0.8f * -Player.direction;
        }

        HeadRotation = Utils.AngleLerp(HeadRotation, targetRot, 16f * (1f / 60));
    }

    public void FaceplateAnimation()
    {
        if (!FaceplateMoving) return;

        if (FaceplateFrame != (FaceplateOn ? 2 : 0))
        {
            FaceplateFramerate--;

            if (FaceplateFramerate < 0)
            {
                FaceplateFrame += FaceplateOn ? 1 : -1;
                FaceplateFramerate = 5;
            }
        }
        else
        {
            FaceplateMoving = false;
            FaceplateOn = !FaceplateOn;
        }
    }

    public void DropHelmet()
    {
        if (HelmetOn) return;

        if (!HelmetDropped)
        {
            Projectile.NewProjectile(Terraria.Entity.GetSource_None(), new Vector2(Player.Center.X + 20 * Player.direction, Player.Center.Y), new Vector2(Player.velocity.X, 0f), ModContent.ProjectileType<IronManHelmet>(), 0, 0, Player.whoAmI);

            HelmetDropped = true;
        }
    }

    public void EjectSuit()
    {
        if (SuitOn) return;

        if (!SuitEjected)
        {
            SuitDirection = Player.direction;
            Projectile.NewProjectile(Terraria.Entity.GetSource_None(), Player.Center, Player.velocity, ModContent.ProjectileType<IronManSuit>(), 0, 0, Player.whoAmI);

            SuitEjected = true;
            ResetEverything(true);
        }
    }

    public void FireMainWeapons()
    {
        if (!SuitOn) return;

        //if (Main.mouseLeft)
        if (true)
        {
            int count = 1;
            for (int i = 0; i < count; i++)
            {
                float k = (Main.GameUpdateCount / 60f % MathHelper.TwoPi) * 0 + (MathHelper.TwoPi / (float)count * i) * 1;
                RequestedWeapon = Weapon.Repulsor;
                float angle = Player.MountedCenter.AngleTo(Main.MouseWorld) + MathHelper.PiOver2 * 3 - Player.fullRotation + k;
                Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, angle);
                Vector2 pos = Player.GetFrontHandPosition(Player.CompositeArmStretchAmount.Full, angle);

                //Main.mouseY = (int)(Math.Abs(Math.Sin(Main.GameUpdateCount / 100f)) * Main.screenHeight);
                Projectile.NewProjectile(Terraria.Entity.GetSource_None(), pos, Vector2.UnitX.RotatedBy(k + MathHelper.Pi), ModContent.ProjectileType<Laser>(), 1, 1, Player.whoAmI);

                /*
                    RequestedWeapon = Weapon.Repulsor;
                    Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Player.AngleTo(Main.MouseWorld) + MathHelper.PiOver2 * 3 - Player.fullRotation);
                    Vector2 pos = Player.GetFrontHandPosition(Player.CompositeArmStretchAmount.Full, Player.AngleTo(Main.MouseWorld) + MathHelper.PiOver2 - Player.fullRotation);

                    Projectile.NewProjectile(Terraria.Entity.GetSource_None(), pos, Vector2.Zero, ModContent.ProjectileType<Laser>(), 1, 1, Player.whoAmI);*/
            }
        }
    }

    public override void PostUpdate()
    {
        if (!IsTransformed) return;

        FlightAnimation();
        FaceplateAnimation();
        DropHelmet();
        EjectSuit();
        FireMainWeapons();
    }

    public override void FrameEffects()
    {
        if (!IsTransformed) return;

        var headName = $"IronManMark{Mark}" + (FaceplateFrame == 1 ? "Alt" : FaceplateFrame == 2 ? "Alt2" : "");
        var bodyName = $"IronManMark{Mark}" + (Flying ? "Alt" : "");

        if (SuitOn && HelmetOn) Player.head = EquipLoader.GetEquipSlot(Mod, headName, EquipType.Head);
        if (SuitOn) Player.body = EquipLoader.GetEquipSlot(Mod, bodyName, EquipType.Body);
        if (SuitOn) Player.legs = EquipLoader.GetEquipSlot(Mod, $"IronManMark{Mark}" + (Hovering ? "Alt" : ""), EquipType.Legs);

        if (Main.dedServ) return;

        var path = $"{MarvelTerrariaUniverse.TextureAssets}/Glowmasks/IronMan";

        if (SuitOn && HelmetOn && FaceplateFrame != 2 && FaceplateOn)
        {
            HelmetGlowmask.RegisterData(EquipLoader.GetEquipSlot(Mod, headName, EquipType.Head), new DrawLayerData()
            {
                Texture = ModContent.Request<Texture2D>($"{path}/Faceplate{FaceplateFrame}"),
                Color = (drawInfo) => Color.White
            });
        }

        BasePlayer.RegisterData(EquipLoader.GetEquipSlot(Mod, bodyName, EquipType.Body), () => Color.White);
    }

    public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
    {
        if (!IsTransformed) return;

        var drawPlayer = drawInfo.drawPlayer;
        drawInfo.rotationOrigin = drawPlayer.Hitbox.Size() / 2f;

        if (SuitOn) Lighting.AddLight((int)drawPlayer.position.X / 16, (int)drawPlayer.position.Y / 16, TorchID.Torch, 0.5f);

        if (Flying)
        {
            if (Hovering)
            {
                RotationCooldown--;

                if (RotationCooldown <= 0) drawPlayer.fullRotation = drawPlayer.fullRotation.AngleLerp(((Main.MouseWorld - drawPlayer.Center) * drawPlayer.direction).ToRotation() * 0.55f, 0.05f);
            }
            else
            {
                RotationCooldown = 30;
                drawPlayer.fullRotation = drawPlayer.fullRotation.AngleLerp(drawPlayer.velocity.ToRotation() + MathHelper.PiOver2, 0.075f);
            }
        }

        drawPlayer.headRotation = HeadRotation;
    }

    public override void SetControls()
    {
        if (!IsTransformed) return;

        if (Flying)
        {
            Player.controlJump = false;
            Player.controlMount = false;
        }
    }

    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        if (!IsTransformed || Mark <= 1 || !SuitOn) return;

        if (KeybindSystem.FlightToggle.JustPressed)
        {
            Flying = !Flying;

            if (Flying) Player.mount.SetMount(ModContent.MountType<IronManFlight>(), Player, Player.direction == -1);
            else Player.mount.Dismount(Player);
        }

        if (KeybindSystem.FaceplateToggle.JustPressed && HelmetOn)
        {
            SoundEngine.PlaySound(new SoundStyle($"{MarvelTerrariaUniverse.SoundAssets}/IronMan/Faceplate{(!FaceplateOn ? "On" : "Off")}"));
            FaceplateMoving = true;
        }

        if (KeybindSystem.DropHelmet.JustPressed) HelmetOn = false;

        if (KeybindSystem.EjectSuit.JustPressed)
        {
            SoundEngine.PlaySound(new SoundStyle($"{MarvelTerrariaUniverse.SoundAssets}/IronMan/Depower"));
            SuitOn = false;
        }
    }
}