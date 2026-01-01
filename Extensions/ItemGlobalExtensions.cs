using BetelgueseLib.Core;
using Terraria;

namespace BetelgueseLib.Extensions;

public static class ItemGlobalExtensions
{
    /// <summary>
    /// Locates the Item's <see cref="ItemGlobal"/> instance by its name.
    /// <br></br> Do not use during the loading process, as <see cref="Item.EntityGlobals"/> is not populated at that time!
    /// </summary>
    /// <param name="item"></param>
    /// <param name="globalName"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool TryFindItemGlobal(this Item item, string globalName, out ItemGlobal result)
    {
        foreach (var global in item.EntityGlobals)
        {
            if (global.Name != globalName)
                continue;

            if (global is ItemGlobal itemGlobal)
            {
                result = itemGlobal;
                return true;
            }
        }

        //ComponentNotFound warn.
        result = null;
        return false;
    }

    public static bool TryEnableGlobal<D>(this Item item, D data)
        where D : IComponent
    {
        if (!item.TryFindItemGlobal(data.GlobalName, out var result))
            return false;

        result.Data = data;
        result.OnEnabled(item);
        //DebugComponentEnabled message.
        return true;
    }

    public static bool TryEnableGlobal<C, D>(this Item item, D data)
        where C : ItemGlobal
        where D : IComponent
    {
        if (!item.TryGetGlobalItem(out C global))
        {
            //ComponentNotFound warn.
            return false;
        }

        global.Data = data;
        global.OnEnabled(item);
        //DebugComponentEnabled message.
        return true;
    }

    public static bool IsGlobalEnabled<C>(this Item item)
        where C : ItemGlobal
        => item.TryGetGlobalItem(out C global) && global.Enabled;

    public static bool TryDisableGlobal<C>(this Item item)
        where C : ItemGlobal
    {
        if (!item.TryGetGlobalItem(out C global))
        {
            //ComponentNotFound warn.
            return false;
        }

        if (!global.Enabled)
        {
            //ComponentAlreadyDisabled warn.
            return false;
        }

        global.Data = null;
        global.OnDisabled(item);
        //DebugComponentDisabled message.
        return true;
    }
}