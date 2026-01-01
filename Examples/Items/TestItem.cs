using BetelgueseLib.Core;
using BetelgueseLib.Helpers;
using BetelgueseLib.Registries;
using Terraria.ModLoader;

namespace BetelgueseLib.Examples.Items;

[Autoload(false)]
public sealed class TestItem(EntityTemplate itemTemplate) : ModItem
{
    internal sealed class TestItemLoader : ILoadable
    {
        public void Load(Mod mod)
        {
            foreach (var data in JsonHelpers.GetJsonContent<EntityTemplate>(mod, TemplatePath))
                mod.AddContent(new TestItem(data));

            foreach (var prefab in JsonHelpers.GetJsonContent<Prefab>(mod, PrefabPath))
                PrefabRegistry.TryRegisterPrefab(prefab);
        }

        public void Unload() { }
    }

    public static string TemplatePath => "Examples/dat/TestItemTemplates.json";

    public static string PrefabPath => "Examples/dat/TestItemPrefabs.json";

    protected override bool CloneNewInstances => true;

    public override string LocalizationCategory => "TestItems";

    public override string Name => itemTemplate.TemplateName;

    public override string Texture => ResourceHelpers.ResolveTexturePath(itemTemplate.TexturePath);

    public override void SetDefaults()
    {
        Item.height = 20;
        Item.width = 20;
        base.SetDefaults();
    }
}