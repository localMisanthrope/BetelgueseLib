using BetelgueseLib.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace BetelgueseLib.Examples.Items.Tags;

public class TestingItemTag : ItemTag
{
    public override string TagName => "IsTestingItem";

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (!TagEnabled)
            return;

        base.ModifyTooltips(item, tooltips);
        tooltips.Add(new(Mod, "IsTestingTagLine", "This is a testing item. If you see this, the tag is working!"));
    }
}