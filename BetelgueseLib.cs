using System.Diagnostics;
using System.Reflection;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BetelgueseLib;

public class BetelgueseLib : Mod
{
    public static BetelgueseLib Instance => ModContent.GetInstance<BetelgueseLib>();

    /// <summary>
    /// BetelgeuseLib's localization key value.
    /// </summary>
    public const string LOCAL_KEY = "Mods.BetelgeuseLib.";

    private delegate void orig_Load(Mod self);

    static BetelgueseLib() => MonoModHooks.Add(typeof(Mod).GetMethod("Load", BindingFlags.Public | BindingFlags.Instance), On_Load);

    private static void On_Load(orig_Load orig, Mod self)
    {
        orig.Invoke(self);

        if (self.Name == "BetelgeuseLib" || self.Name == "ModLoader")
            return;

        //Begin loading entities.
        var watch = Stopwatch.StartNew();

        watch.Stop();
        self.Logger.Info(Language.GetText($"{LOCAL_KEY}Messages.FinishedLoadingEntities").Format(self.Name, watch.Elapsed.TotalMilliseconds));
    }

    public override void Load() => Logger.Info("Mod(s) present made with BetelgeuseLib!");
}