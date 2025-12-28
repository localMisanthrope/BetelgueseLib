using BetelgueseLib.Core;
using BetelgueseLib.Registries;
using Terraria;

namespace BetelgueseLib.Extensions;

public static class ItemGlobalExtensions
{
    public static bool TrySubscribeToComponent<C, D>(this Item item, D componentData)
        where C : ItemGlobal
        where D : IComponentData
    {
        if (!ItemGlobalsRegistry.TryGetGlobal(typeof(C).Name, out ItemGlobal global))
        {
            //GlobalNotFound warn.
            return false;
        }

        if (!(global as ItemComponentGlobal<D>).TrySubscribe(item, componentData))
        {
            //FailedToInstantiate warn.
            return false;
        }
        global.OnEnabled(item);
        return true;
    }

    public static bool IsSubcribedToComponent<C, D>(this Item item) 
        where C : ItemGlobal
        where D : IComponentData
        => ItemGlobalsRegistry.TryGetGlobal(typeof(C).Name, out var global) && (global as ItemComponentGlobal<D>).Enabled(item);

    public static bool UnsubscribeFromComponent<C, D>(this Item item)
        where C : ItemGlobal
        where D : IComponentData
    {
        if (!ItemGlobalsRegistry.TryGetGlobal(typeof(C).Name, out ItemGlobal global))
        {
            //GlobalNotFound warn.
            return false;
        }

        if (!(global as ItemComponentGlobal<D>).TryUnsubscribe(item))
        {
            //FailedToInstantiate warn.
            return false;
        }
        global.OnDisabled(item);
        return true;
    }

    public static bool TrySubscribeToTag(this Item item, string tagName)
    {
        if (!ItemGlobalsRegistry.TryGetTagGlobal(tagName, out var global))
        {
            //TagNotFound warn.
            return false;
        }

        global.Subscribe(item);
        global.OnEnabled(item);
        return true;
    }

    public static bool IsSubscribedToTag(this Item item, string tagName) 
        => ItemGlobalsRegistry.TryGetTagGlobal(tagName, out var global) && global.Enabled(item);

    public static bool TryUnsubscribeToTag(this Item item, string tagName)
    {
        if (!ItemGlobalsRegistry.TryGetTagGlobal(tagName, out var global))
        {
            //TagNotFound warn.
            return false;
        }

        global.Unsubscribe(item);
        global.OnDisabled(item);
        return true;
    }
}