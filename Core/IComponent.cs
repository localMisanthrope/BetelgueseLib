namespace BetelgueseLib.Core;

public interface IComponent
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
}