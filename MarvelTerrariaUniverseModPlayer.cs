using MarvelTerrariaUniverse.Mounts;
using MarvelTerrariaUniverse.Projectiles;
using MarvelTerrariaUniverse.Tiles;
using MarvelTerrariaUniverse.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse
{
    public class MarvelTerrariaUniverseModPlayer : ModPlayer
    {
        #region Glowmask Support

        private static Dictionary<int, Func<Color>> BodyColor { get; set; }

        /// <summary>
        /// Add glowmask color associated with the body equip slot here, usually in <see cref="ModType.SetStaticDefaults"/>.
        /// <para>Don't forget the !Main.dedServ check!</para>
        /// </summary>
        /// <param name="bodySlot">Body equip slot</param>
        /// <param name="color">Color</param>
        public static void RegisterData(int bodySlot, Func<Color> color)
        {
            if (!BodyColor.ContainsKey(bodySlot))
            {
                BodyColor.Add(bodySlot, color);
            }
        }

        public override void Load()
        {
            BodyColor = new Dictionary<int, Func<Color>>();
        }

        public override void Unload()
        {
            BodyColor = null;
        }

        #endregion

        #region All transformations

        public bool TransformationActive => TransformationActive_IronMan;
        public string ActiveTransformation = "None";

        public void UseEquipSlot(string texture)
        {
            if (texture.Contains("IronMan"))
            {
                if (texture == "IronManMk1") Player.head = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Head);
                else
                {
                    Player.head = EquipLoader.GetEquipSlot(Mod, $"{texture}_Faceplate{FaceplateFrameCount}", EquipType.Head);

                    if (!Main.dedServ)
                    {
                        if (FaceplateFrameCount < 3)
                        {
                            HeadLayer.RegisterData(EquipLoader.GetEquipSlot(Mod, $"{texture}_Faceplate{FaceplateFrameCount}", EquipType.Head), new DrawLayerData()
                            {
                                Texture = ModContent.Request<Texture2D>($"MarvelTerrariaUniverse/TransformationTextures/Glowmasks/IronMan_Faceplate{FaceplateFrameCount}_Glowmask")
                            });
                        }

                        RegisterData(EquipLoader.GetEquipSlot(Mod, texture, EquipType.Body), () => new Color(255, 255, 255, 0));
                    }
                }
            }
            else Player.head = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Head);

            Player.body = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Body);
            Player.legs = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Legs);

            ActiveTransformation = texture;
        }

        public void ResetEquipSlot()
        {
            ResetSuits_IronMan();
        }

        public override void ResetEffects()
        {
            ModContent.GetInstance<GantryTile>().PlayGantryFrames = false;
        }

        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if (!BodyColor.TryGetValue(Player.body, out Func<Color> color))
            {
                return;
            }

            drawInfo.bodyGlowColor = color();
            drawInfo.armGlowColor = color();

            if (drawInfo.drawPlayer == UICharacterEquipped.DrawnPlayer)
            {
                drawInfo.colorArmorHead = Color.White;
                drawInfo.colorArmorBody = Color.White;
                drawInfo.colorArmorLegs = Color.White;
            }

            ModifyDrawInfo_IronMan(ref drawInfo);
        }

        public override void FrameEffects()
        {
            FrameEffects_IronMan();
        }

        public override void PreUpdate()
        {
            PreUpdate_IronMan();
        }

        public override void PostUpdate()
        {
            PostUpdate_IronMan();
        }

        public override void SetControls()
        {
            SetControls_IronMan();
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            ProcessTriggers_IronMan(triggersSet);
        }

        #endregion

        #region Transformation: Iron Man

        public bool GantryUIActive;

        public float HeadRotation;
        public float TargetHeadRotation;
        public float BodyRotation;
        public float TargetBodyRotation;

        public bool FaceplateOn = true;
        public bool FaceplateMoving = false;
        public int FaceplateFrameCount = 0;
        public int FaceplateFrameTimer = 0;

        public bool HelmetOn = true;
        public bool HelmetDropped = false;

        public bool FlightToggled = false;
        public bool Flying = false;

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
                Player.mount.SetMount(ModContent.MountType<IronManFlight>(), Player, Player.direction == -1);
                Player.fullRotationOrigin = Player.Hitbox.Size() / 2;
                Player.fullRotation = BodyRotation;
            }
            else Player.mount.Dismount(Player);
        }

        public void ModifyDrawInfo_IronMan(ref PlayerDrawSet drawInfo)
        {
            if (TransformationActive_IronMan)
            {
                Player.direction = Main.MouseWorld.X >= Player.Center.X ? 1 : -1;
                Player.headRotation = HeadRotation;
            }
        }

        public void FrameEffects_IronMan()
        {
            if (TransformationActive_IronManMk1) UseEquipSlot("IronManMk1");
            if (TransformationActive_IronManMk2) UseEquipSlot("IronManMk2");
            if (TransformationActive_IronManMk3) UseEquipSlot("IronManMk3");
            if (TransformationActive_IronManMk4) UseEquipSlot("IronManMk4");
            if (TransformationActive_IronManMk5) UseEquipSlot("IronManMk5");
            if (TransformationActive_IronManMk6) UseEquipSlot("IronManMk6");
        }

        public void PreUpdate_IronMan()
        {
            if (Player.sleeping.isSleeping)
            {
                TargetBodyRotation = 0;
                TargetHeadRotation = 0;
            }
            else
            {
                Vector2 offset = Player.velocity;

                if (Math.Sign(offset.X) == Player.direction)
                {
                    TargetBodyRotation = (offset * Player.direction).ToRotation() * 0.55f + MathHelper.PiOver2 * Player.direction;
                    TargetHeadRotation = (offset * Player.direction).ToRotation() * 0.55f;
                }
                else
                {
                    TargetBodyRotation = 0;
                    TargetHeadRotation = 0;
                }
            }

            BodyRotation = MathHelper.Lerp(BodyRotation, TargetBodyRotation, 16f * (1f / 60));
            HeadRotation = MathHelper.Lerp(HeadRotation, TargetHeadRotation, 16f * (1f / 60));
        }

        public void PostUpdate_IronMan()
        {
            if (TransformationActive_IronMan) Main.playerInventory = false;

            FaceplateToggle();
            HelmetToggle();
            FlightToggle();
        }

        public void SetControls_IronMan()
        {
            if (TransformationActive_IronMan || GantryUIActive)
            {
                if (Flying) Player.controlJump = false;

                Player.controlCreativeMenu = false;
                Player.controlHook = false;
                Player.controlInv = false;
                Player.controlMap = false;
                Player.controlMount = false;
                Player.controlThrow = false;
                Player.controlTorch = false;
                Player.controlUseItem = false;
                // Player.controlUseTile = false;
            }
        }

        public void ProcessTriggers_IronMan(TriggersSet triggersSet)
        {
            if (TransformationActive_IronMan)
            {
                if (Keybinds.IronMan_ToggleFaceplate.JustPressed && HelmetOn) if (!TransformationActive_IronManMk1) FaceplateMoving = true;
                if (Keybinds.IronMan_ToggleHelmet.JustPressed) HelmetOn = false;
                if (Keybinds.IronMan_ToggleFlight.JustPressed) FlightToggled = !FlightToggled;
            }
        }

        #endregion
    }
}