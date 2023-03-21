using MarvelTerrariaUniverse.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace MarvelTerrariaUniverse.Tiles
{
    public class GantryTileItem : ModItem
    {
        private void LoadEquipTextures(string texture)
        {
            EquipLoader.AddEquipTexture(Mod, $"MarvelTerrariaUniverse/IMTransformations/TransformationTextures/{texture}/{texture}_Head", EquipType.Head, name: texture);
            EquipLoader.AddEquipTexture(Mod, $"MarvelTerrariaUniverse/IMTransformations/TransformationTextures/{texture}/{texture}_Body", EquipType.Body, name: texture);
            EquipLoader.AddEquipTexture(Mod, $"MarvelTerrariaUniverse/IMTransformations/TransformationTextures/{texture}/{texture}_Legs", EquipType.Legs, name: texture);

            if (texture != "IronManMk1")
            {
                EquipLoader.AddEquipTexture(Mod, $"MarvelTerrariaUniverse/IMTransformations/TransformationTextures/{texture}/Flight/{texture}_Body_Flight", EquipType.Body, name: $"{texture}_Body_Flight");
                EquipLoader.AddEquipTexture(Mod, $"MarvelTerrariaUniverse/IMTransformations/TransformationTextures/{texture}/Flight/{texture}_Legs_Hover", EquipType.Legs, name: $"{texture}_Legs_Hover");

                for (int i = 0; i < 6; i++)
                {
                    EquipLoader.AddEquipTexture(Mod, $"MarvelTerrariaUniverse/IMTransformations/TransformationTextures/{texture}/Faceplate/{texture}_Faceplate{i}", EquipType.Head, name: $"{texture}_Faceplate{i}");
                }
            }

            ModContent.GetInstance<IronManModPlayer>().IronManSuitTextures.Add(texture);
        }

        private void SetupDrawing()
        {
            if (Main.netMode == NetmodeID.Server) return;

            foreach (string item in ModContent.GetInstance<IronManModPlayer>().IronManSuitTextures)
            {
                int head = EquipLoader.GetEquipSlot(Mod, item, EquipType.Head);
                int body = EquipLoader.GetEquipSlot(Mod, item, EquipType.Body);
                int legs = EquipLoader.GetEquipSlot(Mod, item, EquipType.Legs);

                if (item != "IronManMk1")
                {
                    for (int i = 0; i < 6; i++)
                    {
                        int faceplate = EquipLoader.GetEquipSlot(Mod, $"{item}_Faceplate{i}", EquipType.Head);
                        ArmorIDs.Head.Sets.DrawBackHair[faceplate] = false;
                    }

                    int bodyFlight = EquipLoader.GetEquipSlot(Mod, $"{item}_Body_Flight", EquipType.Body);
                    int legsHover = EquipLoader.GetEquipSlot(Mod, $"{item}_Legs_Hover", EquipType.Legs);

                    ArmorIDs.Body.Sets.HidesTopSkin[bodyFlight] = true;
                    ArmorIDs.Body.Sets.HidesArms[bodyFlight] = true;
                    ArmorIDs.Legs.Sets.HidesBottomSkin[legsHover] = true;
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

            LoadEquipTextures("WarMachineMk1");
            LoadEquipTextures("IronManMk1");
            LoadEquipTextures("IronManMk2");
            LoadEquipTextures("IronManMk3");
            LoadEquipTextures("IronManMk4");
            LoadEquipTextures("IronManMk5");
            LoadEquipTextures("IronManMk6");
            LoadEquipTextures("IronManMk7");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gantry");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

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
            Item.width = 96;
            Item.height = 74;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<GantryTile>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.CopperBar, 10)
            .AddIngredient(ItemID.IronBar, 20)
            .AddIngredient(ItemID.LeadBar, 5)
            .AddIngredient(ItemID.GoldBar, 5)
            .AddIngredient(ItemID.GrayPressurePlate, 1)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }

    public class GantryTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileMergeDirt[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.Width = 4;
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.Origin = new Point16(2, 0);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.CoordinateHeights = new[] { 16 };
            TileObjectData.addTile(Type);
            Main.placementPreview = true;
            AddMapEntry(new Color(0, 0, 127));
        }

        public override void PlaceInWorld(int i, int j, Item item)
        {
            WorldGen.PoundTile(i - 2, j);
            WorldGen.PoundTile(i - 1, j);
            WorldGen.PoundTile(i, j);
            WorldGen.PoundTile(i + 1, j);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(Main.LocalPlayer.GetSource_TileInteraction(i, j), i * 16, j * 16, 32, 16, ModContent.ItemType<GantryTileItem>());
        }

        public override bool Slope(int i, int j) => false;

        public bool PlayGantryFrames;
        public int GantryFrame = 0;
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            return false;
        }

        int Timer = 0;
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);

            Timer++;

            if (Timer > 3)
            {
                if (this.PlayGantryFrames)
                {
                    if (GantryFrame < 46) GantryFrame++;
                }
                else
                {
                    if (GantryFrame > 0) GantryFrame--;
                }

                Timer = 0;
            }

            if (Framing.GetTileSafely(i - 2, j).BlockType != BlockType.Solid && Framing.GetTileSafely(i + 1, j).BlockType != BlockType.Solid)
            {
                spriteBatch.Draw(ModContent.Request<Texture2D>("MarvelTerrariaUniverse/Tiles/GantryFrames").Value, new Vector2(i * 16 - (int)Main.screenPosition.X - 48, j * 16 - (int)Main.screenPosition.Y - 52) + zero, new Rectangle(0, 68 * GantryFrame, 96, 68), Color.White);
            }
        }

        public override void FloorVisuals(Player player)
        {
            this.PlayGantryFrames = true;
        }

        public override bool RightClick(int i, int j)
        {
            IronManModPlayer ModPlayer = Main.LocalPlayer.GetModPlayer<IronManModPlayer>();

            if (ModPlayer.TransformationActive_IronMan) Main.LocalPlayer.GetModPlayer<IronManModPlayer>().ResetSuits_IronMan();
            else ModPlayer.GantryUIActive = true;

            return true;
        }

    }
}