using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace MarvelTerrariaUniverse.Tiles
{
    public class GantryTileItem : ModItem
    {
        private void LoadEquipTextures(string texture)
        {
            EquipLoader.AddEquipTexture(Mod, $"MarvelTerrariaUniverse/TransformationTextures/{texture}/{texture}_Head", EquipType.Head, name: texture);
            EquipLoader.AddEquipTexture(Mod, $"MarvelTerrariaUniverse/TransformationTextures/{texture}/{texture}_Body", EquipType.Body, name: texture);
            EquipLoader.AddEquipTexture(Mod, $"MarvelTerrariaUniverse/TransformationTextures/{texture}/{texture}_Legs", EquipType.Legs, name: texture);

            for (int i = 0; i < 6; i++)
            {
                EquipLoader.AddEquipTexture(Mod, $"MarvelTerrariaUniverse/TransformationTextures/{texture}/Faceplate/{texture}_Faceplate{i}", EquipType.Head, name: $"{texture}_Faceplate{i}");
            }

            ModContent.GetInstance<MarvelTerrariaUniverseModPlayer>().IronManSuitTextures.Add(texture);
        }

        private void SetupDrawing()
        {
            if (Main.netMode == NetmodeID.Server) return;

            foreach (string item in ModContent.GetInstance<MarvelTerrariaUniverseModPlayer>().IronManSuitTextures)
            {
                int head = EquipLoader.GetEquipSlot(Mod, item, EquipType.Head);
                int body = EquipLoader.GetEquipSlot(Mod, item, EquipType.Body);
                int legs = EquipLoader.GetEquipSlot(Mod, item, EquipType.Legs);

                for (int i = 0; i < 6; i++)
                {
                    int faceplate = EquipLoader.GetEquipSlot(Mod, $"{item}_Faceplate{i}", EquipType.Head);
                    ArmorIDs.Head.Sets.DrawBackHair[faceplate] = false;
                }

                ArmorIDs.Head.Sets.DrawBackHair[head] = false;
                ArmorIDs.Body.Sets.HidesTopSkin[body] = true;
                ArmorIDs.Body.Sets.HidesArms[body] = true;
                ArmorIDs.Legs.Sets.HidesBottomSkin[legs] = true;
            }
        }

        public override void Load()
        {
            if (Main.netMode == NetmodeID.Server) return;

            LoadEquipTextures("IronManMk1");
            LoadEquipTextures("IronManMk2");
            LoadEquipTextures("IronManMk3");
            LoadEquipTextures("IronManMk4");
            LoadEquipTextures("IronManMk5");
            LoadEquipTextures("IronManMk6");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gantry");

            SetupDrawing();
        }

        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.width = 58;
            Item.height = 46;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<GantryTile>();
        }
    }

    public class GantryTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileMergeDirt[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.Width = 7;
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.Origin = new Point16(3, 4);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16, 16 };
            TileObjectData.addTile(Type);
            Main.placementPreview = true;
            AddMapEntry(new Color(0, 0, 127));
        }

        public override void PlaceInWorld(int i, int j, Item item)
        {
            WorldGen.KillTile(i, j);
            WorldGen.PlaceTile(i, j, ModContent.TileType<GantryTileBase>(), true, true);
            WorldGen.SlopeTile(i - 2, j, 2);
            WorldGen.SlopeTile(i + 2, j, 1);
        }
    }
}