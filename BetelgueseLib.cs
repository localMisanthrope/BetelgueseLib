using BetelgueseLib.Core;
using BetelgueseLib.Helpers;
using BetelgueseLib.Registries;
using System;
using System.Diagnostics;
using System.Linq;
using Terraria.ModLoader;

namespace BetelgueseLib;

public enum MessageType
{
    Info,
    Warn,
    Error
}

public class BetelgueseLib : Mod
{
    public static BetelgueseLib Instance => ModContent.GetInstance<BetelgueseLib>();

    /// <summary>
    /// BetelgeuseLib's localization key value.
    /// </summary>
    public const string LOCAL_KEY = "Mods.BetelgeuseLib.";

    private delegate void orig_Load(Mod self);

    public BetelgueseLib()
    {
        MonoModHooks.Add(typeof(Mod).GetMethod(nameof(Load), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public), On_Load);
    }

    public void Log(MessageType messageType, string messageName, params object[] args)
    {
        string message = GetLocalization($"Messages.{messageType}.{messageName}").Format(args);
        switch (messageType)
        {
            case MessageType.Info:
                Logger.Info(message);
                break;
            case MessageType.Warn:
                Logger.Warn(message);
                break;
            case MessageType.Error:
                Logger.Error(message);
                break;
        }
    }

    private static void On_Load(orig_Load load, Mod self)
    {
        load.Invoke(self);
        if (self == Instance || self.Name == "BetelgeuseLib")
            return;

        var watch = Stopwatch.StartNew();

        foreach (var type in self.Code.DefinedTypes.Where(x => x.BaseType == typeof(ModType)))
        {
            int templateCount = 0;
            int prefabCount = 0;

            string templatePath = (string)type.GetProperty("TemplatePath", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).GetValue(null);
            if (templatePath != null || templatePath != string.Empty)
            {
                foreach (var template in JsonHelpers.GetJsonContent<EntityTemplate>(self, templatePath))
                {
                    self.AddContent((ModType)Activator.CreateInstance(type, [template]));
                    templateCount++;
                }
            }

            self.Logger.Info($"Finished loading Entity Templates for type \"{type.Name}\" ({templateCount})");

            string prefabPath = (string)type.GetProperty("PrefabPath", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).GetValue(null);
            if (prefabPath != null || prefabPath != string.Empty)
            {
                foreach (var prefab in JsonHelpers.GetJsonContent<Prefab>(self, prefabPath))
                {
                    PrefabRegistry.TryRegisterPrefab(prefab);
                    prefabCount++;
                }
            }

            self.Logger.Info($"Finished loading Prefabs for type \"{type.Name}\" ({prefabCount})");
        }

        watch.Stop();
        self.Logger.Info($"Finished loading Prefabs and Entity Templates for {self.Name}; Took {watch.ElapsedMilliseconds} ms");
    }

    public override void Load() => Logger.Info("Mod(s) present made with BetelgeuseLib!");
}