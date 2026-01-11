using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BetelgueseLib.Core;

/// <summary>
/// A custom ItemGlobal which does not hold any instance data.
/// </summary>
public abstract class ItemTag : GlobalItem, ILocalizedModType
{
    public sealed override bool InstancePerEntity => true;

    public virtual string LocalizationCategory => "ItemTags";

    public virtual string TagName { get; }

    public bool TagEnabled { get; set; } = false;

    public override void LoadData(Item item, TagCompound tag)
    {
        base.LoadData(item, tag);
        TagEnabled = tag.GetBool(nameof(TagEnabled));
    }

    public override void SaveData(Item item, TagCompound tag)
    {
        base.LoadData(item, tag);
        tag[nameof(TagEnabled)] = TagEnabled;
    }
}

/// <summary>
/// The base implementation with a nullable object as its Data.
/// </summary>
public abstract class ItemGlobal : GlobalItem, ILocalizedModType
{
    public sealed override bool InstancePerEntity { get; } = true;

    public virtual string LocalizationCategory => "ItemGlobals";

    public bool Enabled => Data != null;

    public object? Data { get; set; } = null;

    public virtual void OnEnabled(Item item) { }

    public virtual void OnDisabled(Item item) { }
}

/// <summary>
/// An implementation of <see cref="ItemGlobal"/> that takes <see cref="IComponent"/>.
/// </summary>
/// <typeparam name="D"></typeparam>
public abstract class ItemComponentGlobal<D> : ItemGlobal
    where D : IComponent
{
    public override string LocalizationCategory => "ItemComponentGlobals";

    public D ComponentData => (D)Data!;

    public delegate void RefAction<D>(ref D data);
    public void ModifyData(RefAction<D> init)
    {
        D inst = (D)ComponentData.Copy();
        init(ref inst);
        Data = inst;
    }

    public sealed override void SaveData(Item item, TagCompound tag)
    {
        if (!Enabled)
            return;

        base.SaveData(item, tag);
        tag[nameof(Data)] = (D)Data!;
    }

    public sealed override void LoadData(Item item, TagCompound tag)
    {
        base.LoadData(item, tag);
        Data = tag.Get<D>(nameof(Data));
    }
}