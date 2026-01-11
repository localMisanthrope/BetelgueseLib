using BetelgueseLib.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BetelgueseLib.Examples.Items.Components;

public struct TestComponentB(string ModName, string GlobalName, float DataA, string DataB) : IComponent
{
    public static readonly Func<TagCompound, TestComponentB> DESERIALIZER = Load;

    public string ModName { get; set; } = ModName;

    public string GlobalName { get; set; } = GlobalName;

    public float DataA { get; set; } = DataA;

    public string DataB { get; set; } = DataB;

    public readonly TagCompound SerializeData() => new()
    {
        [nameof(ModName)] = ModName,
        [nameof(GlobalName)] = GlobalName,
        [nameof(DataA)] = DataA,
        [nameof(DataB)] = DataB
    };

    public static TestComponentB Load(TagCompound tag)
        => new(tag.GetString(nameof(ModName)), tag.GetString(nameof(GlobalName)), tag.GetFloat(nameof(DataA)), tag.GetString(nameof(DataB)));

    public readonly IComponent Copy() => new TestComponentB(ModName, GlobalName, DataA, DataB);
}

public class OtherTestComponentGlobal : ItemComponentGlobal<TestComponentB>
{
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (!Enabled)
            return;

        base.ModifyTooltips(item, tooltips);
        tooltips.Add(new(Mod, "DataALine", $"Component DataA is: {ComponentData.DataA}."));
        tooltips.Add(new(Mod, "DataBLine", $"Component DataB is: {ComponentData.DataB}."));
    }
}