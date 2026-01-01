using BetelgueseLib.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BetelgueseLib.Examples.Items.Components;

public struct TestComponentA(string modName, string globalName, int dataA, string dataB) : IComponent
{
    public string ModName { get; set; } = modName;

    public string GlobalName { get; set; } = globalName;

    public int DataA { get; set; } = dataA;

    public string DataB { get; set; } = dataB;
}

public sealed class TestComponentASerializer : TagSerializer<TestComponentA, TagCompound>
{
    public override TestComponentA Deserialize(TagCompound tag)
        => new(tag.GetString("ModName"), tag.GetString("GlobalName"), tag.GetInt(nameof(TestComponentA.DataA)), tag.GetString(nameof(TestComponentA.DataB)));

    public override TagCompound Serialize(TestComponentA value)
        => new()
        {
            ["ModName"] = value.ModName,
            ["GlobalName"] = value.GlobalName,
            [nameof(TestComponentA.DataA)] = value.DataA,
            [nameof(TestComponentA.DataB)] = value.DataB
        };
}

public class TestComponentGlobal : ItemComponentGlobal<TestComponentA>
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