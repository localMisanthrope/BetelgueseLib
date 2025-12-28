using BetelgueseLib.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace BetelgueseLib.Json;

internal sealed class ComponentDataJsonConverter : JsonConverter
{
    public override bool CanRead => true;

    public override bool CanWrite => false;

    public override bool CanConvert(Type objectType)
        => objectType == typeof(IComponentData);

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        => throw new NotImplementedException();

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        List<IComponentData> fields = [];
        var jsonArr = JArray.Load(reader);

        foreach (var item in jsonArr)
        {
            var jsonObj = item as JObject;
            var type = jsonObj!["$type"]!.Value<string>();
            var obj = (IComponentData)jsonObj.ToObject(Type.GetType(type!), serializer)!;
            fields.Add(obj);
        }

        return fields.ToArray();
    }
}