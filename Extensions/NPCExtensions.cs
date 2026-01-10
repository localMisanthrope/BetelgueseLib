using BetelgueseLib.Registries;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace BetelgueseLib.Extensions;

public static partial class NPCExtensions
{
    public static bool TrySpawnPrefabNPC(IEntitySource source, string fullName, string prefabName, Vector2 position)
    {
        NPC npc = NPC.NewNPCDirect(source, position, NPCID.Search.GetId(fullName));

        if (PrefabRegistry.TryGetPrefab(fullName, prefabName, out var prefab))
            return false;

        foreach (var tag in prefab.Tags)
            npc.TryEnableTag(tag);

        foreach (var component in prefab.Components)
            npc.TryEnableGlobal(component);

        return true;
    }

    public static bool TrySpawnPrefabNPC(IEntitySource source, int type, string prefabName, Vector2 position)
    {
        NPC npc = NPC.NewNPCDirect(source, position, type);

        if (PrefabRegistry.TryGetPrefab(NPCID.Search.GetName(type), prefabName, out var prefab))
            return false;

        foreach (var tag in prefab.Tags)
            npc.TryEnableTag(tag);

        foreach (var component in prefab.Components)
            npc.TryEnableGlobal(component);

        return true;
    }
}