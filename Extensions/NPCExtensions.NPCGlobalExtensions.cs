using BetelgueseLib.Core;
using System;
using Terraria;

namespace BetelgueseLib.Extensions;

public static partial class NPCExtensions
{
    public static bool TryFindNPCGloblal(this NPC npc, string globalName, out NPCGlobal result)
    {
        foreach (var global in npc.EntityGlobals)
        {
            if (global is not NPCGlobal npcGlobal)
                continue;

            if (npcGlobal.Name == globalName)
            {
                result = npcGlobal;
                return true;
            }
        }

        result = null;
        return false;
    }

    public static bool TryEnableGlobal<D>(this NPC npc, D data)
        where D : IComponent
    {
        if (!npc.TryFindNPCGloblal(data.GlobalName, out var global))
            return false;

        global.Data = data;
        global.OnEnabled(npc);
        //DebugComponentEnabled message.
        return true;
    }

    public static bool IsGlobalEnabled(this NPC npc, string globalName)
        => npc.TryFindNPCGloblal(globalName, out var global) && global.Enabled;

    public static bool TryDisableGlobal(this NPC npc, string globalName)
    {
        if (!npc.TryFindNPCGloblal(globalName, out var global))
            return false;

        global.Data = null;
        global.OnDisabled(npc);
        return true;
    }

    public static bool TryEnableGlobal<G>(this NPC npc, Action<G> init = null)
        where G : NPCGlobal
    {
        if (!npc.TryGetGlobalNPC(out G result))
        {
            return false;
        }

        result.Data = new();
        init?.Invoke(result);
        result.OnEnabled(npc);
        return true;
    }
}