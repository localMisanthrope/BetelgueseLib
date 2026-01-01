using BetelgueseLib.Extensions;
using Microsoft.Xna.Framework.Input;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace BetelgueseLib.Systems;

public class ItemSpawnTestPlayer : ModPlayer
{
    public class ItemSpawnKeybind : ILoadable
    {
        public static ModKeybind SpawnKeybind { get; internal set; }

        public void Load(Mod mod) => SpawnKeybind = KeybindLoader.RegisterKeybind(mod, "Spawn Item", Keys.F);

        public void Unload() => SpawnKeybind = null;
    }

    public int iteration = 0;

    public string[] Items = ["BetelgueseLib/TestItemA", "BetelgueseLib/TestItemA", "BetelgueseLib/TestItemB"];
    public string[] Prefabs = ["TestItemAPrefabA", "TestItemAPrefabB", "TestItemBPrefabA"];

    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        if (ItemSpawnKeybind.SpawnKeybind.JustPressed)
        {
            ItemExtensions.TrySpawnPrefabItem(Items[iteration], Prefabs[iteration], Player.Center);
            iteration++;

            if (iteration >= 3)
                iteration = 0;
        }

        base.ProcessTriggers(triggersSet);
    }
}