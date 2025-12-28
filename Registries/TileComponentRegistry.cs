using BetelgueseLib.Core;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace BetelgueseLib.Registries;

internal sealed class TileComponentRegistry : ILoadable
{
    private static readonly List<TileComponent> _reg = [];

    public static int Count => _reg.Count;

    public static int Register(TileComponent component)
    {
        int count = _reg.Count;
        _reg.Add(component);
        return count;
    }

    public static TileComponent GetComponent(int type) => type < 0 || type >= _reg.Count ? null : _reg[type];

    public void Load(Mod mod) { }

    public void Unload() { }
}