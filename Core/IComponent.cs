using Terraria.ModLoader.IO;

namespace BetelgueseLib.Core;

/// <summary>
/// The base instance for all components.
/// <br></br> All components must implement proper saving and loading for their data.
/// </summary>
public interface IComponent : TagSerializable
{
    /// <summary>
    /// The internal name of the Component.
    /// </summary>
    string Name => GetType().Name;

    /// <summary>
    /// The internal name of the Mod that holds this Component.
    /// </summary>
    string ModName { get; }

    /// <summary>
    /// The name of the <see cref="ItemGlobal"/> that utilizes this Component.
    /// </summary>
    string GlobalName { get; }

    IComponent Copy();
}