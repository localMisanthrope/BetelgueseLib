using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BetelgueseLib.Core;

/// <summary>
/// A specialized component which attaches to tiles to store data and execute logic.
/// </summary>
public abstract class TileComponent : ModType, ILocalizedModType
{
    public virtual string LocalizationCategory => "TileComponents";

    public int Type { get; private set; }

    public Tile Owner => Main.tile[Position];

    public Point Position { get; set; }

    public int X => Position.X;

    public int Y => Position.Y;

    public virtual void Init() { }

    public virtual void Update() { }

    public virtual void LoadData(TagCompound tag) { }

    public virtual void SaveData(TagCompound tag) { }

    protected override void Register()
    {
        ModTypeLookup<TileComponent>.Register(this);

    }
}

/// <summary>
/// A specialized <see cref="TileEntity"/> which acts as a container for this tile's <see cref="TileComponent"/>s.
/// </summary>
public sealed class TileComponentContainer : ModTileEntity
{
    private static readonly List<TileComponent> _components = [];

    public override bool IsTileValidForEntity(int x, int y)
        => Main.tile[x, y].HasTile;

    public override void Update()
    {
        if (!IsTileValidForEntity(Position.X, Position.Y))
            Kill(Position.X, Position.Y);

        if (_components.Count <= 0 || _components is null)
        {
            base.Update();
            return; 
        }

        foreach (var component in _components)
            component.Update();

        base.Update();
    }

    public override void LoadData(TagCompound tag)
    {
        var list = tag.GetList<TagCompound>("componentData");

        if (list is null || list.Count <= 0)
        {
            base.LoadData(tag);
            return;
        }

        foreach (var component in tag.GetList<TagCompound>("componentData"))
        {
            TileComponent instance = null;
            instance.Position = new(component.GetInt("X"), component.GetInt("Y"));
            if (component.ContainsKey("saveData"))
                instance.LoadData(component.Get<TagCompound>("saveData"));

            _components.Add(instance);
        }

        base.LoadData(tag);
    }

    public override void SaveData(TagCompound tag)
    {
        var list = new List<TagCompound>();
        var saveData = new TagCompound();

        if (_components is null)
        {
            base.SaveData(tag);
            return;
        }

        foreach (var component in _components)
        {
            component.SaveData(tag);

            var data = new TagCompound()
            {
                ["Type"] = component.Type,
                ["Name"] = component.Name,
                ["X"] = component.X,
                ["Y"] = component.Y
            };

            if (saveData.Count > 0)
            {
                data["saveData"] = saveData;
                saveData = [];
            }

            list.Add(data);
        }

        tag["componentData"] = list;
        base.SaveData(tag);
    }

    public override void OnKill() => _components.Clear();

    public override void Unload() => _components.Clear();
}