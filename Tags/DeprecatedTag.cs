using BetelgueseLib.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace BetelgueseLib.Tags;

public sealed class DeprecatedItemTag : ItemTag
{
    public override string TagName => "IsDeprecated";

    public string ModName { get; set; }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (!TagEnabled)
            return;

        item.SetNameOverride(item.Name + " (Deprecated)");
        tooltips.Clear();
        tooltips.Add(new(Mod, "ModWhoDeprecatedMeLine", $"Deprecated by [{ModName}]"));
        tooltips.Add(new(Mod, "DeprecatedLine", "This item has been deprecated and stripped of most function."));
    }
}