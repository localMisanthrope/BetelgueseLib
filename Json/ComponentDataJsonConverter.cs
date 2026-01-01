using BetelgueseLib.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace BetelgueseLib.Json;

internal sealed class ComponentDataJsonConverter : JsonConverter
{
    public override bool CanRead => true;

    public override bool CanWrite => false;

    public override bool CanConvert(Type objectType)
        => objectType == typeof(IComponent[]);

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        => throw new NotImplementedException();

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        List<IComponent> fields = [];
        var jsonArr = JArray.Load(reader);

        foreach (var item in jsonArr)
        {
            var jsonObj = item as JObject;
            var mod = ModLoader.GetMod(jsonObj["ModName"].Value<string>());
            var type = jsonObj!["$type"]!.Value<string>();
            var obj = jsonObj.ToObject(mod.Code.GetTypes().First(x => x.Name == type), serializer)!;
            var data = obj as IComponent;
            fields.Add(data);
        }

        return fields.ToArray();
    }
}