using System;
using Terraria.ModLoader;

namespace BetelgueseLib.Helpers;

public static class CrossModHelpers
{
    public static bool GetModEntity<E>(string modName, string entityName, out E entity)
        where E : ModType
    {
        if (!ModLoader.TryGetMod(modName, out var mod))
        {
            entity = null;
            return false;
        }

        return mod.TryFind(entityName, out entity);
    }

    public static bool TryModifyEntity<E>(string modName, string entityName, Action<E> init = null)
        where E : ModType
    {
        if (!GetModEntity(modName, entityName, out E result))
        {
            //NoEntityFound warn.
            return false;
        }

        init?.Invoke(result);
        //Log change.
        return true;
    }
}