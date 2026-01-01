using BetelgueseLib.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BetelgueseLib.Examples.Items.Components;

public struct TestComponentB(string modName, string globalName, float dataA, string dataB) : IComponent
{
    public string ModName { get; set; } = modName;

    public string GlobalName { get; set; } = globalName;

    public float DataA { get; set; } = dataA;

    public string DataB { get; set; } = dataB;
}

public sealed class TestComponentBSerializer : TagSerializer<TestComponentB, TagCompound>
{
    public override TestComponentB Deserialize(TagCompound tag)
        => new(tag.GetString("ModName"), tag.GetString("GlobalName"), tag.GetFloat(nameof(TestComponentB.DataA)), tag.GetString(nameof(TestComponentB.DataB)));

    public override TagCompound Serialize(TestComponentB value)
        => new()
        {
            ["ModName"] = value.ModName,
            ["GlobalName"] = value.GlobalName,
            [nameof(TestComponentB.DataA)] = value.DataA,
            [nameof(TestComponentB.DataB)] = value.DataB
        };
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