using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace BetelgueseLib;

public class BetelgeuseLibConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    public static BetelgeuseLibConfig Instance => ModContent.GetInstance<BetelgeuseLibConfig>();

    [DefaultValue(false)]
    public bool LoadExampleContent { get; set; }

    [DefaultValue(false)]
    public bool DeveloperMode { get; set; }
}