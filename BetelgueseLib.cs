using Terraria.ModLoader;

namespace BetelgueseLib;

public class BetelgueseLib : Mod
{
    public static BetelgueseLib Instance => ModContent.GetInstance<BetelgueseLib>();

    /// <summary>
    /// BetelgeuseLib's localization key value.
    /// </summary>
    public const string LOCAL_KEY = "Mods.BetelgeuseLib.";

    public override void Load() => Logger.Info("Mod(s) present made with BetelgeuseLib!");
}