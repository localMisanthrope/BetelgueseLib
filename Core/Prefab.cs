using BetelgueseLib.Json;
using Newtonsoft.Json;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetelgueseLib.Core;

public struct Prefab
{    
    /// <summary>
    /// The internal name of the prefab.
    /// </summary>
    public string PrefabName { get; set; }

    /// <summary>
    /// The internal name of the entity.
    /// <br></br> Vanilla Prefabs should use the vanilla internal name (use <see cref="ItemID.Search"/>).
    /// <br></br> Modded Prefabs should use <see cref="ModType.FullName"/> format (e.g. "BetelgueseLib/TestItem").
    /// </summary>
    public string EntityName { get; set; }

    /// <summary>
    /// The entity's tags. Enabled when the entity is spawned.
    /// </summary>
    public string[] Tags { get; set; }

    /// <summary>
    /// The entity's Components. Enabled and instances when the entity is spawned.
    /// </summary>
    [JsonConverter(typeof(ComponentDataJsonConverter))]
    public IComponent[] Components { get; set; }
}