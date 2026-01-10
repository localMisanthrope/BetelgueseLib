using BetelgueseLib.Core;
using System.Collections.Generic;
using Terraria;

namespace BetelgueseLib.Extensions;

public static partial class NPCExtensions
{
    public static bool TryFindTag(this NPC npc, string tagName, out NPCTag? tag)
    {
        foreach (var global in npc.EntityGlobals)
        {
            if (global is not NPCTag npcTag)
                continue;

            if (npcTag.TagName == tagName)
            {
                tag = npcTag;
                return true;
            }
        }

        tag = null;
        return false;
    }

    public static string[] GetNPCTags(this NPC npc)
    {
        List<string> list = [];

        foreach (var global in npc.EntityGlobals)
        {
            if (global is not NPCTag npcTag)
                continue;

            list.Add(npcTag.Name);
        }

        return [.. list];
    }

    public static bool TryEnableTag(this NPC npc, string tagName)
    {
        if (!npc.TryFindTag(tagName, out var tag))
            return false;

        tag.TagEnabled = true;
        return true;
    }

    public static bool IsTagEnabled(this NPC npc, string tagName)
        => npc.TryFindTag(tagName, out var tag) && tag.TagEnabled;

    public static bool TryRemoveTag(this NPC npc, string tagName)
    {
        if (!npc.TryFindTag(tagName, out var tag))
            return false;

        tag.TagEnabled = false;
        return true;
    }
}