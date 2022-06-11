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
        public string ActiveTransformation = "IronManMk3";

        public void UseEquipSlot(string texture)
        {
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

        public override void PreUpdate()
        {
            PreUpdate_IronMan();
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
        public int FaceplateAnimFrame = 0;
        public int FaceplateFrameTimer = 0;

        public bool TransformationActive_IronMan => TransformationActive_IronManMk2 || TransformationActive_IronManMk3;
        public bool TransformationActive_IronManMk2;
        public bool TransformationActive_IronManMk3;

        public void ResetSuits()
        {
            TransformationActive_IronManMk2 = false;
            TransformationActive_IronManMk3 = false;
        }

        public void FaceplateToggle()
        {
            if (!FaceplateOn)
            {
                if (FaceplateAnimFrame < 3)
                {
                    FaceplateFrameTimer++;

                    if (FaceplateFrameTimer > 5)
                    {
                        FaceplateAnimFrame++;
                        FaceplateFrameTimer = 0;
                    }
                }
            }
            else
            {
                if (FaceplateAnimFrame > 0)
                {
                    FaceplateFrameTimer++;

                    if (FaceplateFrameTimer > 5)
                    {
                        FaceplateAnimFrame--;
                        FaceplateFrameTimer = 0;
                    }
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

            if (TransformationActive_IronMan) FaceplateToggle();
        }

        public void FrameEffects_IronMan()
        {
            if (TransformationActive_IronManMk2) UseEquipSlot("IronManMk2");
            if (TransformationActive_IronManMk3) UseEquipSlot("IronManMk3");
        }

        public void PreUpdate_IronMan()
        {
            if (TransformationActive_IronMan) Main.playerInventory = false;
        }

        public void SetControls_IronMan()
        {
            if (TransformationActive_IronMan)
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
                if (Keybinds.IronMan_ToggleFaceplate.JustPressed) FaceplateOn = !FaceplateOn;
            }
        }

        #endregion
    }
}