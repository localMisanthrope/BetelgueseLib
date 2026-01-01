using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace BetelgueseLib.Helpers;

public static class ResourceHelpers
{
    public const string PLACEHOLDER_PATH = "BetelgueseLib/res/texture/placeholder";

    public static readonly Texture2D Placeholder = ModContent.Request<Texture2D>(PLACEHOLDER_PATH).Value;

    public static string ResolveTexturePath(string path)
        => ModContent.RequestIfExists<Texture2D>(path, out var asset) ? path : PLACEHOLDER_PATH;

    public static Texture2D ResolveTexture(string path)
        => ModContent.RequestIfExists<Texture2D>(path, out var asset) ? asset.Value : Placeholder;
}