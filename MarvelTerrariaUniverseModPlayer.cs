using MarvelTerrariaUniverse.Projectiles;
using MarvelTerrariaUniverse.UI.Elements;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse
{
    public class MarvelTerrariaUniverseModPlayer : ModPlayer
    {
        #region All transformations

        public bool TransformationActive => TransformationActive_IronManMk2 || TransformationActive_IronManMk3;
        public string ActiveTransformation = "None";

        public void UseEquipSlot(string texture)
        {
            Player.head = EquipLoader.GetEquipSlot(Mod, $"{texture}_Faceplate{FaceplateFrameCount}", EquipType.Head);
            Player.body = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Body);
            Player.legs = EquipLoader.GetEquipSlot(Mod, texture, EquipType.Legs);

            ActiveTransformation = texture;
        }

        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            ModifyDrawInfo_IronMan(ref drawInfo);
        }

        public override void FrameEffects()
        {
            FrameEffects_IronMan();
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

        public bool FaceplateOn = true;
        public bool FaceplateMoving = false;
        public int FaceplateFrameCount = 0;
        public int FaceplateFrameTimer = 0;

        public bool HelmetOn = true;
        public bool HelmetDropped = false;

        public bool TransformationActive_IronMan => TransformationActive_IronManMk2 || TransformationActive_IronManMk3;
        public bool TransformationActive_IronManMk2;
        public bool TransformationActive_IronManMk3;

        public void ResetSuits()
        {
            FaceplateFrameCount = 0;
            FaceplateOn = true;
            HelmetOn = true;

            TransformationActive_IronManMk2 = false;
            TransformationActive_IronManMk3 = false;
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

        public void ModifyDrawInfo_IronMan(ref PlayerDrawSet drawInfo)
        {
            if (drawInfo.drawPlayer == UICharacterEditable.Player)
            {
                drawInfo.colorArmorHead = Color.White;
                drawInfo.colorArmorBody = Color.White;
                drawInfo.colorArmorLegs = Color.White;
            }
        }

        public void FrameEffects_IronMan()
        {
            if (TransformationActive_IronManMk2) UseEquipSlot("IronManMk2");
            if (TransformationActive_IronManMk3) UseEquipSlot("IronManMk3");
        }

        public void PostUpdate_IronMan()
        {
            if (TransformationActive_IronMan) Main.playerInventory = false;

            FaceplateToggle();
            HelmetToggle();
        }

        public void SetControls_IronMan()
        {
            if (TransformationActive_IronMan || GantryUIActive)
            {
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
                if (Keybinds.IronMan_ToggleFaceplate.JustPressed) FaceplateMoving = true;
                if (Keybinds.IronMan_ToggleHelmet.JustPressed) HelmetOn = false;
            }
        }

        #endregion
    }
}