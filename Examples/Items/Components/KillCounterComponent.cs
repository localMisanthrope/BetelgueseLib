using BetelgueseLib.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BetelgueseLib.Examples.Items.Components;

public struct KillCounterComponent(string ModName, string GlobalName, int KillCount = 0) : IComponent
{
    public static readonly Func<TagCompound, KillCounterComponent> DESERIALIZER = Load;

    public string ModName { get; set; } = ModName;

    public string GlobalName { get; set; } = GlobalName;

    public int KillCount { get; set; } = KillCount;

    public readonly TagCompound SerializeData() => new()
    {
        [nameof(ModName)] = ModName,
        [nameof(GlobalName)] = GlobalName,
        [nameof(KillCount)] = KillCount
    };

    public static KillCounterComponent Load(TagCompound tag)
        => new(tag.GetString(nameof(ModName)), tag.GetString(nameof(GlobalName)), tag.GetInt(nameof(KillCount)));

    public readonly IComponent Copy() => new KillCounterComponent(ModName, GlobalName, KillCount);
}

public sealed class KillCounterGlobal : ItemComponentGlobal<KillCounterComponent>
{
    public LocalizedText KillCounterTooltip => this.GetLocalization(nameof(KillCounterTooltip), () => "");

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (!Enabled)
            return;

        base.ModifyTooltips(item, tooltips);
        tooltips.Add(new(Mod, "KillCountLine", KillCounterTooltip.Format(ComponentData.KillCount)));
    }

    public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (!Enabled)
            return;

        base.OnHitNPC(item, player, target, hit, damageDone);
    }
}