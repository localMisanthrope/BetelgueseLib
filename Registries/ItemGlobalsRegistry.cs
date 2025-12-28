using BetelgueseLib.Core;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace BetelgueseLib.Registries;

internal class ItemGlobalsRegistry : ILoadable
{
    private static readonly List<ItemGlobal> _reg = [];

    public static int Count => _reg.Count;

    public static int Register(ItemGlobal global)
    {
        int count = Count;
        _reg.Add(global);
        return count;
    }

    public static ItemGlobal GetGlobal(int type) => type < 0 || type >= _reg.Count ? null : _reg[type];

    public static bool TryGetGlobal(string name, out ItemGlobal global)
    {
        var result = _reg.Find(x => x.Name == name);

        if (result is null)
        {
            global = null;
            return false;
        }

        global = result;
        return true;
    }

    public static bool TryGetTagGlobal(string tagName, out ItemTagGlobal global)
    {
        var result = (ItemTagGlobal)_reg.Find(x => (x as ItemTagGlobal).Name == tagName);

        if (result is null)
        {
            global = null;
            return false;
        }

        global = result;
        return true;
    }

    public void Load(Mod mod) { }

    public void Unload() => _reg.Clear();
}
