using BetelgueseLib.Core;
using System.Collections.Generic;
using Terraria;

namespace BetelgueseLib.Extensions;

public static class ItemTagExtensions
{
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

    public static string[] GetTags(this Item item)
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