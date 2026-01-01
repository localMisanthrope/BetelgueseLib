using BetelgueseLib.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using Terraria.ModLoader;

namespace BetelgueseLib.Helpers;

public static class JsonHelpers
{
    public static IEnumerable<D>? GetJsonContent<D>(Mod mod, string path)
        where D : struct
    {
        var file = mod.GetFileBytes(path);

        if (file is null)
        {
            //FileNotFound warn.
            return null;
        }

        return JsonConvert.DeserializeObject<IEnumerable<D>>(Encoding.UTF8.GetString(file));
    }
}