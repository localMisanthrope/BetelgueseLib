using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace BetelgueseLib;

public class BetelgueseLibConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    public static BetelgueseLibConfig Instance => ModContent.GetInstance<BetelgueseLibConfig>();

    [DefaultValue(false)]
    public bool LoadExampleContent { get; set; }

    [DefaultValue(false)]
    public bool DeveloperMode { get; set; }
}