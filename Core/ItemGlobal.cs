using BetelgueseLib.Registries;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BetelgueseLib.Core;

/// <summary>
/// A base class for all forms of subscriber-based <see cref="Item"/> components and tags.
/// </summary>
public abstract class ItemGlobal : GlobalItem, ILocalizedModType
{
    public override bool InstancePerEntity { get; } = true;

    /// <summary>
    /// The internal type of this ItemGlobal.
    /// </summary>
    public int Type { get; set; }

    public virtual string LocalizationCategory => "ItemGlobals";

    /// <summary>
    /// Whether or not this ItemGlobal is enabled and should run logic.
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Useful for one-time loading actions when this ItemGlobal is enabled.
    /// </summary>
    /// <param name="item"></param>
    public virtual void OnEnabled(Item item) { }

    public virtual void OnDisabled(Item item) { }

    public override void Load() => Type = ItemGlobalsRegistry.Register(this);
}

/// <summary>
/// A variation of <see cref="ItemGlobal"/> which acts as an <see cref="ItemID.Sets"/> collection with added logic.
/// </summary>
public abstract class ItemTagGlobal : ItemGlobal
{
    public sealed override bool InstancePerEntity => false;

    public override string LocalizationCategory => "ItemTagGlobals";

    /// <summary>
    /// The name of the Tag to be used when subscribing.
    /// </summary>
    public virtual string TagName { get; set; }

    /// <summary>
    /// The <see cref="ItemID.Sets"/> this Tag uses to identify items subscribed to it.
    /// <br></br>You should not be directly modifying this!
    /// </summary>
    private bool[] TagIDSet { get; set; }

    /// <summary>
    /// Whether or not the current item is subscribed to this Tag.
    /// <br></br> Use this to check if an Item should run logic as you would any other component type.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public new bool Enabled(Item item) => TagIDSet[item.type];

    public void Subscribe(Item item) => TagIDSet[item.type] = true;

    public void Unsubscribe(Item item) => TagIDSet[item.type] = false;

    public override void Load()
    {
        base.Load();

        TagIDSet = ItemID.Sets.Factory.CreateNamedSet(TagName).RegisterBoolSet();
    }
}

/// <summary>
/// A variation of <see cref="ItemGlobal"/> which requires Items be manually subscribed to it, storing, saving, and modifying their data where necessary.
/// </summary>
/// <typeparam name="D"></typeparam>
public abstract class ItemComponentGlobal<D> : ItemGlobal
    where D : IComponentData
{
    public sealed override bool InstancePerEntity => false;

    public override string LocalizationCategory => "ItemComponentGlobals";

    /// <summary>
    /// The collection of <see cref="Item"/>s who are subscribed to this <see cref="ItemGlobal"/>.
    /// <br></br> You should not be directly modifying this!
    /// </summary>
    private static Dictionary<Item, D> _componentSubscribers = [];

    /// <summary>
    /// Checks whether or not the <see cref="Item"/> in question is subscribed to this Component.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public new bool Enabled(Item item) => _componentSubscribers.ContainsKey(item);

    public bool TrySubscribe(Item item, D data)
        => _componentSubscribers.TryAdd(item, data);

    public bool TryUnsubscribe(Item item)
        => _componentSubscribers.Remove(item);

    /// <summary>
    /// Attempts to grab the current data of the Item being iterated over.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="data"></param>
    /// <returns>If the data is a struct; the data if found; default else-wise.</returns>
    public bool TryGetCurrentData(Item item, out D data)
        => _componentSubscribers.TryGetValue(item, out data);

    public sealed override void SaveData(Item item, TagCompound tag)
    {
        tag["subs_items"] = _componentSubscribers.Keys.ToList();
        tag["subs_data"] = _componentSubscribers.Values.ToList();

        base.SaveData(item, tag);
    }

    public sealed override void LoadData(Item item, TagCompound tag)
    {
        var items = tag.Get<List<Item>>("subs_items");
        var values = tag.Get<List<D>>("subs_data");

        _componentSubscribers = items.Zip(values, (k, v) => new { key = k, value = v }).ToDictionary(x => x.key, x => x.value);

        base.LoadData(item, tag);
    }
}