using BetelgueseLib.Registries;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace BetelgueseLib.Extensions;

public static partial class ItemExtensions 
{
    public static string ToTextIcon(this Item item)
        => $"[i{(item.prefix != -1 ? $"/p{item.prefix}" : "")}{(item.stack > 0 ? $"/s{item.stack}" : "")}:{(item.ModItem is null ? $"{item.type}" : $"{item.ModItem.FullName}")}]";

    public static bool TrySpawnPrefabItem(int type, string prefabName, Vector2 position, int stack = 1)
    {
        Item item = new();
        item.SetDefaults(type);
        item.stack = stack;

        if (!PrefabRegistry.TryGetPrefab(ItemID.Search.GetName(type), prefabName, out var prefab))
            return false;

        item.BatchEnableTags(prefab.Tags);

        foreach (var component in prefab.Components)
            item.TryEnableGlobal(component);

        Item.NewItem(new EntitySource_Misc("ItemSpawnAttempt"), position, item);
        return true;
    }

    public static bool TrySpawnPrefabItem(string fullName, string prefabName, Vector2 position, int stack = 1)
    {
        Item item = new();
        item.SetDefaults(ItemID.Search.GetId(fullName));
        item.stack = stack;

        if (!PrefabRegistry.TryGetPrefab(fullName, prefabName, out var prefab))
            return false;

        item.BatchEnableTags(prefab.Tags);

        foreach (var component in prefab.Components)
            item.TryEnableGlobal(component);

        Item.NewItem(new EntitySource_Misc("ItemSpawnAttempt"), position, item);
        return true;
    }
}