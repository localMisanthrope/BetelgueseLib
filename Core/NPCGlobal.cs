using BetelgueseLib.Json;
using Newtonsoft.Json;
using Terraria.ModLoader;

namespace BetelgueseLib.Core;

public struct NPCPrefab
{
    /// <summary>
    /// If your NPC prefab is a vanilla NPC, you would set this to the respective ID.
    /// <br></br> If it is not, set this to -1.
    /// </summary>
    public int NPCID { get; set; }

    /// <summary>
    /// The internal name of the NPC. Use this to identify the template instance you want to spawn.
    /// <br></br> Do not use this for vanilla NPCs.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The set NPCTags attributed to this NPC.
    /// <br></br> Note these are not the same as Components, and are handled in their own way!
    /// </summary>
    public string[] Tags { get; set; }

    [JsonConverter(typeof(ComponentDataJsonConverter))]
    public IComponentData[] Components { get; set; }
}

public abstract class NPCGlobal : GlobalNPC, ILocalizedModType
{
    public virtual string LocalizationCategory => "NPCGlobals";


}