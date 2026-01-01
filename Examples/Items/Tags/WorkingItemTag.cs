using BetelgueseLib.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace BetelgueseLib.Examples.Items.Tags;

public class WorkingItemTag : ItemTag
{
    public override string TagName => "IsWorkingItem";

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (!TagEnabled)
            return;

        base.ModifyTooltips(item, tooltips);
        tooltips.Add(new(Mod, "IsWorkingTagLine", "This is a working item. If you see this, the tag is also working!"));
    }
}
