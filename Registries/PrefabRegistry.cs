using BetelgueseLib.Core;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace BetelgueseLib.Registries;

/// <summary>
/// A simple registry which stores <see cref="Prefab"/> instances.
/// </summary>
public sealed class PrefabRegistry : ILoadable
{
    private static readonly Dictionary<string, List<Prefab>> _reg = [];

    public static int Count => _reg.Count;

    /// <summary>
    /// Registers a <see cref="Prefab"/> to the entity in question.
    /// <br></br> If the entity's name is already registered to have Prefabs, the Prefab will be inserted into a list.
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns>True if the Prefab was registered; otherwise false.</returns>
    public static bool TryRegisterPrefab(Prefab prefab)
    {
        if (!_reg.ContainsKey(prefab.EntityName))
            _reg.Add(prefab.EntityName, []);

        if (_reg[prefab.EntityName].Contains(prefab))
        {
            //PrefabDuplicate warn.
            return false;
        }

        _reg[prefab.EntityName].Add(prefab);
        //DebugAddedPrefab message.
        return true;
    }

    /// <summary>
    /// Grabs the designated <see cref="Prefab"/> by the entity's name and the Prefab's name.
    /// </summary>
    /// <param name="entityName"></param>
    /// <param name="prefabName"></param>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static bool TryGetPrefab(string entityName, string prefabName, out Prefab prefab)
    {
        if (!_reg.TryGetValue(entityName, out var list))
        {
            //EntityNotRegisteredForPrefab warn.
            prefab = default;
            return false;
        }

        if (!list.Any(x => x.PrefabName == prefabName))
        {
            //PrefabNotFoundOnEntity warn.
            prefab = default;
            return false;
        }

        prefab = list.FirstOrDefault(x => x.PrefabName == prefabName);
        return true;
    }

    public void Load(Mod mod) { }

    public void Unload() => _reg.Clear();
}