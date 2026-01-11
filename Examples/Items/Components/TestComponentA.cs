using BetelgueseLib.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BetelgueseLib.Examples.Items.Components;

public struct TestComponentA(string ModName, string GlobalName, int DataA, string DataB) : IComponent
{
    public static readonly Func<TagCompound, TestComponentA> DESERIALIZER = Load;

    public string ModName { get; set; } = ModName;

    public string GlobalName { get; set; } = GlobalName;

    public int DataA { get; set; } = DataA;

    public string DataB { get; set; } = DataB;

    public readonly TagCompound SerializeData() => new()
    {
        [nameof(ModName)] = ModName,
        [nameof(GlobalName)] = GlobalName,
        [nameof(DataA)] = DataA,
        [nameof(DataB)] = DataB
    };

    public static TestComponentA Load(TagCompound tag)
        => new(tag.GetString(nameof(ModName)), tag.GetString(nameof(GlobalName)), tag.GetInt(nameof(DataA)), tag.GetString(nameof(DataB)));

    public readonly IComponent Copy() => new TestComponentA(ModName, GlobalName, DataA, DataB);
}

public class TestComponentGlobal : ItemComponentGlobal<TestComponentA>
{
    public int timer = 0;

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (!Enabled)
            return;

        base.ModifyTooltips(item, tooltips);
        tooltips.Add(new(Mod, "DataALine", $"Component DataA is: {ComponentData.DataA}."));
        tooltips.Add(new(Mod, "DataBLine", $"Component DataB is: {ComponentData.DataB}."));
    }

    public override void UpdateInventory(Item item, Player player)
    {
        if (!Enabled)
            return;

        timer++;

        if (timer >= 180)
        {
            ModifyData(delegate(ref TestComponentA dat) { dat.DataA++; });

            timer = 0;
        }

        base.UpdateInventory(item, player);
    }
}