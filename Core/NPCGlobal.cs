using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BetelgueseLib.Core;

public abstract class NPCTag : GlobalNPC, ILocalizedModType
{
    public sealed override bool InstancePerEntity => true;

    public virtual string LocalizationCategory => "NPCTags";

    public virtual string TagName { get; }

    public bool TagEnabled { get; set; } = false;

    public override void LoadData(NPC npc, TagCompound tag)
    {
        base.LoadData(npc, tag);
        TagEnabled = tag.GetBool(nameof(TagEnabled));
    }

    public override void SaveData(NPC npc, TagCompound tag)
    {
        base.LoadData(npc, tag);
        tag[nameof(TagEnabled)] = TagEnabled;
    }
}

public abstract class NPCGlobal : GlobalNPC, ILocalizedModType
{
    public sealed override bool InstancePerEntity => true;

    public virtual string LocalizationCategory => "NPCGlobals";

    public bool Enabled => Data != null || Data != default;

    public object? Data { get; set; } = null;

    public virtual void OnEnabled(NPC npc) { }

    public virtual void OnDisabled(NPC npc) { }
}

public abstract class NPCComponentGlobal<D> : NPCGlobal
    where D : IComponent
{
    public override string LocalizationCategory => "ItemComponentGlobals";

    public D ComponentData => (D)Data!;

    public override void SaveData(NPC npc, TagCompound tag)
    {
        if (!Enabled)
            return;

        base.SaveData(npc, tag);
        tag[nameof(Data)] = (D)Data!;
    }

    public override void LoadData(NPC npc, TagCompound tag)
    {
        base.LoadData(npc, tag);
        Data = tag.Get<D>(nameof(Data));
    }
}