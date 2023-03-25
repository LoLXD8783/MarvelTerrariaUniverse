using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace MarvelTerrariaUniverse;

public enum Transformations
{
    None = 0,
    IronMan = 1
}

public class MarvelTerrariaUniverse : Mod
{
    public static string AssetsFolder = "MarvelTerrariaUniverse/Assets";

    public static Dictionary<List<string>, EquipType> TransformationTextures = new();

    public override void Load()
    {
        GetFileNames().Where(e => e.StartsWith("Assets/Textures/Transformations")).ToList().ForEach(file =>
        {
            var root = "Assets/Textures/Transformations/";
            var path = file[root.Length..].Split("/");
            var type = path.ToList().Find(e => e.Contains(".rawimg")).Split(".")[0];
            var name = path.Length == 3 ? $"{path[0]}{path[1]}" : path[1];

            if (type.Contains("Alt"))
            {
                name += $"Alt{(type.Split("Alt").Length == 2 ? type.Split("Alt")[1] : "")}";
                type = type.Split("Alt")[0];

            }

            EquipLoader.AddEquipTexture(this, $"MarvelTerrariaUniverse/{file}".Split(".")[0], Enum.Parse<EquipType>(type), name: name);
            TransformationTextures.Add(new() { name }, Enum.Parse<EquipType>(type));
        });
    }
}