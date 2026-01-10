using BetelgueseLib.Core;
using System;
using System.Collections.Generic;
using Terraria;

namespace BetelgueseLib.Extensions;

public static partial class ItemExtensions
{
    public static bool TryFindTag<T>(this Item item, out T itemTag)
        where T : ItemTag
    {
        if (!item.TryGetGlobalItem(out T result))
        {
            itemTag = null;
            return false; 
        }

        itemTag = result;
        return true;
    }

    public static bool TryFindTag(this Item item, string tagName, out ItemTag? result)
    {
        foreach (var global in item.EntityGlobals)
        {
            if (global is not ItemTag tag)
                continue;

            if (tag.TagName == tagName)
            {
                result = tag;
                return true;
            }
        }

        result = null;
        return false;
    }

    public static bool TryEnableTag(this Item item, string tagName)
    {
        if (!item.TryFindTag(tagName, out var tag))
            return false;

        tag.TagEnabled = true;
        return true;
    }

    public static bool TryEnableTag<T>(this Item item, Action<T> init = null)
        where T : ItemTag
    {
        if (!item.TryFindTag(out T tag))
            return false;

        tag.TagEnabled = true;
        init?.Invoke(tag);
        return true;
    }

    public static void BatchEnableTags(this Item item, string[] tags)
    {
        foreach (var tag in tags)
            item.TryEnableTag(tag);
    }

    public static string[] GetItemTags(this Item item)
    {
        List<string> list = null;

        foreach (var global in item.EntityGlobals)
        {
            if (global is not ItemTag tag)
                continue;

            list.Add(tag.TagName);
        }

        return [.. list];
    }

    public static bool TryRemoveTag(this Item item, string tagName)
    {
        if (!item.TryFindTag(tagName, out var tag))
            return false;

        tag.TagEnabled = false;
        return true;
    }
}